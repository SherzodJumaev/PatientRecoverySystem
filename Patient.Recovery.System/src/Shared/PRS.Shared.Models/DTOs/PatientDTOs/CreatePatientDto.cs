using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRS.Shared.Models.DTOs.PatientDTOs
{
    public class CreatePatientDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;
    }
}