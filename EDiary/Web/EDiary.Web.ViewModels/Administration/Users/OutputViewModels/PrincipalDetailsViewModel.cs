namespace EDiary.Web.ViewModels.Administration.Users.OutputViewModels
{
    using System;

    public class PrincipalDetailsViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string UniqueCitizenshipNumber { get; set; }
    }
}
