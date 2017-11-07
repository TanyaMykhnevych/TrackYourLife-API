using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Entities.Organ;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class OrganInfoRepository : IOrganInfoRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrganInfoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<OrganInfo>> GetAllAsync()
        {
            return await _appDbContext.OrganInfos.ToListAsync();
        }

        public bool IfOrganInfoExist(int organInfoId)
        {
            return _appDbContext.OrganInfos.Any(x => x.Id == organInfoId);
        }
    }
}
