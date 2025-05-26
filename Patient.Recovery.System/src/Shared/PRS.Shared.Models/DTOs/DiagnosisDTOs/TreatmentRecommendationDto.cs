using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS.Shared.Models.DTOs.DiagnosisDTOs
{
    public class TreatmentRecommendationDto
    {
        public string TreatmentType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string ExpectedOutcome { get; set; } = string.Empty;
    }
}