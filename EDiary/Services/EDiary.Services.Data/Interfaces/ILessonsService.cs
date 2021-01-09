namespace EDiary.Services.Data.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface ILessonsService
    {
        Task CreateAsync(string name, DateTime startAt, DateTime finishAt, int subjectClassId);
    }
}
