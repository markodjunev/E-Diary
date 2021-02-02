using System;
using System.Collections.Generic;
using System.Text;

namespace EDiary.Web.ViewModels.Parents.Attendances.OuputViewModels
{
    public class AllStudentsAttendancesByParentViewModel
    {
        public IEnumerable<StudentAttendanceByParentViewModel> Attendances { get; set; }
    }
}
