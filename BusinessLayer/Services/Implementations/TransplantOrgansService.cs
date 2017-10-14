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

        public TransplantOrgan GetById(int transplantOrganId)
        {
            return _transplantOrgansRepository.GetById(transplantOrganId);
        }

        public TransplantOrgan Save(TransplantOrgan transplantOrgan)
        {
            return _transplantOrgansRepository.Save(transplantOrgan);
        }

        public void Update(TransplantOrgan transplantOrgan)
        {
            _transplantOrgansRepository.Update(transplantOrgan);
        }
    }
}
