using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Abstractions;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class ClinicsService : IClinicsService
    {
        private readonly IClinicsRepository _clinicsRepository;

        public ClinicsService(IClinicsRepository clinicsRepository)
        {
            _clinicsRepository = clinicsRepository;
        }

        public Task<IList<Clinic>> GetAllClinicsAsync()
        {
            return _clinicsRepository.GetAllAsync();
        }

        public Task<Clinic> GetClinicByIdAsync(int id)
        {
            return _clinicsRepository.GetByIdAsync(id);
        }

        public Clinic GetFirst()
        {
            return _clinicsRepository.GetFirst();
        }

        public async Task<Clinic> AddClinicAsync(Clinic clinic)
        {
            return await _clinicsRepository.AddAsync(clinic);
        }

        public Task<Clinic> UpdateClinicAsync(Clinic clinic)
        {
            return _clinicsRepository.UpdateAsync(clinic);
        }
    }
}
