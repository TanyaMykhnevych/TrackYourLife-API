using Common.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IDonorOrganRequestRepository : IRepositoryBase<DonorOrganQuery>
    {
        DonorOrganQuery GetById(int donorRequestId);
    }
}
