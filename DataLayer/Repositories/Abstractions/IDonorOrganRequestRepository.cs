using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.OrganQueries;

namespace DataLayer.Repositories.Abstractions
{
    public interface IDonorOrganRequestRepository
    {
        DonorOrganQuery Save(DonorOrganQuery donorOrganRequest);

        void Update(DonorOrganQuery request);

        DonorOrganQuery GetById(int donorRequestId);
    }
}
