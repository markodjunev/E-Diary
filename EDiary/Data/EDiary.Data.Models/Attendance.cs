namespace EDiary.Data.Models
{
    using EDiary.Data.Common.Models;

    public class Attendance : BaseDeletableModel<int>
    {
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public bool IsAttended { get; set; }
    }
}
