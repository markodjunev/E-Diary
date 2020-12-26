namespace EDiary.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;

    public class ScheduleSubjectsClassesService : IScheduleSubjectsClassesService
    {
        private readonly IDeletableEntityRepository<ScheduleSubjectClass> scheduleSubjectsClassesRepository;

        public ScheduleSubjectsClassesService(IDeletableEntityRepository<ScheduleSubjectClass> scheduleSubjectsClassesRepository)
        {
            this.scheduleSubjectsClassesRepository = scheduleSubjectsClassesRepository;
        }

        public async Task CreateAsync(DateTime startAt, DateTime finishAt, DayOfWeek dayOfWeek, int subjectClassId)
        {
            var scheduleSubjectClass = new ScheduleSubjectClass
            {
                StartAt = startAt,
                FinishAt = finishAt,
                DayOfWeek = dayOfWeek,
                SubjectClassId = subjectClassId,
            };

            await this.scheduleSubjectsClassesRepository.AddAsync(scheduleSubjectClass);
            await this.scheduleSubjectsClassesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var scheduleSubjectClass = this.GetById(id);

            this.scheduleSubjectsClassesRepository.Delete(scheduleSubjectClass);
            await this.scheduleSubjectsClassesRepository.SaveChangesAsync();
        }

        public ScheduleSubjectClass GetById(int id)
        {
            var scheduleSubjectClass = this.scheduleSubjectsClassesRepository.All().FirstOrDefault(x => x.Id == id);

            return scheduleSubjectClass;
        }
    }
}
