namespace EDiary.Services.Data.Interfaces
{
    using System.Collections.Generic;

    using EDiary.Data.Models;

    public interface IRolesService
    {
        IEnumerable<T> GetAll<T>();

        ApplicationRole GetRoleById(string id);
    }
}
