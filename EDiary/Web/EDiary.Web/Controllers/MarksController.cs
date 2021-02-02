namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Parents.Marks.OutputViewModels;
    using EDiary.Web.ViewModels.Teachers.Marks.InputModels;
    using EDiary.Web.ViewModels.Teachers.Marks.OutputViewModels;
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
        private readonly IStudentsParentsService studentsParentsService;

        public MarksController(
            ISubjectsClassesService subjectsClassesService,
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            ISubjectsService subjectsService,
            IMarksService marksService,
            IStudentsParentsService studentsParentsService)
        {
            this.subjectsClassesService = subjectsClassesService;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.subjectsService = subjectsService;
            this.marksService = marksService;
            this.studentsParentsService = studentsParentsService;
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

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> All(int id, string studentId)
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

            var subjectClassTeacherId = this.subjectsClassesTeachersService.GetBySubjectClassIdAndTeacherId(id, teacher.Id).Id;

            var viewModel = new AllStudentMarksViewModel
            {
                Marks = this.marksService.GetAllByStudentIdAndSubjectClassTeacherId<StudentMarkViewModel>(studentId, subjectClassTeacherId),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var mark = this.marksService.GetById(id);

            if (mark == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var teacherId = this.subjectsClassesTeachersService.GetById(mark.SubjectClassTeacherId).TeacherId;
            var teacher = await this.userManager.GetUserAsync(this.User);

            if (teacherId != teacher.Id)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var editViewModel = new EditMarkInputModel
            {
                NameOfExam = mark.NameOfExam,
                Score = mark.Score,
            };

            return this.View(editViewModel);
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMarkInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.marksService.EditAsync(id, input.NameOfExam, input.Score);

            var mark = this.marksService.GetById(id);
            var subjectClassTeacher = this.subjectsClassesTeachersService.GetById(mark.SubjectClassTeacherId);

            return this.RedirectToAction("All", "Marks", new { id = subjectClassTeacher.SubjectClassId, studentId = mark.StudentId });
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var mark = this.marksService.GetById(id);

            if (mark == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var teacherId = this.subjectsClassesTeachersService.GetById(mark.SubjectClassTeacherId).TeacherId;
            var teacher = await this.userManager.GetUserAsync(this.User);

            if (teacherId != teacher.Id)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var studentId = mark.StudentId;
            var subjectClassId = this.subjectsClassesTeachersService.GetById(mark.SubjectClassTeacherId).SubjectClassId;

            await this.marksService.DeleteAsync(id);

            return this.RedirectToAction("All", "Marks", new { id = subjectClassId, studentId = studentId });
        }

        [Authorize(Roles = GlobalConstants.ParentRoleName)]
        public async Task<IActionResult> LatestMarks(string id)
        {
            var student = this.usersService.GetUserById(id);

            if (student == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var isStudent = await this.userManager.IsInRoleAsync(student, GlobalConstants.StudentRoleName);

            if (!isStudent)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var parent = await this.userManager.GetUserAsync(this.User);
            var exist = this.studentsParentsService.Exist(id, parent.Id);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty, });
            }

            var viewModel = new AllStudentsLatestMarks
            {
                Marks = this.marksService.GetAllLatestMarksByStudentId<StudentLatestMarks>(id),
            };

            return this.View(viewModel);
        }
    }
}
