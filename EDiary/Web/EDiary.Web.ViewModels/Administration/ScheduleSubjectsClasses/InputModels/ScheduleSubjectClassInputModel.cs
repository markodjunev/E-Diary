namespace EDiary.Web.ViewModels.Administration.ScheduleSubjectsClasses.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ScheduleSubjectClassInputModel
    {
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime FinishAt { get; set; }

        [Required]
        [EnumDataType(typeof(DayOfWeek))]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
