using System;
using Common.Entities.Base;

namespace Common.Entities.Organ
{
    public class OrganInfo : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan OutsideHumanPossibleTime { get; set; }
    }
}
