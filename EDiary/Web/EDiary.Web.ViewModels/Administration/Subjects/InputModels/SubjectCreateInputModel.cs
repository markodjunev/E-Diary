namespace EDiary.Web.ViewModels.Administration.Subjects.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubjectCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
