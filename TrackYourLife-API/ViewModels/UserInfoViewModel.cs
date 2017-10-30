using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public int[] Claims { get; set; }
    }
}
