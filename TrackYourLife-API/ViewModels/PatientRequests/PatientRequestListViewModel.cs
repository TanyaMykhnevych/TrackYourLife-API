using System.Collections.Generic;

namespace TrackYourLife.API.ViewModels.PatientRequests
{
    public class PatientRequestListViewModel
    {
        public ICollection<PatientRequestListItemViewModel> PatientRequestList { get; set; }

        public PatientRequestListViewModel(ICollection<PatientRequestListItemViewModel> list)
        {
            PatientRequestList = list;
        }

        public PatientRequestListViewModel() { }
    }
}
