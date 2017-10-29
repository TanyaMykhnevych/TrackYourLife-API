using System;
using DataLayer.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities.Identity
{
    public class AppUser : IdentityUser, IBaseEntity
    {        
        public virtual UserInfo UserInfo { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
    }
}
