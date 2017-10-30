using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels.Common
{
    public class ResponseWrapper<T>
    {
        public T Content { get; set; }

        public string Error { get; set; }
    }
}
