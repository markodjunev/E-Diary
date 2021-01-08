namespace EDiary.Web.ViewModels.Teachers.ScheduleSubjectsClasses.OutputViewModels
{
    using System.Collections.Generic;

    public class AllTeacherScheduleSubjectClassViewModel
    {
        public IEnumerable<TeacherScheduleSubjectClassViewModel> Schedule { get; set; }
    }
}
