namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMarksService
    {
        Task CreateAsync(string nameOfExam, double score, string studentId, int subjectClassTeacherId);

        IEnumerable<T> GetAllByStudentIdAndSubjectClassTeacherId<T>(string studentId, int subjectClassTeacherId);
    }
}
