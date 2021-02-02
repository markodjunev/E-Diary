namespace EDiary.Web.ViewModels.Parents.Attendances.OuputViewModels
{
    using System;

    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class StudentAttendanceByParentViewModel : IMapFrom<Attendance>
    {
        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public bool IsAttended { get; set; }

        public string LessonName { get; set; }

        public DateTime LessonStartAt { get; set; }

        public string LessonSubjectClassSubjectName { get; set; }
    }
}
