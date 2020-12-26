namespace EDiary.Data.Models
{
    using System;

    using EDiary.Data.Common.Models;

    public class ScheduleSubjectClass : BaseDeletableModel<int>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }

        public int SubjecClassId { get; set; }

        public virtual SubjectClass SubjectClass { get; set; }
    }
}
