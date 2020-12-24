namespace EDiary.Web.ViewModels.Administration.Users.InputViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserEditInputModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Unique Citizenship Number")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "The value must be exactly 10 digits")]
        public string UniqueCitizenshipNumber { get; set; }
    }
}
