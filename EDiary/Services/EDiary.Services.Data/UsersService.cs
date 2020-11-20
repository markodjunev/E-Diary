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
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;
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

        public async Task<List<AvailableSubjectTeacher>> GetAllAvailableSubjectTeachersAsync(int subjectId, int schoolId)
        {
            var usersInTeacherRole = await this.userManager.GetUsersInRoleAsync(GlobalConstants.TeacherRoleName);
            var teachers = new List<AvailableSubjectTeacher>();

            foreach (var user in usersInTeacherRole.Where(x => x.SchoolId == schoolId))
            {
                var isTeacherAdded = this.subjectsTeachersService.IsTeacherArleadyEnrolledToSubject(subjectId, user.Id);
                if (!isTeacherAdded)
                {
                    var viewModel = new AvailableSubjectTeacher
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.UserName,
                    };

                    teachers.Add(viewModel);
                }
            }

            return teachers;
        }

        public async Task<List<TeacherInSubjectDetailsViewModel>> GetAllTeachersBySubjectIdAsync(int subjectId)
        {
            var subjectTeachers = this.subjectsTeachersService.GetAllSubjectTeachers(subjectId);

            var result = new List<TeacherInSubjectDetailsViewModel>();
            foreach (var subjectTeacher in subjectTeachers)
            {
                var teacher = await this.userManager.FindByIdAsync(subjectTeacher.TeacherId);
                var viewModel = new TeacherInSubjectDetailsViewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Username = teacher.UserName,
                    Email = teacher.Email,
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
            var principals = await this.userManager.GetUsersInRoleAsync(GlobalConstants.PrincipalRoleName);

            if (principals.Where(x => x.SchoolId == id).Count() == 0)
            {
                return false;
            }

            return true;
        }

        public bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.UniqueCitizenshipNumber == uniqueCitizenshipNumber);

            return isUsed;
        }
    }
}
