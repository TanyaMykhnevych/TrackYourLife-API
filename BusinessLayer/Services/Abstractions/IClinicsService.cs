﻿using System.Collections.Generic;
using DataLayer.Entities;

namespace BusinessLayer.Services.Abstractions
{
    public interface IClinicsService
    {
        IList<Clinic> GetAllClinics();

        Clinic GetClinicById(int id);

        Clinic GetFirst();

        Clinic AddClinic(Clinic clinic);

        Clinic UpdateClinic(Clinic clinic);
    }
}
