﻿using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System.Linq;
using Common.Entities.Organ;

namespace DataLayer.Repositories.Implementations
{
    public class OrganInfoRepository : RepositoryBase<OrganInfo>, IOrganInfoRepository
    {
        public OrganInfoRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.OrganInfos)
        {
        }

        public OrganInfo GetById(int id)
        {
            return GetSingleByPredicate(c => c.Id == id);
        }

        public bool IfOrganInfoExist(int organInfoId)
        {
            return Any(x => x.Id == organInfoId);
        }
    }
}
