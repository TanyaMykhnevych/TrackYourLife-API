using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories.Implementations
{
    public class OrganInfoRepository : IOrganInfoRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrganInfoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool IfOrganInfoExist(int organInfoId)
        {
            return _appDbContext.OrganInfos.Any(x => x.Id == organInfoId);
        }
    }
}
