using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories.Implementations
{
    public class OrganDeliveryRepository : IOrganDeliveryRepository
    {
        private readonly AppDbContext _dbContext;

        public OrganDeliveryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
