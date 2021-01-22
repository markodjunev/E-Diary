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

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository)
        {
            this.lessonsRepository = lessonsRepository;
        }

        public async Task CreateAsync(string name, DateTime startAt, DateTime finishAt, int subjectClassId)
        {
            var lesson = new Lesson
            {
                Name = name,
                StartAt = startAt,
                FinishAt = finishAt,
                SubjectClassId = subjectClassId,
            };

            await this.lessonsRepository.AddAsync(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllBySubjectClassId<T>(int subjectClassId)
        {
            IQueryable<Lesson> lessons = this.lessonsRepository.All().Where(x => x.SubjectClassId == subjectClassId);

            return lessons.To<T>().ToList();
        }

        public Lesson GetLesson(int id)
        {
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);

            return lesson;
        }
    }
}
