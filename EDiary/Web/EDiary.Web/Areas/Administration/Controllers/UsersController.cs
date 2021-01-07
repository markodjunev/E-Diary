namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.Roles.InputModels;
    using EDiary.Web.ViewModels.Administration.Users.InputViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;
        private readonly ISchoolsService schoolsService;
        private readonly IRolesService rolesService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, ISchoolsService schoolsService, IRolesService rolesService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.schoolsService = schoolsService;
            this.rolesService = rolesService;
            this.userManager = userManager;
        }

        public IActionResult Create(int id)
        {
            var school = this.schoolsService.GetSchool(id);

            if (school == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            this.ViewBag.SchoolName = school.Name;

            var viewModel = new UserCreateInputModel
            {
                SchoolId = school.Id,
                ApplicationRoles = this.rolesService.GetAll<ApplicationRoleDropDownViewModel>(),
                Birthday = DateTime.UtcNow,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, UserCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var role = this.rolesService.GetRoleById(input.ApplicationRoleId);

            if (role == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var newUser = new ApplicationUser();

            if (role.Name != GlobalConstants.StudentRoleName)
            {
                newUser.Class = Class.None;
                newUser.TypeOfClass = TypeOfClass.None;
            }

            if (role.Name == GlobalConstants.StudentRoleName || role.Name == GlobalConstants.TeacherRoleName || role.Name == GlobalConstants.PrincipalRoleName)
            {
                if (role.Name == GlobalConstants.PrincipalRoleName)
                {
                    var isSchoolPrincipalAlreadyAdded = await this.usersService.IsSchoolPrincipalAlreadyAddedAsync(id);

                    if (isSchoolPrincipalAlreadyAdded)
                    {
                        return this.Json("The school has already a principal.");
                    }
                }

                if (role.Name == GlobalConstants.StudentRoleName)
                {
                    if (input.Class == Class.None || input.TypeOfClass == TypeOfClass.None)
                    {
                        return this.Json("Please choose correct class of the Student");
                    }

                    newUser.Class = input.Class;
                    newUser.TypeOfClass = input.TypeOfClass;
                }

                newUser.SchoolId = id;
            }

            newUser.FirstName = input.FirstName;
            newUser.LastName = input.LastName;
            newUser.Email = input.Email;
            newUser.UserName = input.UniqueCitizenshipNumber;
            newUser.Birthday = input.Birthday;
            newUser.UniqueCitizenshipNumber = input.UniqueCitizenshipNumber;

            var result = await this.userManager.CreateAsync(newUser, input.UniqueCitizenshipNumber);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(newUser, role.Name);
            }
            else
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            return this.RedirectToAction("Details", "Schools", new { area = string.Empty, id = id });
        }

        public IActionResult Edit(string id)
        {
            var user = this.usersService.GetUserById(id);

            if (user == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var model = new UserEditInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UniqueCitizenshipNumber = user.UniqueCitizenshipNumber,
                Birthday = user.Birthday ?? DateTime.Now,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.usersService.IsEmailVaildInEdit(input.Email, id) == false)
            {
                return this.Json($"The email {input.Email} is already in use.");
            }

            if (this.usersService.IsUniqueCitizenshipNumberVaildInEdit(input.UniqueCitizenshipNumber, id) == false)
            {
                return this.Json($"The UniqueCitizenshipNumber {input.UniqueCitizenshipNumber} is already in use.");
            }

            var user = await this.usersService.EditAsync(input, id);

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.StudentRoleName))
            {
                return this.RedirectToAction("ByClass", "Users", new { area = string.Empty, id = user.SchoolId, @class = user.Class, typeOfClass = user.TypeOfClass });
            }

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.ParentRoleName))
            {
                return this.Redirect("/");
            }

            return this.RedirectToAction("Details", "Schools", new { area = string.Empty, id = user.SchoolId });
        }

        public async Task<IActionResult> ChangeClass(string id)
        {
            var user = this.usersService.GetUserById(id);
            var isStudent = await this.userManager.IsInRoleAsync(user, GlobalConstants.StudentRoleName);

            if (user == null || isStudent == false)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            this.ViewBag.StudentFullName = $"{user.FirstName} {user.LastName}";

            var model = new ChangeClassInputModel
            {
                Class = user.Class ?? Class.None,
                TypeOfClass = user.TypeOfClass ?? TypeOfClass.None,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeClass(ChangeClassInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (input.Class == Class.None || input.TypeOfClass == TypeOfClass.None)
            {
                return this.Json("Enter a valid class");
            }

            await this.usersService.ChangeClassAsync(input.Class, input.TypeOfClass, id);

            var user = this.usersService.GetUserById(id);

            return this.RedirectToAction("Search", "Users", new { area = string.Empty, id = user.SchoolId });
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber)
        {
            var isUsed = this.usersService.IsUniqueCitizenshipNumberUsed(uniqueCitizenshipNumber);

            if (!isUsed)
            {
                return this.Json(true);
            }
            else
            {
                return this.Json($"Unique Citizenship Number {uniqueCitizenshipNumber} is already in use.");
            }
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsEmailUsed(string email)
        {
            var isUsed = this.usersService.IsEmailUsed(email);

            if (!isUsed)
            {
                return this.Json(true);
            }
            else
            {
                return this.Json($"The email {email} is already in use.");
            }
        }
    }
}
