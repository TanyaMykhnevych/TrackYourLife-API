using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface IOrganInfoRepository
    {
        bool IfOrganInfoExist(int organInfoId);

        Task<IList<OrganInfo>> GetAllAsync();
    }
}
