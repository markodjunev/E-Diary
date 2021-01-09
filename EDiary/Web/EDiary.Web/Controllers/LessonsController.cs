namespace EDiary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Teachers.Lessons.InputModels;
    using EDiary.Web.ViewModels.Teachers.Lessons.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LessonsController : BaseController
    {
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly ILessonsService lessonsService;
        private readonly ISubjectsService subjectsService;

        public LessonsController(
            ISubjectsClassesService subjectsClassesService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            ILessonsService lessonsService,
            ISubjectsService subjectsService)
        {
            this.subjectsClassesService = subjectsClassesService;
            this.userManager = userManager;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.lessonsService = lessonsService;
            this.subjectsService = subjectsService;
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(int id)
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

            var subjectName = this.subjectsService.GetSubject(subjectClass.SubjectId).Name;
            this.ViewBag.Details = $"{subjectName} in {subjectClass.Class} {subjectClass.TypeOfClass}";

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(LessonCreateInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.lessonsService.CreateAsync(input.Name, input.StartAt, input.FinishAt, id);
            return this.RedirectToAction("All", "Lessons", new { area = string.Empty, id = id });
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> All(int id)
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

            var subjectName = this.subjectsService.GetSubject(subjectClass.SubjectId).Name;
            this.ViewBag.Details = $"{subjectName} in {subjectClass.Class} {subjectClass.TypeOfClass}";
            this.ViewBag.SubjectClassId = id;
            var viewModel = new AllTeacherLessonsViewModel
            {
                Lessons = this.lessonsService.GetAllBySubjectClassId<TeacherLessonViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
