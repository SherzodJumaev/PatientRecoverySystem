using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRS.Shared.Models.DTOs.DiagnosisDTOs
{
    public class CreateDiagnosisRequest
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int PhysicianId { get; set; }
        [Required]
        public string Symptoms { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}