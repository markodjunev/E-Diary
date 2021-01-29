namespace EDiary.Web.ViewModels.Administration.StudentsParents.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentParentsViewModel : IMapFrom<StudentParent>
    {
        public string ParentId { get; set; }

        public string ParentFirstName { get; set; }

        public string ParentLastName { get; set; }

        public string ParentEmail { get; set; }

        public string ParentUniqueCitizenshipNumber { get; set; }
    }
}
