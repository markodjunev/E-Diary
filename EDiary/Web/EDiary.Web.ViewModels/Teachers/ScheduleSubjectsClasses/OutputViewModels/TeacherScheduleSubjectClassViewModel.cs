namespace EDiary.Web.ViewModels.Teachers.ScheduleSubjectsClasses.OutputViewModels
{
    using System;

    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class TeacherScheduleSubjectClassViewModel : IMapFrom<ScheduleSubjectClass>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }
    }
}
