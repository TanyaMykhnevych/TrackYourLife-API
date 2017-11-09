using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface IOrganInfoRepository
    {
        Task<IList<OrganInfo>> GetAllAsync();

        Task<OrganInfo> GetByIdAsync(int id);

        bool IfOrganInfoExist(int organInfoId);

        Task<OrganInfo> AddAsync(OrganInfo organInfo);

        Task<OrganInfo> UpdateAsync(OrganInfo organInfo);
    }
}
