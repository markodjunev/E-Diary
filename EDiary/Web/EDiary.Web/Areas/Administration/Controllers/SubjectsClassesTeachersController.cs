namespace EDiary.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.SubjectsTeachers.OutputViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectsClassesTeachersController : AdministrationController
    {
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly ISubjectsTeachersService subjectsTeachersService;
        private readonly IUsersService usersService;

        public SubjectsClassesTeachersController(
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            ISubjectsClassesService subjectsClassesService,
            ISubjectsTeachersService subjectsTeachersService,
            IUsersService usersService)
        {
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.subjectsClassesService = subjectsClassesService;
            this.subjectsTeachersService = subjectsTeachersService;
            this.usersService = usersService;
        }

        public IActionResult AvailableTeachers(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var viewModel = new AllSubjectTeachersBySubjectId
            {
                Teachers = this.subjectsTeachersService.SubjectTeachers<SubjectTeachersBySubjectId>(subjectClass.SubjectId),
            };

            this.ViewBag.SubjectClassId = subjectClass.Id;
            return this.View(viewModel);
        }

        public async Task<IActionResult> Add(int id, string teacherId)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var subjectTeacher = this.subjectsTeachersService.GetSubjectTeacher(subjectClass.SubjectId, teacherId);

            if (subjectTeacher == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var exist = this.subjectsClassesTeachersService.Exist(id, teacherId);

            if (exist)
            {
                return this.Json("Already added");
            }

            await this.subjectsClassesTeachersService.CreateAsync(id, teacherId);

            return this.RedirectToAction("Schedule", "ScheduleSubjectsClasses", new { area = string.Empty, id = id });
        }

        public async Task<IActionResult> Remove(int id, string teacherId)
        {
            var exist = this.subjectsClassesTeachersService.Exist(id, teacherId);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            await this.subjectsClassesTeachersService.DeleteAsync(id, teacherId);

            return this.RedirectToAction("Schedule", "ScheduleSubjectsClasses", new { area = string.Empty, id = id });
        }
    }
}
