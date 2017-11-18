using DataLayer.Entities.Organ;
using System.Collections.Generic;

namespace BusinessLayer.Services.Abstractions
{
    public interface IOrganInfoService
    {
        IList<OrganInfo> GetOrganInfos();

        OrganInfo GetOrganInfoById(int id);

        OrganInfo AddOrganInfo(OrganInfo organInfo);

        OrganInfo UpdateOrganInfo(OrganInfo organInfo);

        bool IfOrganInfoExists(int organInfoId);
    }
}
