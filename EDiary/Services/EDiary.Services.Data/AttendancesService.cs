namespace EDiary.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class AttendancesService : IAttendancesService
    {
        private readonly IDeletableEntityRepository<Attendance> attendancesRepository;

        public AttendancesService(IDeletableEntityRepository<Attendance> attendancesRepository)
        {
            this.attendancesRepository = attendancesRepository;
        }

        public async Task CreateAsync(int lessonId, string studentId, bool isAttended)
        {
            var attendace = new Attendance
            {
                LessonId = lessonId,
                StudentId = studentId,
                IsAttended = isAttended,
            };

            await this.attendancesRepository.AddAsync(attendace);
            await this.attendancesRepository.SaveChangesAsync();
        }

        public bool Exist(int lessonId, string studentId)
        {
            var exist = this.attendancesRepository.All()
                .Any(x => x.LessonId == lessonId && x.StudentId == studentId);

            return exist;
        }

        public IEnumerable<T> GetAllStudentsByLessonId<T>(int lessonId)
        {
            IQueryable<Attendance> attendances = this.attendancesRepository.All()
                .Where(x => x.LessonId == lessonId);

            return attendances.To<T>().ToList();
        }

        public async Task UpdateAsync(int lessonId, string studentId, bool isAttended)
        {
            var attendace = this.attendancesRepository.All().FirstOrDefault(x => x.LessonId == lessonId && x.StudentId == studentId);

            attendace.IsAttended = isAttended;

            this.attendancesRepository.Update(attendace);
            await this.attendancesRepository.SaveChangesAsync();
        }
    }
}
