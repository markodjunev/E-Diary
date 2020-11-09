namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EDiary.Data.Models;

    public interface ISchoolsService
    {
        Task<int> CreateAsync(string name, string address, string city, string imageUrl);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        School GetSchool(int id);
    }
}
