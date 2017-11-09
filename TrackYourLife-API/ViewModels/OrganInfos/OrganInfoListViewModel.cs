using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels.OrganInfos
{
    public class OrganInfoListViewModel
    {
        public ICollection<OrganInfoListItemViewModel> OrganInfoList { get; set; }
    }
}
