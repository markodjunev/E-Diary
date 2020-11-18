namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;
    using EDiary.Web.ViewModels.Administration.Users.OutputViewModels;

    public interface IUsersService
    {
        bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber);

        bool IsEmailUsed(string email);

        Task<bool> IsSchoolPrincipalAlreadyAddedAsync(int id);

        Task<List<AvailableSubjectTeacher>> GetAllAvailableSubjectTeachers(int subjectId, int schoolId);
    }
}
