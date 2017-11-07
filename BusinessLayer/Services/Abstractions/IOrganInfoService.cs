using DataLayer.Entities.Organ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganInfoService
    {
        bool IfOrganInfoExists(int organInfoId);

        Task<IList<OrganInfo>> GetOrganInfosAsync();
    }
}
