namespace EDiary.Web.ViewModels.Administration.SubjectsClasses.InputViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using EDiary.Data.Models.Enums;
    using EDiary.Web.ViewModels.Administration.Subjects.InputModels;

    public class SubjectClassInputModel
    {
        [Required]
        [EnumDataType(typeof(Class))]
        public Class Class { get; set; }

        [Required]
        [EnumDataType(typeof(TypeOfClass))]
        public TypeOfClass TypeOfClass { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public IEnumerable<SubjectsToClassDropDownViewModel> Subjects { get; set; }

    }
}
