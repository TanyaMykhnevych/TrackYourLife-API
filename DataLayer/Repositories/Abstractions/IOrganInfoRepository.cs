using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories.Abstractions
{
    public interface IOrganInfoRepository
    {
        bool IfOrganInfoExist(int organInfoId);
    }
}
