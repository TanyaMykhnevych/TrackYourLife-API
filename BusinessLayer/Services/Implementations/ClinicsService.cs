using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using DataLayer.Repositories.Abstractions;
using System.Threading.Tasks;
using Common.Entities;

namespace BusinessLayer.Services.Implementations
{
    public class ClinicsService : IClinicsService
    {
        private readonly IClinicsRepository _clinicsRepository;

        public ClinicsService(IClinicsRepository clinicsRepository)
        {
            _clinicsRepository = clinicsRepository;
        }

        public IList<Clinic> GetAllClinics()
        {
            return _clinicsRepository.GetAll();
        }

        public Clinic GetClinicById(int id)
        {
            return _clinicsRepository.GetById(id);
        }

        public Clinic GetFirst()
        {
            return _clinicsRepository.GetFirst();
        }

        public Clinic AddClinic(Clinic clinic)
        {
            return _clinicsRepository.Add(clinic);
        }

        public Clinic UpdateClinic(Clinic clinic)
        {
            return _clinicsRepository.Update(clinic);
        }
    }
}
