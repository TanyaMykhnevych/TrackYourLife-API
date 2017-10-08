using BusinessLayer.Models.Enums;
using BusinessLayer.Services.Abstractions;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganQueries;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class PatientOrganRequestService : IPatientOrganRequestService
    {
        private readonly IPatientOrganQueriesRepository _patientOrganQueriesRepository;
        private readonly IOrganInfoService _organInfoService;

        public PatientOrganRequestService(
            IPatientOrganQueriesRepository patientOrganQueriesRepository,
            IOrganInfoService organInfoService)
        {
            _patientOrganQueriesRepository = patientOrganQueriesRepository;
            _organInfoService = organInfoService;
        }

        public void AddPatientOrganQueryToQueue(PatientOrganQuery patientOrganQuery)
        {
            bool isOrganInfoExist = _organInfoService.IfOrganInfoExists(patientOrganQuery.OrganInfoId);
            if (!isOrganInfoExist)
            {
                throw new ArgumentException(nameof(patientOrganQuery.OrganInfoId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = (int)PatientQueryPriority.Normal;
            }

            patientOrganQuery.Status = (int)PatientQueryStatuses.AwaitingForDonor;

            _patientOrganQueriesRepository.Save(patientOrganQuery);

            //TODO: send email to clinic that query has been added
        }

        public void ChangePatientOrganQueryStatus(int patientOrganQueryId, PatientQueryStatuses status)
        {
            PatientOrganQuery patientOrganQuery = _patientOrganQueriesRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = (int)PatientQueryPriority.Normal;
            }

            patientOrganQuery.Status = (int)PatientQueryStatuses.AwaitingForDonor;

            _patientOrganQueriesRepository.Update(patientOrganQuery);

            //TODO: send email to clinic that query status has been changed
        }

        public void SetTransplantOrganToPatient(int patientOrganQueryId, TransplantOrgan transplantOrgan)
        {
            PatientOrganQuery patientOrganQuery = _patientOrganQueriesRepository.GetById(patientOrganQueryId);
            if (patientOrganQuery == null)
            {
                throw new ArgumentException(nameof(patientOrganQueryId));
            }

            if (transplantOrgan == null)
            {
                throw new ArgumentException(nameof(transplantOrgan));
            }

            if (!Enum.IsDefined(typeof(PatientQueryPriority), patientOrganQuery.Priority))
            {
                patientOrganQuery.Priority = (int)PatientQueryPriority.Normal;
            }

            //TODO: determine current user
            transplantOrgan.CreatedBy = "Default";
            transplantOrgan.Created = DateTime.UtcNow;

            patientOrganQuery.TransplantOrgan = transplantOrgan;
            patientOrganQuery.Status = (int)PatientQueryStatuses.AwaitingForTransplanting;

            //TODO: check that transplant organ will be saved
            _patientOrganQueriesRepository.Update(patientOrganQuery);

            //TODO: send email to clinic that query status has been changed
        }
    }
}
