using System.Collections.Generic;
using Common.Entities.Organ;

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
