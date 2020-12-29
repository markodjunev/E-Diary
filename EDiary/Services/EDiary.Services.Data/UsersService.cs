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
    using EDiary.Data.Models.Enums;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;
    using EDiary.Web.ViewModels.Administration.Users.InputViewModels;
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

        public async Task ChangeClassAsync(Class newClass, TypeOfClass typeOfClass, string userId)
        {
            var user = this.GetUserById(userId);

            user.Class = newClass;
            user.TypeOfClass = typeOfClass;
            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task<ApplicationUser> EditAsync(UserEditInputModel editedUser, string id)
        {
            var user = this.GetUserById(id);

            user.FirstName = editedUser.FirstName;
            user.LastName = editedUser.LastName;
            user.Email = editedUser.Email;
            user.NormalizedEmail = editedUser.Email.ToUpper();
            user.Birthday = editedUser.Birthday;
            user.UniqueCitizenshipNumber = editedUser.UniqueCitizenshipNumber;
            user.UserName = editedUser.UniqueCitizenshipNumber;
            user.NormalizedUserName = editedUser.UniqueCitizenshipNumber;

            await this.userManager.RemovePasswordAsync(user);
            await this.userManager.AddPasswordAsync(user, editedUser.UniqueCitizenshipNumber);
            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();

            return user;
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

        public IEnumerable<T> GetAllStudentsByClass<T>(int schoolId, Class @class, TypeOfClass typeOfClass)
        {
            IQueryable<ApplicationUser> students = this.usersRepository.All()
                .Where(x => x.SchoolId == schoolId && x.Class == @class && x.TypeOfClass == typeOfClass)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            return students.To<T>().ToList();
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

        public ApplicationUser GetUserById(string id)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            return user;
        }

        public bool IsEmailUsed(string email)
        {
            var isUsed = this.usersRepository.AllWithDeleted().Any(x => x.Email == email);

            return isUsed;
        }

        public bool IsEmailVaildInEdit(string email, string userId)
        {
            var user = this.GetUserById(userId);
            if (user.Email == email)
            {
                return true;
            }

            var users = this.usersRepository.All().Where(x => x.Email == email);
            if (users.Count() == 0)
            {
                return true;
            }

            return false;
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

        public bool IsUniqueCitizenshipNumberVaildInEdit(string uniqueCitizenshipNumber, string userId)
        {
            var user = this.GetUserById(userId);
            if (user.UniqueCitizenshipNumber == uniqueCitizenshipNumber)
            {
                return true;
            }

            var users = this.usersRepository.All().Where(x => x.UniqueCitizenshipNumber == uniqueCitizenshipNumber);
            if (users.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}
