namespace EDiary.Web.ViewModels.Common.Subjects.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class SubjectsViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
