using DataLayer.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface IOrganInfoRepository : IRepositoryBase<OrganInfo>
    {
        OrganInfo GetById(int id);

        bool IfOrganInfoExist(int organInfoId);
    }
}
