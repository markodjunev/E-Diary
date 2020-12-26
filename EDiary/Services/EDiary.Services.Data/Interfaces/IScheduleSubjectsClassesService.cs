namespace EDiary.Services.Data.Interfaces
{
    using EDiary.Data.Models;
    using System;
    using System.Threading.Tasks;

    public interface IScheduleSubjectsClassesService
    {
        Task CreateAsync(DateTime startAt, DateTime finishAt, DayOfWeek dayOfWeek, int subjectClassId);

        ScheduleSubjectClass GetById(int id);

        Task DeleteAsync(int id);
    }
}
