namespace EDiary.Web.ViewModels.Common.ScheduleSubjectsClasses.OutputViewModels
{
    using System.Collections.Generic;

    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;

    public class ScheduleSubjectsClassesViewModel
    {
        public IEnumerable<ScheduleSubjectClassViewModel> Schedule { get; set; }

        public IEnumerable<TeacherScheduleSubjectClass> Teachers { get; set; }
    }
}
