namespace EDiary.Web.ViewModels.Common.Subjects.OutputViewModels
{
    using System.Collections.Generic;

    public class AllSubjectsViewModel
    {
        public IEnumerable<SubjectsViewModel> Subjects { get; set; }
    }
}
