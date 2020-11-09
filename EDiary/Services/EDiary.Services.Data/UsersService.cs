namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<ApplicationUser> GetStudentsBySchoolId(int id)
        {
            var users = this.usersRepository.AllWithDeleted().Where(x => x.SchoolId == id);

            return users;
        }

        public bool IsEmailUsed(string email)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.Email == email);

            return isUsed;
        }

        public bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.UniqueCitizenshipNumber == uniqueCitizenshipNumber);

            return isUsed;
        }
    }
}
