namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Web.ViewModels.Administration.Users.OutputViewModels;
    using EDiary.Web.ViewModels.Common.Users.OutputViewModels;

    public interface IUsersService
    {
        bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber);

        bool IsEmailUsed(string email);

        Task<bool> IsSchoolPrincipalAlreadyAddedAsync(int id);

        Task<List<AvailableSubjectTeacher>> GetAllAvailableSubjectTeachersAsync(int subjectId, int schoolId);

        Task<List<TeacherInSubjectDetailsViewModel>> GetAllTeachersBySubjectIdAsync(int subjectId);
    }
}
