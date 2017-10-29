using System;

namespace DataLayer.Entities.Base
{
    public interface IBaseEntity
    {
        string CreatedBy { get; set; }
        DateTime Created { get; set; }

        string UpdatedBy { get; set; }
        DateTime? Updated { get; set; }
    }
}
