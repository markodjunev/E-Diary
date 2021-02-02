namespace EDiary.Web.ViewModels.Parents.StudentsParents.OutputViewModels
{
    using System;

    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Mapping;

    public class StudentByParentIdViewModel : IMapFrom<StudentParent>
    {
        public string StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public Class StudentClass { get; set; }

        public TypeOfClass StudentTypeOfClass { get; set; }

        public DateTime StudentBirthday { get; set; }
    }
}
