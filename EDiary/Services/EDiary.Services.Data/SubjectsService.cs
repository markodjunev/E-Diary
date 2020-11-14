﻿namespace EDiary.Services.Data
{
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;

    public class SubjectsService : ISubjectsService
    {
        private readonly IDeletableEntityRepository<Subject> subjectsRepository;

        public SubjectsService(IDeletableEntityRepository<Subject> subjectsRepository)
        {
            this.subjectsRepository = subjectsRepository;
        }

        public async Task CreateAsync(int schoolId, string name)
        {
            var subject = new Subject
            {
                SchoolId = schoolId,
                Name = name,
            };

            await this.subjectsRepository.AddAsync(subject);
            await this.subjectsRepository.SaveChangesAsync();
        }
    }
}
