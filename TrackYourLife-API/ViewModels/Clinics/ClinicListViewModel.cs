using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels.Clinics
{
    public class ClinicListViewModel
    {
        public ICollection<ClinicListItemViewModel> Clinics { get; set; }
    }
}
