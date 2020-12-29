namespace EDiary.Web.ViewModels.Common.Users.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentByClassViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UniqueCitizenshipNumber { get; set; }
    }
}
