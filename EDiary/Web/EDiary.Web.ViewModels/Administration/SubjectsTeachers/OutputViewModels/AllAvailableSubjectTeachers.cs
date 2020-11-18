namespace EDiary.Web.ViewModels.Administration.SubjectsTeachers.OutputViewModels
{
    using System.Collections.Generic;

    using EDiary.Web.ViewModels.Administration.Schools.OutputViewModels;
    using EDiary.Web.ViewModels.Administration.Users.OutputViewModels;

    public class AllAvailableSubjectTeachers
    {
        public List<AvailableSubjectTeacher> Teachers { get; set; }

        public SchoolInAvailableSubjectTeachers School { get; set; }
    }
}
