namespace EDiary.Web.ViewModels.Administration.Users.InputViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Mapping;
    using EDiary.Web.ViewModels.Administration.Roles.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class UserCreateInputModel : IMapTo<ApplicationUser>
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
        [RemoteAttribute("IsEmailUsed", "Users", "Administration")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Unique Citizenship Number")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "The value must be exactly 10 digits")]
        [RemoteAttribute("IsUniqueCitizenshipNumberUsed", "Users", "Administration")]
        public string UniqueCitizenshipNumber { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [EnumDataType(typeof(Class))]
        public Class Class { get; set; }

        [Required]
        [EnumDataType(typeof(TypeOfClass))]
        public TypeOfClass TypeOfClass { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }

        public IEnumerable<ApplicationRoleDropDownViewModel> ApplicationRoles { get; set; }
    }
}
