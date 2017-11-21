using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities;
using Common.Entities.OrganQueries;
using Common.Enums;

namespace BusinessLayer.Models.ViewModels.MedicalExam
{
    public class DonorMedicalExamListItemViewModel
    {
        public int Id { get; set; }

        public DateTime ScheduledAt { get; set; }
        
        public int ClinicId { get; set; }

        public MedicalExamStatuses Status { get; set; }

        public string Results { get; set; }

        public int DonorOrganQueryId { get; set; }


        public DonorMedicalExamListItemViewModel(DonorMedicalExam exam)
        {
            Id = exam.Id;
            ScheduledAt = exam.ScheduledAt;
            ClinicId = exam.ClinicId;
            Status = exam.Status;
            Results = exam.Results;
            DonorOrganQueryId = exam.DonorRequestId;
        }
    }
}
