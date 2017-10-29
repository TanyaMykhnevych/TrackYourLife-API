﻿using DataLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.OrganQueries
{
    public class DonorMedicalExam : BaseEntity
    {
        public int Id { get; set; }

        public DateTime ScheduledAt { get; set; }
        
        /// <summary>
        /// Where examination will be passed
        /// </summary>
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        
        //TODO: need to implement enum
        public int Status { get; set; }

        public string Results { get; set; }

        public int DonorOrganQueryId { get; set; }
        public DonorOrganQuery DonorOrganQuery { get; set; }
    }
}
