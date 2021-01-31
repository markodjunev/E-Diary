namespace EDiary.Web.ViewModels.Teachers.SubjectsClasses.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Mapping;

    public class SubjectClassInfoStudents : IMapFrom<SubjectClass>
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public Class Class { get; set; }

        public TypeOfClass TypeOfClass { get; set; }

        public string SchoolName { get; set; }
    }
}
