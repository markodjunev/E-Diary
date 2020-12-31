namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class SubjectsTeachersService : ISubjectsTeachersService
    {
        private readonly IDeletableEntityRepository<SubjectTeacher> subjectsTeachersRepository;

        public SubjectsTeachersService(IDeletableEntityRepository<SubjectTeacher> subjectsTeachersRepository)
        {
            this.subjectsTeachersRepository = subjectsTeachersRepository;
        }

        public async Task CreateAsync(int subjectId, string teacherId)
        {
            var subjectTeacher = new SubjectTeacher
            {
                SubjectId = subjectId,
                TeacherId = teacherId,
            };

            await this.subjectsTeachersRepository.AddAsync(subjectTeacher);
            await this.subjectsTeachersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int subjectId, string teacherId)
        {
            var subjectTeacher = this.subjectsTeachersRepository.All().FirstOrDefault(x => x.SubjectId == subjectId && x.TeacherId == teacherId);

            this.subjectsTeachersRepository.Delete(subjectTeacher);
            await this.subjectsTeachersRepository.SaveChangesAsync();
        }

        public IEnumerable<SubjectTeacher> GetAllSubjectTeachers(int id)
        {
            var subjectTeachers = this.subjectsTeachersRepository.All().Where(x => x.SubjectId == id);

            return subjectTeachers;
        }

        public SubjectTeacher GetSubjectTeacher(int subjectId, string teacherId)
        {
            var subjectTeacher = this.subjectsTeachersRepository.All().FirstOrDefault(x => x.SubjectId == subjectId && x.TeacherId == teacherId);

            return subjectTeacher;
        }

        public bool IsTeacherArleadyEnrolledToSubject(int subjectId, string teacherId)
        {
            var subjectTeachers = this.subjectsTeachersRepository.All().Where(x => x.SubjectId == subjectId);

            var isTeacherAdded = subjectTeachers.Any(x => x.TeacherId == teacherId);

            return isTeacherAdded;
        }

        public IEnumerable<T> SubjectTeachers<T>(int subjectId)
        {
            IQueryable<SubjectTeacher> subjectTeachers = this.subjectsTeachersRepository.All().Where(x => x.SubjectId == subjectId);

            return subjectTeachers.To<T>().ToList();
        }
    }
}
