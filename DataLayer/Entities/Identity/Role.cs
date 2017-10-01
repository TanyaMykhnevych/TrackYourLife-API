using DataLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Identity
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> RoleUsers { get; set; }
    }
}
