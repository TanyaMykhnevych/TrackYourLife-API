using DataLayer.Entities.OrganQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories.Abstractions
{
    public interface IPatientOrganQueriesRepository
    {
        PatientOrganQuery GetById(int patientOrganQueryId);

        PatientOrganQuery Save(PatientOrganQuery patientOrganQuery);

        void Update(PatientOrganQuery patientOrganQuery);
    }
}
