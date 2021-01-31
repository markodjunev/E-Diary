namespace EDiary.Data.Models
{
    using EDiary.Data.Common.Models;

    public class Mark : BaseDeletableModel<int>
    {
        public double Score { get; set; }

        public string NameOfExam { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public int SubjectClassTeacherId { get; set; }

        public virtual SubjectClassTeacher SubjectClassTeacher { get; set; }
    }
}
