namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IMarksService
    {
        Task CreateAsync(string nameOfExam, double score, string studentId, int subjectClassTeacherId);
    }
}
