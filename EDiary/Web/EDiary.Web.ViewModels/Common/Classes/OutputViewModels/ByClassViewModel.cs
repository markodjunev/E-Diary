namespace EDiary.Web.ViewModels.Common.Classes.OutputViewModels
{
    using System.Collections.Generic;

    using EDiary.Web.ViewModels.Common.SubjectsClasses.OutputViewModels;
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;

    public class ByClassViewModel
    {
        public IEnumerable<StudentByClassViewModel> Students { get; set; }

        public IEnumerable<SubjectByClassViewModel> Subjects { get; set; }
    }
}
