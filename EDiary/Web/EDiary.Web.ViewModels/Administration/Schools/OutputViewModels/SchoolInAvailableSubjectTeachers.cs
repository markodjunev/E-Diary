namespace EDiary.Web.ViewModels.Administration.Schools.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class SchoolInAvailableSubjectTeachers : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
