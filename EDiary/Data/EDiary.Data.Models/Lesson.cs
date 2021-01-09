namespace EDiary.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EDiary.Data.Common.Models;

    public class Lesson : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int SubjectClassId { get; set; }

        public virtual SubjectClass SubjectClass { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
