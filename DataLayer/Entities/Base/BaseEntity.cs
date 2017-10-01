using System;

namespace DataLayer.Entities.Base
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
    }
}
