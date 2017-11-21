using Common.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IDonorRequestsRepository : IRepositoryBase<DonorRequest>
    {
        DonorRequest GetById(int donorRequestId);

        DonorRequest GetDetailedById(int donorRequestId);
    }
}
