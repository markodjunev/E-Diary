namespace EDiary.Web.ViewModels.Teachers.Lessons.OutputViewModels
{
    using System;

    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class TeacherLessonViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime FinishAt { get; set; }
    }
}
