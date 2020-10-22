namespace EDiary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Common.Schools.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SchoolsController : BaseController
    {
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SchoolsController(ISchoolsService schoolsService, UserManager<ApplicationUser> userManager)
        {
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var school = this.schoolsService.GetById<SchoolDetailsViewModel>(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(school);
        }
    }
}
