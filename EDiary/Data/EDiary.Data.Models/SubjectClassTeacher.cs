namespace EDiary.Data.Models
{
    using EDiary.Data.Common.Models;

    public class SubjectClassTeacher : BaseDeletableModel<int>
    {
        public int SubjectClassId { get; set; }

        public virtual SubjectClass SubjectClass { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}
