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
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;

        public SubjectsTeachersController(
            ISubjectsTeachersService subjectsTeachersService,
            ISubjectsService subjectsService,
            IUsersService usersService,
            ISchoolsService schoolsService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesService subjectsClassesService,
            ISubjectsClassesTeachersService subjectsClassesTeachersService)
        {
            this.subjectsTeachersService = subjectsTeachersService;
            this.subjectsService = subjectsService;
            this.usersService = usersService;
            this.schoolsService = schoolsService;
            this.userManager = userManager;
            this.subjectsClassesService = subjectsClassesService;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
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

            var isTeacher = await this.userManager.IsInRoleAsync(user, GlobalConstants.TeacherRoleName);

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

            var teacher = await this.userManager.FindByIdAsync(teacherId);

            if (teacher == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            if (subject.SchoolId != teacher.SchoolId)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var isTeacher = await this.userManager.IsInRoleAsync(teacher, GlobalConstants.TeacherRoleName);

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

            var subjectClasses = this.subjectsClassesService.GetAllBySubjectId(subjectId);

            foreach (var subjectClass in subjectClasses)
            {
                if (this.subjectsClassesTeachersService.Exist(subjectClass.Id, teacher.Id))
                {
                    await this.subjectsClassesTeachersService.DeleteAsync(subjectClass.Id, teacher.Id);
                }
            }

            return this.RedirectToAction("All", "Subjects", new { id = subject.SchoolId, area = string.Empty });
        }
    }
}
