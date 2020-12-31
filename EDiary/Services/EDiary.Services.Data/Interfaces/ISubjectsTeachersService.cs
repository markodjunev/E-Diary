namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface ISubjectsTeachersService
    {
        IEnumerable<SubjectTeacher> GetAllSubjectTeachers(int id);

        bool IsTeacherArleadyEnrolledToSubject(int subjectId, string teacherId);

        SubjectTeacher GetSubjectTeacher(int subjectId, string teacherId);

        Task CreateAsync(int subjectId, string teacherId);

        Task DeleteAsync(int subjectId, string teacherId);

        IEnumerable<T> SubjectTeachers<T>(int subjectId);
    }
}
