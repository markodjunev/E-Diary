namespace EDiary.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;

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
    }
}
