using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities.OrganQueries;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPatientQueueService
    {
        IList<PatientRequest> GetPengingQueue();

        IList<PatientRequest> GetPengingQueueByOrgan(int organInfoId);
    }
}
