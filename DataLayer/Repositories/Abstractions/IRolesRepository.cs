using DataLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories.Abstractions
{
    public interface IRolesRepository
    {
        Role GetRoleByName(string roleName);
    }
}
