using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities.OrganQueries;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientQueueService
    {
        IList<PatientOrganQuery> GetPengingQueue();

        IList<PatientOrganQuery> GetPengingQueueByOrgan(int organInfoId);
    }
}
