namespace EDiary.Web.ViewModels.Administration.Users.InputViewModels
{
    using System.ComponentModel.DataAnnotations;

    using EDiary.Data.Models.Enums;

    public class ChangeClassInputModel
    {
        [Required]
        [EnumDataType(typeof(Class))]
        public Class Class { get; set; }

        [Required]
        [EnumDataType(typeof(TypeOfClass))]
        public TypeOfClass TypeOfClass { get; set; }
    }
}
