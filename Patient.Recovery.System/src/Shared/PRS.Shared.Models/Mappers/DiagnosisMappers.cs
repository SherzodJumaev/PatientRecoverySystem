using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.DiagnosisModels;
using PRS.Shared.Models.DTOs.DiagnosisDTOs;

namespace PRS.Shared.Models.Mappers
{
    public static class DiagnosisMappers
    {
        public static Diagnosis ToDiagnosisFromCreateDiagnosisRequest(this CreateDiagnosisRequest create)
        {
            return new Diagnosis
            {
                PatientId = create.PatientId,
                PhysicianId = create.PhysicianId,
                Symptoms = create.Symptoms,
                Notes = create.Notes,
            };
        }

        public static Diagnosis ToDiagnosisFromDiagnosisUpdateRequest(this DiagnosisUpdateRequest request)
        {
            return new Diagnosis
            {
                TreatmentPlan = request.TreatmentPlan,
                Medications = request.Medications,
                Recommendations = request.Recommendations,
                Status = request.Status,
                Notes = request.Notes
            };
        }
    }
}