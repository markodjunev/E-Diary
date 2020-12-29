namespace EDiary.Web.ViewModels.Common.SubjectsClasses.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Mapping;

    public class SubjectByClassViewModel : IMapFrom<SubjectClass>
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }
    }
}
