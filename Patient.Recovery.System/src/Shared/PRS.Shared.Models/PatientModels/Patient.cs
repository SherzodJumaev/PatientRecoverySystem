using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.DiagnosisModels;
using PRS.Shared.Models.MedicalHistoryModels;

namespace PRS.Shared.Models.PatientModels
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation property
        public ICollection<MedicalHistory>? MedicalHistories { get; set; } 
        public List<Diagnosis> Diagnoses { get; set; } = new();
    }
}