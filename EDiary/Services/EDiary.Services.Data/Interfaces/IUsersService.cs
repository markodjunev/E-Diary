namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;

    using EDiary.Data.Models;

    public interface IUsersService
    {
        bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber);

        bool IsEmailUsed(string email);

        IEnumerable<ApplicationUser> GetStudentsBySchoolId(int id);
    }
}
