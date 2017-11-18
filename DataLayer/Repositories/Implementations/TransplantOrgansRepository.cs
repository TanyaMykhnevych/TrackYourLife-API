using DataLayer.Repositories.Abstractions;
using System;
using DataLayer.Entities.Organ;
using DataLayer.DbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class TransplantOrgansRepository : RepositoryBase<TransplantOrgan>, ITransplantOrgansRepository
    {
        public TransplantOrgansRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.TransplantOrgans)
        {
        }

        public TransplantOrgan GetById(int transplantOrganId)
        {
            return GetSingleByPredicate(x => x.Id == transplantOrganId);
        }
    }
}
