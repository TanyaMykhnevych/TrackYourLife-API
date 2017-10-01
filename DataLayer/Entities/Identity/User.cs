using DataLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Identity
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
        
        public virtual UserInfo UserInfo { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
