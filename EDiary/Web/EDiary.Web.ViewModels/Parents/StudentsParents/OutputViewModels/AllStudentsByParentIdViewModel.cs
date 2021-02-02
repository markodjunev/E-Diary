namespace EDiary.Web.ViewModels.Parents.StudentsParents.OutputViewModels
{
    using System.Collections.Generic;

    public class AllStudentsByParentIdViewModel
    {
        public IEnumerable<StudentByParentIdViewModel> Students { get; set; }
    }
}
