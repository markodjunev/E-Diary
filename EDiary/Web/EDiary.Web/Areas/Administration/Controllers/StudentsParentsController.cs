namespace EDiary.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class StudentsParentsController : AdministrationController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentsParentsService studentsParentsService;

        public StudentsParentsController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IStudentsParentsService studentsParentsService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.studentsParentsService = studentsParentsService;
        }

        public async Task<IActionResult> AddStudentParent(string studentId, string parentId)
        {
            var student = this.usersService.GetUserById(studentId);
            var isStudent = await this.userManager.IsInRoleAsync(student, GlobalConstants.StudentRoleName);
            var parent = this.usersService.GetUserById(parentId);
            var isParent = await this.userManager.IsInRoleAsync(parent, GlobalConstants.ParentRoleName);

            if (isStudent == false || isParent == false)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var exist = this.studentsParentsService.Exist(studentId, parentId);

            if (exist == true)
            {
                return this.Json($"The student {student.FirstName + ' ' + student.LastName} has already parent {parent.FirstName + ' ' + parent.LastName}");
            }

            await this.studentsParentsService.CreateAsync(studentId, parentId);

            return this.Redirect("/");
        }

        public async Task<IActionResult> RemoveStudentParent(string studentId, string parentId)
        {
            var exist = this.studentsParentsService.Exist(studentId, parentId);

            if (exist == false)
            {
                return this.Json($"Something went wrong");
            }

            await this.studentsParentsService.DeleteAsync(studentId, parentId);

            return this.Redirect("/");
        }
    }
}
