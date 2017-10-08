using BusinessLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services.Implementations
{
    public class ClinicsService : IClinicsService
    {
        private readonly IClinicsRepository _clinicsRepository;

        public ClinicsService(IClinicsRepository clinicsRepository)
        {
            _clinicsRepository = clinicsRepository;
        }

        public Clinic GetFirst()
        {
            return _clinicsRepository.GetFirst();
        }
    }
}
