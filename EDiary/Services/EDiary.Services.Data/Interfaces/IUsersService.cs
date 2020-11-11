namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        bool IsUniqueCitizenshipNumberUsed(string uniqueCitizenshipNumber);

        bool IsEmailUsed(string email);

        Task<bool> IsSchoolPrincipalAlreadyAddedAsync(int id);
    }
}
