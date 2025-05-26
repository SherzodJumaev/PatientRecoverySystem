using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.PatientModels;

namespace PRS.Shared.Models.DiagnosisModels
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string Symptoms { get; set; } = string.Empty;
        public string TreatmentPlan { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public int SeverityLevel { get; set; } // 1-5 scale
        public bool RequiresEmergency { get; set; }
        public DiagnosisStatus Status { get; set; } // Active, Resolved, Under Review
        public string Notes { get; set; } = string.Empty;
        public Patient? Patient { get; set; }
        public Physician? Physician { get; set; }
    }
}