namespace EDiary.Data.Models
{
    using System.Collections.Generic;

    using EDiary.Data.Common.Models;

    public class Subject : BaseDeletableModel<int>
    {
        public Subject()
        {
            this.SubjectTeachers = new HashSet<SubjectTeacher>();
            this.SubjectClasses = new HashSet<SubjectClass>();
        }

        public string Name { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public ICollection<SubjectTeacher> SubjectTeachers { get; set; }

        public ICollection<SubjectClass> SubjectClasses { get; set; }
    }
}
