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
    using EDiary.Web.ViewModels.Administration.Users.OutputViewModels;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsTeachersService subjectsTeachersService;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, UserManager<ApplicationUser> userManager, ISubjectsTeachersService subjectsTeachersService)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.subjectsTeachersService = subjectsTeachersService;
        }

        public async Task<List<AvailableSubjectTeacher>> GetAllAvailableSubjectTeachers(int subjectId, int schoolId)
        {
            var subjectTeachers = this.subjectsTeachersService.GetAllSubjectTeachers(subjectId);

            var usersInTeacherRole = await this.userManager.GetUsersInRoleAsync(GlobalConstants.TeacherRoleName);
            var teachers = new List<ApplicationUser>();

            foreach (var user in usersInTeacherRole)
            {
                if (user.SchoolId == schoolId)
                {
                    var isTeacherAdded = this.subjectsTeachersService.IsTeacherArleadyEnrolledToSubject(subjectId, user.Id);
                    if (!isTeacherAdded)
                    {
                        teachers.Add(user);
                    }
                }
            }

            var result = new List<AvailableSubjectTeacher>();

            foreach (var teacher in teachers)
            {
                var viewModel = new AvailableSubjectTeacher
                {
                    Id = teacher.Id,
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Username = teacher.UserName,
                };

                result.Add(viewModel);
            }

            return result;
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
