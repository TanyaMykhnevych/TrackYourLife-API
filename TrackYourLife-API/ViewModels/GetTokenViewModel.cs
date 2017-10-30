using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels
{
    public class GetTokenViewModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
