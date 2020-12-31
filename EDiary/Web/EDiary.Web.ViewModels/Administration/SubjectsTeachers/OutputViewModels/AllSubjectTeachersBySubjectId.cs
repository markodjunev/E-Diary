namespace EDiary.Web.ViewModels.Administration.SubjectsTeachers.OutputViewModels
{
    using System.Collections.Generic;

    public class AllSubjectTeachersBySubjectId
    {
        public IEnumerable<SubjectTeachersBySubjectId> Teachers { get; set; }
    }
}
