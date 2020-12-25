namespace EDiary.Web.ViewModels.Administration.Subjects.InputModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class SubjectsToClassDropDownViewModel : IMapFrom<Subject>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
