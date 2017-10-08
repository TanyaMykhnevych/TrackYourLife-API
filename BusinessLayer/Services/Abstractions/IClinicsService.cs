using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;

namespace BusinessLayer.Services.Abstractions
{
    public interface IClinicsService
    {
        Clinic GetFirst();
    }
}
