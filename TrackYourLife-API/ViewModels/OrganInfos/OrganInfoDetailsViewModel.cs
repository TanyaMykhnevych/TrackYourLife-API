using DataLayer.Entities;
using DataLayer.Entities.Organ;
using System;

namespace TrackYourLife.API.ViewModels.OrganInfos
{
    public class OrganInfoDetailsViewModel
    {
        public OrganInfoDetailsViewModel(OrganInfo organInfo)
        {
            Id = organInfo.Id;
            Name = organInfo.Name;
            Description = organInfo.Description;
            OutsideHumanPossibleTime = organInfo.OutsideHumanPossibleTime;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan OutsideHumanPossibleTime { get; set; }
    }
}
