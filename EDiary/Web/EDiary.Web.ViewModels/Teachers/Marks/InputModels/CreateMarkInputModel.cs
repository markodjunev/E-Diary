namespace EDiary.Web.ViewModels.Teachers.Marks.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateMarkInputModel
    {
        [Required]
        [Display(Name = "Name of exam")]
        public string NameOfExam { get; set; }

        [Required]
        [Range(2, 6, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Score { get; set; }
    }
}
