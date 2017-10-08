using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Identity;
using DataLayer.DbContext;
using System.Linq;

namespace DataLayer.Repositories.Implementations
{
    public class RolesRepository : IRolesRepository
    {
        private readonly AppDbContext _appDbContext;

        public RolesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Role GetRoleByName(string roleName)
        {
            return _appDbContext.Roles.SingleOrDefault(x => x.Name == roleName);
        }
    }
}
