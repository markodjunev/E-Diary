namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Teachers.SubjectsClasses.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsClassesController : BaseController
    {
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly IUsersService usersService;

        public SubjectsClassesController(
            ISubjectsClassesService subjectsClassesService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            IUsersService usersService)
        {
            this.subjectsClassesService = subjectsClassesService;
            this.userManager = userManager;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.usersService = usersService;
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Students(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var teacher = await this.userManager.GetUserAsync(this.User);

            var exist = this.subjectsClassesTeachersService.Exist(id, teacher.Id);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var viewModel = new AllStudentsBySubjectClass
            {
                Students = this.usersService.GetAllStudentsByClass<StudentsBySubjectClass>(subjectClass.SchoolId, subjectClass.Class, subjectClass.TypeOfClass),
                SubjectClassInfo = this.subjectsClassesService.GetInfo<SubjectClassInfoStudents>(id),
            };

            return this.View(viewModel);
        }
    }
}
