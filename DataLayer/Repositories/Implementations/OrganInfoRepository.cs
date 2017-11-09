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

        public Task<OrganInfo> GetByIdAsync(int id)
        {
            return _appDbContext.OrganInfos.SingleOrDefaultAsync(c => c.Id == id);
        }

        public bool IfOrganInfoExist(int organInfoId)
        {
            return _appDbContext.OrganInfos.Any(x => x.Id == organInfoId);
        }

        public async Task<OrganInfo> AddAsync(OrganInfo organInfo)
        {
            organInfo.CreatedBy = "DetermUser";
            organInfo.Created = DateTime.UtcNow;

            var entityEntry = await _appDbContext.OrganInfos.AddAsync(organInfo);
            await _appDbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<OrganInfo> UpdateAsync(OrganInfo organInfo)
        {
            var oldEntity = await GetByIdAsync(organInfo.Id);
            _appDbContext.Entry(oldEntity).State = EntityState.Detached;

            organInfo.Created = oldEntity.Created;
            organInfo.CreatedBy = oldEntity.CreatedBy;
            organInfo.Updated = DateTime.UtcNow;
            organInfo.UpdatedBy = "DetermUser";

            var entityEntry = _appDbContext.OrganInfos.Update(organInfo);
            await _appDbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
