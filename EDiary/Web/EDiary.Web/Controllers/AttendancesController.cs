﻿namespace EDiary.Web.Controllers
{
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Parents.Attendances.OuputViewModels;
    using EDiary.Web.ViewModels.Teachers.Attendances.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AttendancesController : BaseController
    {
        private readonly IAttendancesService attendancesService;
        private readonly ILessonsService lessonsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsClassesTeachersService subjectsClassesTeachersService;
        private readonly IStudentsParentsService studentsParentsService;

        public AttendancesController(
            IAttendancesService attendancesService,
            ILessonsService lessonsService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            ISubjectsClassesTeachersService subjectsClassesTeachersService,
            IStudentsParentsService studentsParentsService)
        {
            this.attendancesService = attendancesService;
            this.lessonsService = lessonsService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.subjectsClassesTeachersService = subjectsClassesTeachersService;
            this.studentsParentsService = studentsParentsService;
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Attend(int id, string studentId)
        {
            var lesson = this.lessonsService.GetLesson(id);

            if (lesson == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var student = this.usersService.GetUserById(studentId);

            if (student == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var exist = this.attendancesService.Exist(id, studentId);

            if (exist)
            {
                await this.attendancesService.UpdateAsync(id, studentId, true);
            }
            else
            {
                await this.attendancesService.CreateAsync(id, studentId, true);
            }

            return this.RedirectToAction("ByLesson", "Attendances", new { area = string.Empty, id = id });
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Absent(int id, string studentId)
        {
            var lesson = this.lessonsService.GetLesson(id);

            if (lesson == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var student = this.usersService.GetUserById(studentId);

            if (student == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var exist = this.attendancesService.Exist(id, studentId);

            if (exist)
            {
                await this.attendancesService.UpdateAsync(id, studentId, false);
            }
            else
            {
                await this.attendancesService.CreateAsync(id, studentId, false);
            }

            return this.RedirectToAction("ByLesson", "Attendances", new { area = string.Empty, id = id });
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> ByLesson(int id)
        {
            var lesson = this.lessonsService.GetLesson(id);

            if (lesson == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            this.ViewBag.LessonName = lesson.Name;
            this.ViewBag.LessonId = lesson.Id;

            var teacher = await this.userManager.GetUserAsync(this.User);

            var exist = this.subjectsClassesTeachersService.Exist(lesson.SubjectClassId, teacher.Id);

            if (!exist)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var viewModel = new AllStudentsAttendancesViewModel
            {
                Students = this.attendancesService.GetAllStudentsByLessonId<StudentAttendanceViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.ParentRoleName)]
        public async Task<IActionResult> LatestAttendaces(string id)
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

            var viewModel = new AllStudentsAttendancesByParentViewModel
            {
                Attendances = this.attendancesService.GetAllLatestAttendancesByStudentId<StudentAttendanceByParentViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
