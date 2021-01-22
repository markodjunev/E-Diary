namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAttendancesService
    {
        bool Exist(int lessonId, string studentId);

        Task UpdateAsync(int lessonId, string studentId, bool isAttended);

        Task CreateAsync(int lessonId, string studentId, bool isAttended);

        IEnumerable<T> GetAllStudentsByLessonId<T>(int lessonId);
    }
}
