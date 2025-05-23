using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS.Shared.Models.DTOs.MonitoringDTOs;
using PRS.Shared.Models.MonitoringModels;

namespace PRS.Shared.Models.Mappers
{
    public static class MonitoringRecordMappers
    {
        public static MonitoringRecord ToMonitoringRecordFromCreateMonitoringRecordDto(this CreateMonitoringRecordDto createMonitoringRecordDto)
        {
            return new MonitoringRecord
            {
                PatientId = createMonitoringRecordDto.PatientId,
                Temperature = createMonitoringRecordDto.Temperature,
                BloodPressureDiastolic = createMonitoringRecordDto.BloodPressureDiastolic,
                BloodPressureSystolic = createMonitoringRecordDto.BloodPressureSystolic,
                HeartRate = createMonitoringRecordDto.HeartRate,
                Weight = createMonitoringRecordDto.Weight,
                Symptoms = createMonitoringRecordDto.Symptoms,
                Notes = createMonitoringRecordDto.Notes,
                Location = createMonitoringRecordDto.Location,
                RecordedBy = createMonitoringRecordDto.Location,
                RecordedAt = DateTime.UtcNow
            };
        }
    }
}