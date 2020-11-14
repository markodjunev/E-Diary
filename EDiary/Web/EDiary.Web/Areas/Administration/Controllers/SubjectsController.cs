namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.Subjects.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsController : AdministrationController
    {
        private readonly ISubjectsService subjectsService;
        private readonly ISchoolsService schoolsService;

        public SubjectsController(ISubjectsService subjectsService, ISchoolsService schoolsService)
        {
            this.subjectsService = subjectsService;
            this.schoolsService = schoolsService;
        }

        public IActionResult Create(int id)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            this.ViewBag.SchoolName = school.Name;
            this.ViewBag.SchoolId = id;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, SubjectCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.subjectsService.CreateAsync(id, input.Name);

            return this.RedirectToAction("Details", "Schools", new { id, area = string.Empty });
        }
    }
}
