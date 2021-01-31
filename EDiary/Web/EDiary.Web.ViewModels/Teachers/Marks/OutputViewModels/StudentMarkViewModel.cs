namespace EDiary.Web.ViewModels.Teachers.Marks.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentMarkViewModel : IMapFrom<Mark>
    {
        public int Id { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public double Score { get; set; }

        public string NameOfExam { get; set; }

        public int SubjectClassTeacherId { get; set; }

        public int SubjectClassTeacherSubjectClassId { get; set; }

        public string SubjectClassTeacherTeacherId { get; set; }

        public string SubjectClassTeacherTeacherFirstName { get; set; }

        public string SubjectClassTeacherTeacherLastName { get; set; }
    }
}
