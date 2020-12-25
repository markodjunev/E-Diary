namespace EDiary.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;

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
