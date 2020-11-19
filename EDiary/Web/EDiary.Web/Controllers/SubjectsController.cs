namespace EDiary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Common.Subjects.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SubjectsController : BaseController
    {
        private readonly ISubjectsService subjectsService;
        private readonly ISchoolsService schoolsService;
        private readonly IUsersService usersService;

        public SubjectsController(
            ISubjectsService subjectsService,
            ISchoolsService schoolsService,
            IUsersService usersService)
        {
            this.subjectsService = subjectsService;
            this.schoolsService = schoolsService;
            this.usersService = usersService;
        }

        public IActionResult All(int id)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.ViewBag.SchoolId = school.Id;
            this.ViewBag.SchoolName = school.Name;

            var subjects = new AllSubjectsViewModel
            {
                Subjects = this.subjectsService.GetAll<SubjectsViewModel>(id),
            };

            return this.View(subjects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subject = this.subjectsService.GetSubject(id);

            if (subject == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.ViewBag.SubjectId = id;
            this.ViewBag.SubjectName = subject.Name;
            this.ViewBag.SchoolId = subject.SchoolId;

            var viewModel = new SubjectDetailsViewModel
            {
                Teachers = await this.usersService.GetAllTeachersBySubjectIdAsync(id),
            };

            return this.View(viewModel);
        }
    }
}
