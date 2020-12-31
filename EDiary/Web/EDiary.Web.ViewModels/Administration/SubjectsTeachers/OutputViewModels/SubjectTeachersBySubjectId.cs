namespace EDiary.Web.ViewModels.Administration.SubjectsTeachers.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class SubjectTeachersBySubjectId : IMapFrom<SubjectTeacher>
    {
        public string TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherEmail { get; set; }
    }
}
