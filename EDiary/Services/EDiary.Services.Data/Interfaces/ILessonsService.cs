namespace EDiary.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface ILessonsService
    {
        Task CreateAsync(string name, DateTime startAt, DateTime finishAt, int subjectClassId);

        IEnumerable<T> GetAllBySubjectClassId<T>(int subjectClassId);

        Lesson GetLesson(int id);
    }
}
