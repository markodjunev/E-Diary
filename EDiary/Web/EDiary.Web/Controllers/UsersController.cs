namespace EDiary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Common.Classes.OutputViewModels;
    using EDiary.Web.ViewModels.Common.SubjectsClasses.OutputViewModels;
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsClassesService subjectsClassesService;

        public UsersController(
            IUsersService usersService,
            ISchoolsService schoolsService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesService subjectsClassesService)
        {
            this.usersService = usersService;
            this.schoolsService = schoolsService;
            this.userManager = userManager;
            this.subjectsClassesService = subjectsClassesService;
        }

        [Authorize(Roles = "Administrator,Principal")]
        public async Task<IActionResult> Search(int id)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            this.ViewBag.SchoolId = school.Id;
            this.ViewBag.SchoolName = school.Name;

            var user = await this.userManager.GetUserAsync(this.User);
            var isPrincipal = await this.userManager.IsInRoleAsync(user, GlobalConstants.PrincipalRoleName);
            if (isPrincipal == true && user.SchoolId != id)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            return this.View();
        }

        [Authorize(Roles = "Administrator,Principal")]
        public async Task<IActionResult> ByClass(int id, string @class, string typeOfClass)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var isPrincipal = await this.userManager.IsInRoleAsync(user, GlobalConstants.PrincipalRoleName);
            if (isPrincipal == true && user.SchoolId != id)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var isClassCorrect = Enum.IsDefined(typeof(Class), @class);
            var isTypeOfClassCorrect = Enum.IsDefined(typeof(TypeOfClass), typeOfClass);

            if (!isClassCorrect || !isTypeOfClassCorrect)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            this.ViewBag.Class = $"{@class} {typeOfClass}";
            var viewModel = new ByClassViewModel
            {
                Students = this.usersService.GetAllStudentsByClass<StudentByClassViewModel>(id, Enum.Parse<Class>(@class), Enum.Parse<TypeOfClass>(typeOfClass)),
                Subjects = this.subjectsClassesService.GetAllByClasses<SubjectByClassViewModel>(id, Enum.Parse<Class>(@class), Enum.Parse<TypeOfClass>(typeOfClass)),
            };

            return this.View(viewModel);
        }
    }
}
