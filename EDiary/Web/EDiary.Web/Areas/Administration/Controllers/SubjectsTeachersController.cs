namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.Schools.OutputViewModels;
    using EDiary.Web.ViewModels.Administration.SubjectsTeachers.OutputViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsTeachersController : AdministrationController
    {
        private readonly ISubjectsTeachersService subjectsTeachersService;
        private readonly ISubjectsService subjectsService;
        private readonly IUsersService usersService;
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubjectsTeachersController(
            ISubjectsTeachersService subjectsTeachersService,
            ISubjectsService subjectsService,
            IUsersService usersService,
            ISchoolsService schoolsService,
            UserManager<ApplicationUser> userManager)
        {
            this.subjectsTeachersService = subjectsTeachersService;
            this.subjectsService = subjectsService;
            this.usersService = usersService;
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddSubjectTeacher(int subjectId, string teacherId)
        {
            var subject = this.subjectsService.GetSubject(subjectId);

            if (subject == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var user = await this.userManager.FindByIdAsync(teacherId);

            if (user == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            if (subject.SchoolId != user.SchoolId)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var isTeacher = false;

            foreach (var role in roles)
            {
                if (role == GlobalConstants.TeacherRoleName)
                {
                    isTeacher = true;
                    break;
                }
            }

            if (!isTeacher)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var subjectTeacher = this.subjectsTeachersService.GetSubjectTeacher(subjectId, teacherId);
            if (subjectTeacher != null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            await this.subjectsTeachersService.CreateAsync(subjectId, teacherId);

            return this.RedirectToAction("Details", "Subjects", new { id = subjectId, area = string.Empty });
        }

        public async Task<IActionResult> AvailableTeachers(int id)
        {
            var subject = this.subjectsService.GetSubject(id);

            if (subject == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            this.ViewBag.SubjectName = subject.Name;
            this.ViewBag.SubjectId = subject.Id;

            var viewModel = new AllAvailableSubjectTeachers
            {
                Teachers = await this.usersService.GetAllAvailableSubjectTeachersAsync(subject.Id, subject.SchoolId),
                School = this.schoolsService.GetById<SchoolInAvailableSubjectTeachers>(subject.SchoolId),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> RemoveSubjectTeacher(int subjectId, string teacherId)
        {
            var subject = this.subjectsService.GetSubject(subjectId);

            if (subject == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var user = await this.userManager.FindByIdAsync(teacherId);

            if (user == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            if (subject.SchoolId != user.SchoolId)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var isTeacher = false;

            foreach (var role in roles)
            {
                if (role == GlobalConstants.TeacherRoleName)
                {
                    isTeacher = true;
                    break;
                }
            }

            if (!isTeacher)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var subjectTeacher = this.subjectsTeachersService.GetSubjectTeacher(subjectId, teacherId);
            if (subjectTeacher == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            await this.subjectsTeachersService.DeleteAsync(subjectId, teacherId);
            return this.RedirectToAction("All", "Subjects", new { id = subject.SchoolId, area = string.Empty });
        }
    }
}
