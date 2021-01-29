namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class StudentsParentsService : IStudentsParentsService
    {
        private readonly IDeletableEntityRepository<StudentParent> studentsParentsRepository;

        public StudentsParentsService(IDeletableEntityRepository<StudentParent> studentsParentsRepository)
        {
            this.studentsParentsRepository = studentsParentsRepository;
        }

        public async Task CreateAsync(string studentId, string parentId)
        {
            var studentParent = new StudentParent
            {
                StudentId = studentId,
                ParentId = parentId,
            };

            await this.studentsParentsRepository.AddAsync(studentParent);
            await this.studentsParentsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string studentId, string parentId)
        {
            var studentParent = this.studentsParentsRepository.All().FirstOrDefault(x => x.StudentId == studentId && x.ParentId == parentId);

            this.studentsParentsRepository.Delete(studentParent);
            await this.studentsParentsRepository.SaveChangesAsync();
        }

        public bool Exist(string studentId, string parentId)
        {
            var exist = this.studentsParentsRepository.All().Any(x => x.StudentId == studentId && x.ParentId == parentId);

            return exist;
        }

        public IEnumerable<T> GetAllParentsByStudentId<T>(string studentId)
        {
            IQueryable<StudentParent> parents = this.studentsParentsRepository.All()
                .Where(x => x.StudentId == studentId);

            return parents.To<T>().ToList();
        }
    }
}
