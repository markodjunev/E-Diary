namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Parents.StudentsParents.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class StudentsParentsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentsParentsService studentsParentsService;

        public StudentsParentsController(
            UserManager<ApplicationUser> userManager,
            IStudentsParentsService studentsParentsService)
        {
            this.userManager = userManager;
            this.studentsParentsService = studentsParentsService;
        }

        [Authorize(Roles = GlobalConstants.ParentRoleName)]
        public async Task<IActionResult> MyKids()
        {
            var parent = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllStudentsByParentIdViewModel
            {
                Students = this.studentsParentsService.GetAllStudentsByParentId<StudentByParentIdViewModel>(parent.Id),
            };

            return this.View(viewModel);
        }
    }
}
