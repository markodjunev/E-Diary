namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Teachers.SubjectsClassesTeachers.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsClassesTeachersController : BaseController
    {
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubjectsClassesTeachersController(
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            UserManager<ApplicationUser> userManager)
        {
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> MySubjects()
        {
            var teacher = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllSubjectsClassesTeachersViewModel
            {
                SubjectClasses = this.subjectsClassesTeachersService.GetAllByTeacherId<SubjectsClassesTeachersViewModel>(teacher.Id),
            };

            return this.View(viewModel);
        }
    }
}
