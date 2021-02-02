namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface IMarksService
    {
        Task CreateAsync(string nameOfExam, double score, string studentId, int subjectClassTeacherId);

        IEnumerable<T> GetAllByStudentIdAndSubjectClassTeacherId<T>(string studentId, int subjectClassTeacherId);

        Mark GetById(int id);

        Task EditAsync(int markId, string nameOfExam, double score);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAllLatestMarksByStudentId<T>(string studentId);
    }
}
