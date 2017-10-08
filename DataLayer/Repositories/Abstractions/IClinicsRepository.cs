using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;

namespace DataLayer.Repositories.Abstractions
{
    public interface IClinicsRepository
    {
        Clinic GetFirst();
    }
}
