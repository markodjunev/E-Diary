namespace EDiary.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface IScheduleSubjectsClassesService
    {
        Task CreateAsync(DateTime startAt, DateTime finishAt, DayOfWeek dayOfWeek, int subjectClassId);

        ScheduleSubjectClass GetById(int id);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAllBySubjectClassId<T>(int id);
    }
}
