namespace EDiary.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class SubjectsClassesTeachersService : ISubjectsClassesTeachersService
    {
        private readonly IDeletableEntityRepository<SubjectClassTeacher> subjectsClassesTeachersRepository;

        public SubjectsClassesTeachersService(IDeletableEntityRepository<SubjectClassTeacher> subjectsClassesTeachersRepository)
        {
            this.subjectsClassesTeachersRepository = subjectsClassesTeachersRepository;
        }

        public async Task CreateAsync(int subjectClassId, string teacherId)
        {
            var subjectClassTeacher = new SubjectClassTeacher
            {
                SubjectClassId = subjectClassId,
                TeacherId = teacherId,
            };

            await this.subjectsClassesTeachersRepository.AddAsync(subjectClassTeacher);
            await this.subjectsClassesTeachersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int subjectClassId, string teacherId)
        {
            var subjectClassTeacher = this.subjectsClassesTeachersRepository.All().FirstOrDefault(x => x.SubjectClassId == subjectClassId && x.TeacherId == teacherId);

            this.subjectsClassesTeachersRepository.Delete(subjectClassTeacher);
            await this.subjectsClassesTeachersRepository.SaveChangesAsync();
        }

        public bool Exist(int subjectClassId, string teacherId)
        {
            var exist = this.subjectsClassesTeachersRepository.All().Any(x => x.SubjectClassId == subjectClassId && x.TeacherId == teacherId);

            return exist;
        }

        public IEnumerable<T> GetAllBySubjectClassId<T>(int subjectClassId)
        {
            IQueryable<SubjectClassTeacher> subjectClassTeachers = this.subjectsClassesTeachersRepository.All()
                .Where(x => x.SubjectClassId == subjectClassId);

            return subjectClassTeachers.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByTeacherId<T>(string teacherId)
        {
            IQueryable<SubjectClassTeacher> subjectClassTeachers = this.subjectsClassesTeachersRepository.All()
                .Where(x => x.TeacherId == teacherId);

            return subjectClassTeachers.To<T>().ToList();
        }

        public SubjectClassTeacher GetById(int id)
        {
            var subjectClassTeacher = this.subjectsClassesTeachersRepository.All().FirstOrDefault(x => x.Id == id);

            return subjectClassTeacher;
        }

        public SubjectClassTeacher GetBySubjectClassIdAndTeacherId(int subjectClassId, string teacherId)
        {
            var subjectClassTeacher = this.subjectsClassesTeachersRepository.All()
                .FirstOrDefault(x => x.SubjectClassId == subjectClassId && x.TeacherId == teacherId);

            return subjectClassTeacher;
        }
    }
}
