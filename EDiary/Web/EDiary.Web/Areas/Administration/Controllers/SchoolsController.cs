namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.Schools.InputModels;
    using EDiary.Web.ViewModels.Administration.Schools.OutputViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SchoolsController : AdministrationController
    {
        private readonly ISchoolsService schoolsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<ApplicationUser> userManager;

        public SchoolsController(ISchoolsService schoolsService, ICloudinaryService cloudinaryService, UserManager<ApplicationUser> userManager)
        {
            this.schoolsService = schoolsService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var schoolsViewModel = new AllSchoolsViewModel
            {
                Schools = this.schoolsService.GetAll<SchoolsViewModel>(),
            };

            return this.View(schoolsViewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SchoolCreateInputModel input)
        {
            // var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var imageUrl = await this.cloudinaryService.UploadPictureAsync(input.ImageUrl, Guid.NewGuid().ToString());
            var schoolId = await this.schoolsService.CreateAsync(input.Name, input.Address, input.City, imageUrl);

            return this.Redirect("/");
        }
    }
}
