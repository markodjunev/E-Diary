namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubjectsService
    {
        Task CreateAsync(int schoolId, string name);

        IEnumerable<T> GetAll<T>(int schoolId);
    }
}
