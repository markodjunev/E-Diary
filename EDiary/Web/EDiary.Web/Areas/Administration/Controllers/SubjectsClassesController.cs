namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.Subjects.InputModels;
    using EDiary.Web.ViewModels.Administration.SubjectsClasses.InputViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsClassesController : AdministrationController
    {
        private readonly ISchoolsService schoolsService;
        private readonly ISubjectsService subjectsService;
        private readonly ISubjectsClassesService subjectsClassesService;

        public SubjectsClassesController(ISchoolsService schoolsService, ISubjectsService subjectsService, ISubjectsClassesService subjectsClassesService)
        {
            this.schoolsService = schoolsService;
            this.subjectsService = subjectsService;
            this.subjectsClassesService = subjectsClassesService;
        }

        public IActionResult AddSubjectToClass(int id)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            this.ViewBag.SchoolName = school.Name;
            var viewModel = new SubjectClassInputModel
            {
                Subjects = this.subjectsService.GetAll<SubjectsToClassDropDownViewModel>(id),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubjectToClass(SubjectClassInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (input.Class == Class.None || input.TypeOfClass == TypeOfClass.None)
            {
                return this.Json("Please choose a correct class");
            }

            if (this.subjectsClassesService.Exist(input.Class, input.TypeOfClass, input.SubjectId, id))
            {
                return this.Json("You have already added this subject to this class");
            }

            await this.subjectsClassesService.Create(input.Class, input.TypeOfClass, input.SubjectId, id);

            return this.RedirectToAction("Details", "Schools", new { area = string.Empty, id = id });
        }

        public async Task<IActionResult> Remove(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            await this.subjectsClassesService.Remove(id);

            return this.Redirect("/");
        }
    }
}
