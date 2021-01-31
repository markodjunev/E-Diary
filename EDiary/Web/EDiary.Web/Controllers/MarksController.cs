namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Teachers.Marks.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MarksController : BaseController
    {
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly ISubjectsService subjectsService;
        private readonly IMarksService marksService;

        public MarksController(
            ISubjectsClassesService subjectsClassesService,
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            ISubjectsService subjectsService,
            IMarksService marksService)
        {
            this.subjectsClassesService = subjectsClassesService;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.subjectsService = subjectsService;
            this.marksService = marksService;
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(int id, string studentId)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var subject = this.subjectsService.GetSubject(subjectClass.SubjectId);
            this.ViewBag.SubjectName = subject.Name;

            var teacher = await this.userManager.GetUserAsync(this.User);

            var exist = this.subjectsClassesTeachersService.Exist(id, teacher.Id);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var student = this.usersService.GetUserById(studentId);

            var isStudent = await this.userManager.IsInRoleAsync(student, GlobalConstants.StudentRoleName);

            if (
                isStudent == false ||
                student.Class != subjectClass.Class ||
                student.TypeOfClass != subjectClass.TypeOfClass ||
                student.SchoolId != subjectClass.SchoolId)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            this.ViewBag.StudentInfo = $"{student.FirstName} {student.LastName}";

            return this.View();
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(int id, string studentId, CreateMarkInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var teacher = await this.userManager.GetUserAsync(this.User);

            var subjectClassTeacher = this.subjectsClassesTeachersService.GetBySubjectClassIdAndTeacherId(id, teacher.Id);

            await this.marksService.CreateAsync(input.NameOfExam, input.Score, studentId, subjectClassTeacher.Id);
            return this.RedirectToAction("Students", "SubjectsClasses", new { id = id });
        }
    }
}
