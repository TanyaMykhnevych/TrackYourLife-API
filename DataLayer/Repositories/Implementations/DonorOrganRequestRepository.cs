﻿using DataLayer.DbContext;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;
using System;
using System.Linq;

namespace DataLayer.Repositories.Implementations
{
    public class DonorOrganRequestRepository : IDonorOrganRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public DonorOrganRequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DonorOrganQuery GetById(int donorRequestId)
        {
            return _appDbContext.DonorOrganQueries.SingleOrDefault(x => x.Id == donorRequestId);
        }

        public DonorOrganQuery Save(DonorOrganQuery donorOrganRequest)
        {
            donorOrganRequest.Created = DateTime.UtcNow;
            donorOrganRequest.CreatedBy = "Default";

            donorOrganRequest = _appDbContext.DonorOrganQueries.Add(donorOrganRequest).Entity;
            _appDbContext.SaveChanges();

            return donorOrganRequest;
        }

        public void Update(DonorOrganQuery donorOrganRequest)
        {
            donorOrganRequest.Updated = DateTime.UtcNow;
            donorOrganRequest.UpdatedBy = "Default";

            _appDbContext.DonorOrganQueries.Update(donorOrganRequest);
            _appDbContext.SaveChanges();
        }
    }
}