using EDiary.Data.Models;
using EDiary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDiary.Web.ViewModels.Teachers.Attendances.OutputViewModels
{
    public class StudentAttendanceViewModel : IMapFrom<Attendance>
    {
        public string StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string StudentEmail { get; set; }

        public bool IsAttended { get; set; }
    }
}
