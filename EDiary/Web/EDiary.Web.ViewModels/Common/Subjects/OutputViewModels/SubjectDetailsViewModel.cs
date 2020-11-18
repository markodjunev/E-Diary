namespace EDiary.Web.ViewModels.Common.Subjects.OutputViewModels
{
    using System.Collections.Generic;

    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;

    public class SubjectDetailsViewModel
    {
        public List<TeacherInSubjectDetailsViewModel> Teachers { get; set; }
    }
}
