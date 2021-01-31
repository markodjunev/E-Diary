namespace EDiary.Web.ViewModels.Teachers.SubjectsClasses.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentsBySubjectClass : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
