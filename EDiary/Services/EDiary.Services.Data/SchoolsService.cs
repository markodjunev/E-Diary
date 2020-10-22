namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class SchoolsService : ISchoolsService
    {
        private readonly IDeletableEntityRepository<School> schoolsRepository;

        public SchoolsService(IDeletableEntityRepository<School> schoolsRepository)
        {
            this.schoolsRepository = schoolsRepository;
        }

        public async Task<int> CreateAsync(string name, string address, string city, string imageUrl)
        {
            var school = new School
            {
                Name = name,
                Address = address,
                City = city,
                ImageUrl = imageUrl,
            };

            await this.schoolsRepository.AddAsync(school);
            await this.schoolsRepository.SaveChangesAsync();

            return school.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<School> schools = this.schoolsRepository.All();

            return schools.To<T>().ToList();
        }
    }
}
