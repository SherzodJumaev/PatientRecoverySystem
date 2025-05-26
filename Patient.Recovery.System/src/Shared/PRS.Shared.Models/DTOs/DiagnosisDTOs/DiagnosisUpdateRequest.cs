using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.DiagnosisModels;

namespace PRS.Shared.Models.DTOs.DiagnosisDTOs
{
    public class DiagnosisUpdateRequest
    {
        public string TreatmentPlan { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public DiagnosisStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}