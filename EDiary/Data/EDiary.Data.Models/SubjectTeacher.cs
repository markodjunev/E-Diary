namespace EDiary.Data.Models
{
    using EDiary.Data.Common.Models;

    public class SubjectTeacher : BaseDeletableModel<int>
    {
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}
