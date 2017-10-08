using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganInfoService
    {
        bool IfOrganInfoExists(int organInfoId);
    }
}
