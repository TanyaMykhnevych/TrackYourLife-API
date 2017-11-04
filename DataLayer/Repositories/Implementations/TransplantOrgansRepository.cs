using DataLayer.Repositories.Abstractions;
using System;
using DataLayer.Entities.Organ;
using DataLayer.DbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
    public class TransplantOrgansRepository : ITransplantOrgansRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransplantOrgansRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public TransplantOrgan GetById(int transplantOrganId)
        {
            return _appDbContext
                .TransplantOrgans
                .SingleOrDefault(x => x.Id == transplantOrganId);
        }

        public TransplantOrgan Save(TransplantOrgan transplantOrgan)
        {
            transplantOrgan.CreatedBy = "Default";
            transplantOrgan.Created = DateTime.UtcNow;

            var entity = _appDbContext.TransplantOrgans.Add(transplantOrgan).Entity;
            _appDbContext.SaveChanges();

            return entity;
        }

        public void Update(TransplantOrgan transplantOrgan)
        {
            var oldEntity = GetById(transplantOrgan.Id);
            _appDbContext.Entry(oldEntity).State = EntityState.Detached;

            transplantOrgan.Created = oldEntity.Created;
            transplantOrgan.CreatedBy = oldEntity.CreatedBy;
            transplantOrgan.Updated = DateTime.UtcNow;
            transplantOrgan.UpdatedBy = "Default";

            _appDbContext.TransplantOrgans.Update(transplantOrgan);
            _appDbContext.SaveChanges();
        }
    }
}
