﻿using BusinessLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.ViewModels
{
    public class MedicalExamResultViewModel
    {
        public int DonorOrganQueryId { get; set; }
        
        public MedicalExamStatuses MedicalExamStatus { get; set; }

        public string MedicalExamResults { get; set; }
    }
}
