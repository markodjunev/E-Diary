namespace EDiary.Data.Models
{
    using System.Collections.Generic;

    using EDiary.Data.Common.Models;
    using EDiary.Data.Models.Enums;

    public class SubjectClass : BaseDeletableModel<int>
    {
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public Class Class { get; set; }

        public TypeOfClass TypeOfClass { get; set; }

    }
}
