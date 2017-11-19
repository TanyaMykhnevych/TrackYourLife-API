using System.Collections.Generic;

namespace TrackYourLife.API.ViewModels.DonorRequests
{
    public class DonorRequestListViewModel
    {
        public ICollection<DonorRequestListItemViewModel> DonorRequestList { get; set; }

        public DonorRequestListViewModel(ICollection<DonorRequestListItemViewModel> list)
        {
            DonorRequestList = list;
        }

        public DonorRequestListViewModel() { }
    }
}
