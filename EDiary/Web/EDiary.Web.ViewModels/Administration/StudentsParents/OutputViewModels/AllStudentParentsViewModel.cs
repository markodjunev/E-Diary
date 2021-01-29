namespace EDiary.Web.ViewModels.Administration.StudentsParents.OutputViewModels
{
    using System.Collections.Generic;

    public class AllStudentParentsViewModel
    {
        public IEnumerable<StudentParentsViewModel> Parents { get; set; }
    }
}
