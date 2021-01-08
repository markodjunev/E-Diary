namespace EDiary.Web.ViewModels.Teachers.SubjectsClassesTeachers.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Mapping;

    public class SubjectsClassesTeachersViewModel : IMapFrom<SubjectClassTeacher>
    {
        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string SubjectClassId { get; set; }

        public string SubjectClassSubjectName { get; set; }

        public Class SubjectClassClass { get; set; }

        public TypeOfClass SubjectClassTypeOfClass { get; set; }
    }
}
