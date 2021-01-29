namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Data.Models.Enums;
    using EDiary.Web.ViewModels.Administration.StudentsParents.OutputViewModels;
    using EDiary.Web.ViewModels.Administration.Users.InputViewModels;
    using EDiary.Web.ViewModels.Administration.Users.OutputViewModels;
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;

    public interface IUsersService
    {
        bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber);

        bool IsEmailUsed(string email);

        Task<bool> IsSchoolPrincipalAlreadyAddedAsync(int id);

        Task<List<AvailableSubjectTeacher>> GetAllAvailableSubjectTeachersAsync(int subjectId, int schoolId);

        Task<List<TeacherInSubjectDetailsViewModel>> GetAllTeachersBySubjectIdAsync(int subjectId);

        ApplicationUser GetUserById(string id);

        bool IsEmailVaildInEdit(string email, string userId);

        bool IsUniqueCitizenshipNumberVaildInEdit(string uniqueCitizenshipNumber, string userId);

        Task<ApplicationUser> EditAsync(UserEditInputModel editedUser, string id);

        Task ChangeClassAsync(Class newClass, TypeOfClass typeOfClass, string userId);

        IEnumerable<T> GetAllStudentsByClass<T>(int schoolId, Class @class, TypeOfClass typeOfClass);

        Task<List<ChooseParentViewModel>> GetAllParents();
    }
}
