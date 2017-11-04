using DataLayer.Repositories.Abstractions;
using DataLayer.Entities;
using DataLayer.DbContext;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer.Repositories.Implementations
{
    public class ClinicsRepository : IClinicsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClinicsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<Clinic>> GetAllAsync()
        {
            return await _appDbContext.Clinics
                .ToListAsync();
        }

        public Task<Clinic> GetByIdAsync(int id)
        {
            return _appDbContext.Clinics.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Clinic GetFirst()
        {
            return _appDbContext.Clinics.FirstOrDefault();
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            clinic.CreatedBy = "DetermUser";
            clinic.Created = DateTime.UtcNow;

            var entityEntry = await _appDbContext.Clinics.AddAsync(clinic);
            await _appDbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Clinic> UpdateAsync(Clinic clinic)
        {
            var oldEntity = await GetByIdAsync(clinic.Id);
            _appDbContext.Entry(oldEntity).State = EntityState.Detached;

            clinic.Created = oldEntity.Created;
            clinic.CreatedBy = oldEntity.CreatedBy;
            clinic.Updated = DateTime.UtcNow;
            clinic.UpdatedBy = "DetermUser";

            var entityEntry = _appDbContext.Clinics.Update(clinic);
            await _appDbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
