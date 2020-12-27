namespace EDiary.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IStudentsParentsService
    {
        bool Exist(string studentId, string parentId);

        Task CreateAsync(string studentId, string parentId);

        Task DeleteAsync(string studentId, string parentId);
    }
}
