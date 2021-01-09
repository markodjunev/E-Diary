namespace EDiary.Web.ViewModels.Teachers.Lessons.OutputViewModels
{
    using System.Collections.Generic;

    public class AllTeacherLessonsViewModel
    {
        public IEnumerable<TeacherLessonViewModel> Lessons { get; set; }
    }
}
