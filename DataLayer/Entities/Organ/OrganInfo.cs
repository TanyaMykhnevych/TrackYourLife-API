using DataLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.Organ
{
    public class OrganInfo : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan OutsideHumanPossibleTime { get; set; }
    }
}
