namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentsParentsService
    {
        bool Exist(string studentId, string parentId);

        Task CreateAsync(string studentId, string parentId);

        Task DeleteAsync(string studentId, string parentId);

        IEnumerable<T> GetAllParentsByStudentId<T>(string studentId);

        IEnumerable<T> GetAllStudentsByParentId<T>(string parentId);
    }
}
