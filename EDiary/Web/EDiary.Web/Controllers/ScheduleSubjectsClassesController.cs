namespace EDiary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Services.Data.Interfaces;
    using EDiary.Web.ViewModels.Common.ScheduleSubjectsClasses.OutputViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ScheduleSubjectsClassesController : BaseController
    {
        private readonly IScheduleSubjectsClassesService scheduleSubjectsClassesService;
        private readonly ISubjectsClassesService subjectsClassesService;

        public ScheduleSubjectsClassesController(
            IScheduleSubjectsClassesService scheduleSubjectsClassesService,
            ISubjectsClassesService subjectsClassesService)
        {
            this.scheduleSubjectsClassesService = scheduleSubjectsClassesService;
            this.subjectsClassesService = subjectsClassesService;
        }

        [Authorize(Roles = "Administrator,Principal")]
        public IActionResult Schedule(int id)
        {
            var subjectClass = this.subjectsClassesService.GetById(id);

            if (subjectClass == null)
            {
                return this.RedirectToAction("Error", "Home", new { area = string.Empty });
            }

            var viewModel = new ScheduleSubjectsClassesViewModel
            {
                Schedule = this.scheduleSubjectsClassesService.GetAllBySubjectClassId<ScheduleSubjectClassViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
