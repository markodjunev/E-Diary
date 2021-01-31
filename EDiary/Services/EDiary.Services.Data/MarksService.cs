namespace EDiary.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class MarksService : IMarksService
    {
        private readonly IDeletableEntityRepository<Mark> marksRepository;

        public MarksService(IDeletableEntityRepository<Mark> marksRepository)
        {
            this.marksRepository = marksRepository;
        }

        public async Task CreateAsync(string nameOfExam, double score, string studentId, int subjectClassTeacherId)
        {
            var mark = new Mark
            {
                NameOfExam = nameOfExam,
                Score = score,
                StudentId = studentId,
                SubjectClassTeacherId = subjectClassTeacherId,
            };

            await this.marksRepository.AddAsync(mark);
            await this.marksRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByStudentIdAndSubjectClassTeacherId<T>(string studentId, int subjectClassTeacherId)
        {
            IQueryable<Mark> marks = this.marksRepository.All()
                .Where(x => x.StudentId == studentId && x.SubjectClassTeacherId == subjectClassTeacherId);

            return marks.To<T>().ToList();
        }
    }
}
