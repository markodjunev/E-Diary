namespace EDiary.Data.Models
{
    using System;

    using EDiary.Data.Common.Models;

    public class ScheduleSubjectClass : BaseDeletableModel<int>
    {
        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public int SubjectClassId { get; set; }

        public virtual SubjectClass SubjectClass { get; set; }
    }
}
