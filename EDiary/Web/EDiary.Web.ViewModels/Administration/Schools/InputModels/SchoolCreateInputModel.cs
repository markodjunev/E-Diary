namespace EDiary.Web.ViewModels.Administration.Schools.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class SchoolCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }
    }
}
