namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ISchoolsService
    {
        Task<int> CreateAsync(string name, string address, string city, string imageUrl);
    }
}
