namespace EDiary.Web.ViewModels.Teachers.Lessons.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LessonCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FinishAt { get; set; }
    }
}
