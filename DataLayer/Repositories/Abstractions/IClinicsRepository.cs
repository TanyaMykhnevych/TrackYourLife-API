using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer.Repositories.Abstractions
{
    public interface IClinicsRepository
    {
        Clinic GetFirst();
        Task<Clinic> GetByIdAsync(int id);

        Task<IList<Clinic>> GetAllAsync();

        Task<Clinic> AddAsync(Clinic clinic);

        Task<Clinic> UpdateAsync(Clinic clinic);
    }
}
