namespace EDiary.Web.ViewModels.Teachers.Lessons.OutputViewModels
{
    using System.Collections.Generic;

    public class AllTeacherLessonStudentDetailsViewModel
    {
        public IEnumerable<TeacherLessonStudentDetailsViewModel> Students { get; set; }
    }
}
