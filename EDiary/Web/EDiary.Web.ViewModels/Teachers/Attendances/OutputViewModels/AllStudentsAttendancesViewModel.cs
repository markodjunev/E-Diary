namespace EDiary.Web.ViewModels.Teachers.Attendances.OutputViewModels
{
    using System.Collections.Generic;

    public class AllStudentsAttendancesViewModel
    {
        public IEnumerable<StudentAttendanceViewModel> Students { get; set; }
    }
}
