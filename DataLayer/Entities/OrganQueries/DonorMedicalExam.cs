using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.OrganQueries
{
    public class DonorMedicalExam
    {
        public int Id { get; set; }

        public DateTime When { get; set; }
        
        /// <summary>
        /// Where examination will be passed
        /// </summary>
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

        public int DonorOrganQueryId { get; set; }
        public DonorOrganQuery DonorOrganQuery { get; set; }
    }
}
