using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities.Organ;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class TransplantOrgansService : ITransplantOrgansService
    {
        private readonly ITransplantOrgansRepository _transplantOrgansRepository;

        public TransplantOrgansService(ITransplantOrgansRepository transplantOrgansRepository)
        {
            _transplantOrgansRepository = transplantOrgansRepository;
        }

        public void Save(TransplantOrgan transplantOrgan)
        {
            _transplantOrgansRepository.Save(transplantOrgan);
        }
    }
}
