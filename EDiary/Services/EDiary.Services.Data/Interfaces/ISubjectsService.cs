namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ISubjectsService
    {
        Task CreateAsync(int schoolId, string name);
    }
}
