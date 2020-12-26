namespace EDiary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Administration.ScheduleSubjectsClasses.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class ScheduleSubjectsClassesController : AdministrationController
    {
        private readonly ISubjectsClassesService subjectsClassesService;
        private readonly ISchoolsService schoolsService;
        private readonly ISubjectsService subjectsService;
        private readonly IScheduleSubjectsClassesService scheduleSubjectsClassesService;

        public ScheduleSubjectsClassesController(
            ISubjectsClassesService subjectsClassesService,
            ISchoolsService schoolsService,
            ISubjectsService subjectsService,
            IScheduleSubjectsClassesService scheduleSubjectsClassesService)
        {
            this.subjectsClassesService = subjectsClassesService;
            this.schoolsService = schoolsService;
            this.subjectsService = subjectsService;
            this.scheduleSubjectsClassesService = scheduleSubjectsClassesService;
        }

        public IActionResult Create(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var subjectName = this.subjectsService.GetSubject(subjectClass.SubjectId).Name;
            this.ViewBag.SubjectName = subjectName;

            var schoolName = this.schoolsService.GetSchool(subjectClass.SchoolId).Name;
            this.ViewBag.SchoolName = schoolName;
            this.ViewBag.Class = $"{subjectClass.Class.ToString()} {subjectClass.TypeOfClass.ToString()}";

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleSubjectClassInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.scheduleSubjectsClassesService.CreateAsync(input.StartAt, input.FinishAt, input.DayOfWeek, id);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var scheduleSubjectClass = this.scheduleSubjectsClassesService.GetById(id);

            if (scheduleSubjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            await this.scheduleSubjectsClassesService.DeleteAsync(id);

            return this.Redirect("/");
        }
    }
}
