namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface ISubjectsClassesTeachersService
    {
        Task CreateAsync(int subjectClassId, string teacherId);

        Task DeleteAsync(int subjectClassId, string teacherId);

        SubjectClassTeacher GetById(int id);

        bool Exist(int subjectClassId, string teacherId);

        IEnumerable<T> GetAllBySubjectClassId<T>(int subjectClassId);

        IEnumerable<T> GetAllByTeacherId<T>(string teacherId);
    }
}
