using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BusinessLayer.Services.Abstractions
{
    public interface IClinicsService
    {
        Task<IList<Clinic>> GetAllClinicsAsync();

        Task<Clinic> GetClinicByIdAsync(int id);

        Clinic GetFirst();

        Task<Clinic> AddClinicAsync(Clinic clinic);

        Task<Clinic> UpdateClinicAsync(Clinic clinic);
    }
}
