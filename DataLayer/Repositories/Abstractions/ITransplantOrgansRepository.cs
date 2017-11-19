using Common.Entities.Organ;

namespace DataLayer.Repositories.Abstractions
{
    public interface ITransplantOrgansRepository : IRepositoryBase<TransplantOrgan>
    {
        TransplantOrgan GetById(int transplantOrganId);
    }
}
