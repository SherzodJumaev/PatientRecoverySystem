using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.PatientModels;

namespace PRS.Shared.Models.RehabilitationPlanModels
{
    public class RehabilitationPlan
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string PlanName { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, Completed, Paused
        
        public int ProgressPercentage { get; set; } = 0;
        
        [StringLength(1000)]
        public string Goals { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Exercises { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual Patient Patient { get; set; } = null!;
        public virtual ICollection<RehabilitationProgress> ProgressRecords { get; set; } = new List<RehabilitationProgress>();
    }
}