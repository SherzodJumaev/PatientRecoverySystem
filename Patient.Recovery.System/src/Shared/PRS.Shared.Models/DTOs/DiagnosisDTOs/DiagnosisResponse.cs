using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS.Shared.Models.DTOs.DiagnosisDTOs
{
    public class DiagnosisResponse
    {
        public int DiagnosisId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PhysicianName { get; set; } = string.Empty;
        public DateTime DiagnosisDate { get; set; }
        public string PrimaryDiagnosis { get; set; } = string.Empty;
        public string TreatmentPlan { get; set; } = string.Empty;
        public int SeverityLevel { get; set; }
        public bool RequiresEmergency { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<TreatmentRecommendationDto> Recommendations { get; set; } = new();
    }
}