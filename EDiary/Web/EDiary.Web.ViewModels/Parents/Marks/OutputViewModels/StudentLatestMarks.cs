namespace EDiary.Web.ViewModels.Parents.Marks.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentLatestMarks : IMapFrom<Mark>
    {
        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public double Score { get; set; }

        public string NameOfExam { get; set; }

        public string SubjectClassTeacherSubjectClassSubjectName { get; set; }

        public string SubjectClassTeacherTeacherFirstName { get; set; }

        public string SubjectClassTeacherTeacherLastName { get; set; }
    }
}
