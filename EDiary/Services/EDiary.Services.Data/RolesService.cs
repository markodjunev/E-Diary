namespace EDiary.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using EDiary.Data.Common.Repositories;
    using EDiary.Data.Models;
    using EDiary.Services.Data.Interfaces;
    using EDiary.Services.Mapping;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<ApplicationRole> roles = this.rolesRepository.All();

            return roles.To<T>().ToList();
        }

        public ApplicationRole GetRoleById(string id)
        {
            var role = this.rolesRepository.All().FirstOrDefault(x => x.Id == id);

            return role;
        }
    }
}
