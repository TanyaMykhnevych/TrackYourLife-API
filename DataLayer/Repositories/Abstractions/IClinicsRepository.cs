using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer.Repositories.Abstractions
{
    public interface IClinicsRepository : IRepositoryBase<Clinic>
    {
        Clinic GetFirst();

        Clinic GetById(int id);
    }
}
