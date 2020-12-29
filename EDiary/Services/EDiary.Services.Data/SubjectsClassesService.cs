namespace EDiary.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class SubjectsClassesService : ISubjectsClassesService
    {
        private readonly IDeletableEntityRepository<SubjectClass> subjectsClassesRepository;

        public SubjectsClassesService(IDeletableEntityRepository<SubjectClass> subjectsClassesRepository)
        {
            this.subjectsClassesRepository = subjectsClassesRepository;
        }

        public async Task Create(Class @class, TypeOfClass typeOfClass, int subjectId, int schoolId)
        {
            var subjectClass = new SubjectClass
            {
                Class = @class,
                TypeOfClass = typeOfClass,
                SubjectId = subjectId,
                SchoolId = schoolId,
            };

            await this.subjectsClassesRepository.AddAsync(subjectClass);
            await this.subjectsClassesRepository.SaveChangesAsync();
        }

        public bool Exist(Class @class, TypeOfClass typeOfClass, int subjectId, int schoolId)
        {
            var subjectClass = this.subjectsClassesRepository.All().FirstOrDefault(
                x => x.Class == @class &&
                x.TypeOfClass == typeOfClass
                && x.SubjectId == subjectId
                && x.SchoolId == schoolId);

            if (subjectClass == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<T> GetAllByClasses<T>(int schoolId, Class @class, TypeOfClass typeOfClass)
        {
            IQueryable<SubjectClass> subjectClasses = this.subjectsClassesRepository.All()
                .Where(x => x.SchoolId == schoolId && x.Class == @class && x.TypeOfClass == typeOfClass);

            return subjectClasses.To<T>().ToList();
        }

        public SubjectClass GetById(int id)
        {
            var subjectClass = this.subjectsClassesRepository.All().FirstOrDefault(x => x.Id == id);

            return subjectClass;
        }

        public async Task Remove(int id)
        {
            var subjectClass = this.GetById(id);

            this.subjectsClassesRepository.Delete(subjectClass);
            await this.subjectsClassesRepository.SaveChangesAsync();
        }
    }
}
