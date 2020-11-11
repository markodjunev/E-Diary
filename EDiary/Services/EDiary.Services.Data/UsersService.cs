namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EDiary.Common;
    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, UserManager<ApplicationUser> userManager)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
        }

        public bool IsEmailUsed(string email)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.Email == email);

            return isUsed;
        }

        public async Task<bool> IsSchoolPrincipalAlreadyAddedAsync(int id)
        {
            var users = this.usersRepository.All().Where(x => x.SchoolId == id);

            foreach (var user in users)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    if (userRole == GlobalConstants.PrincipalRoleName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.UniqueCitizenshipNumber == uniqueCitizenshipNumber);

            return isUsed;
        }
    }
}
