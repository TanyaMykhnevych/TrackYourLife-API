using DataLayer.Entities.Organ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganInfoService
    {
        Task<IList<OrganInfo>> GetOrganInfosAsync();

        Task<OrganInfo> GetOrganInfoByIdAsync(int id);

        Task<OrganInfo> AddOrganInfoAsync(OrganInfo organInfo);

        Task<OrganInfo> UpdateOrganInfoAsync(OrganInfo organInfo);

        bool IfOrganInfoExists(int organInfoId);
    }
}
