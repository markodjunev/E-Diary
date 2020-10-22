namespace EDiary.Web.ViewModels.Administration.Schools.OutputViewModels
{
    using System.Collections.Generic;

    public class AllSchoolsViewModel
    {
        public IEnumerable<SchoolsViewModel> Schools { get; set; }
    }
}
