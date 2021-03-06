﻿namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Common.ScheduleSubjectsClasses.OutputViewModels;
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;
    using EDiary.Web.ViewModels.Teachers.ScheduleSubjectsClasses.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ScheduleSubjectsClassesController : BaseController
    {
        private readonly IScheduleSubjectsClassesService scheduleSubjectsClassesService;
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;

        public ScheduleSubjectsClassesController(
            IScheduleSubjectsClassesService scheduleSubjectsClassesService,
            ISubjectsClassesService subjectsClassesService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesTeachersService subjectsClassesTeachersService)
        {
            this.scheduleSubjectsClassesService = scheduleSubjectsClassesService;
            this.subjectsClassesService = subjectsClassesService;
            this.userManager = userManager;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
        }

        [Authorize(Roles = "Administrator,Principal")]
        public async Task<IActionResult> Schedule(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var isPrincipal = await this.userManager.IsInRoleAsync(user, GlobalConstants.PrincipalRoleName);
            if (isPrincipal == true && user.SchoolId != subjectClass.SchoolId)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var viewModel = new ScheduleSubjectsClassesViewModel
            {
                Schedule = this.scheduleSubjectsClassesService.GetAllBySubjectClassId<ScheduleSubjectClassViewModel>(id),
                Teachers = this.subjectsClassesTeachersService.GetAllBySubjectClassId<TeacherScheduleSubjectClass>(id),
            };

            this.ViewBag.SubjectClassId = id;

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> TeacherSchedule(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var teacher = await this.userManager.GetUserAsync(this.User);

            var exist = this.subjectsClassesTeachersService.Exist(id, teacher.Id);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var viewModel = new AllTeacherScheduleSubjectClassViewModel
            {
                Schedule = this.scheduleSubjectsClassesService.GetAllBySubjectClassId<TeacherScheduleSubjectClassViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
