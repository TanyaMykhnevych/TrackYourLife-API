using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;
using DataLayer.DbContext;

namespace DataLayer.Repositories.Implementations
{
    public class TransplantOrgansRepository : ITransplantOrgansRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransplantOrgansRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save(TransplantOrgan transplantOrgan)
        {
            //TODO: determine current user
            transplantOrgan.CreatedBy = "Default";
            transplantOrgan.Created = DateTime.UtcNow;

            _appDbContext.TransplantOrgans.Add(transplantOrgan);
            _appDbContext.SaveChanges();
        }
    }
}
