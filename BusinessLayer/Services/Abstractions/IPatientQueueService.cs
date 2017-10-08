using DataLayer.Entities.OrganQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientQueueService
    {
        IList<PatientOrganQuery> GetPengingQueue();

        IList<PatientOrganQuery> GetPengingQueueByOrgan(int organInfoId);
    }
}
