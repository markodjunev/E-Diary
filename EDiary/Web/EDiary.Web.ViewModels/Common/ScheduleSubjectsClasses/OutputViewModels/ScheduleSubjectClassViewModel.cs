namespace EDiary.Web.ViewModels.Common.ScheduleSubjectsClasses.OutputViewModels
{
    using System;

    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class ScheduleSubjectClassViewModel : IMapFrom<ScheduleSubjectClass>
    {
        public int Id { get; set; }

        public int SubjectClassId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }
    }
}
