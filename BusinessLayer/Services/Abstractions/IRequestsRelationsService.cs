using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Abstractions
{
    public interface IRequestsRelationsService
    {
        void CreatePatientDonorRequestsRelation(int patientRequestId, int donorRequestId);
    }
}
