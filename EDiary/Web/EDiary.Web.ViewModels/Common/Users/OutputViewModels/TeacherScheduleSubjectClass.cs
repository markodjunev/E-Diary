﻿namespace EDiary.Web.ViewModels.Common.Users.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class TeacherScheduleSubjectClass : IMapFrom<SubjectClassTeacher>
    {
        public string TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherEmail { get; set; }
    }
}
