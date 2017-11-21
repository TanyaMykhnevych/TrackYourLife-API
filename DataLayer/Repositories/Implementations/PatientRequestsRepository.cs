using DataLayer.Repositories.Abstractions;
using DataLayer.DbContext;
using System.Collections.Generic;
using Common.Entities.OrganRequests;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class PatientRequestsRepository : RepositoryBase<PatientRequest>, IPatientRequestsRepository
    {
        public PatientRequestsRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.PatientRequests)
        {
        }

        public IList<PatientRequest> GetAllPending()
        {
            return GetAll(x => x.Status == PatientRequestStatuses.AwaitingForDonor);
        }

        public IList<PatientRequest> GetPendingByOrganInfo(int organInfoId)
        {
            //TODO: use valud from enum
            return GetAll(x => x.OrganInfoId == organInfoId && x.Status == PatientRequestStatuses.AwaitingForDonor);
        }

        public PatientRequest GetById(int patientOrganQueryId)
        {
            return GetSingleByPredicate(x => x.Id == patientOrganQueryId, x => x.Include(e => e.DonorRequest));
        }
    }
}
