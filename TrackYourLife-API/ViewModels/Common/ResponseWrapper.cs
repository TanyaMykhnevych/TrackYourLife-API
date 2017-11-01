using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourLife.API.ViewModels.Common
{
    public class ResponseWrapper<T> : ResponseWrapper
    {
        public T Content { get; set; }
    }

    public class ResponseWrapper
    {
        public string ErrorMessage { get; set; }

        public bool IsValid => string.IsNullOrEmpty(ErrorMessage);
    }
}
