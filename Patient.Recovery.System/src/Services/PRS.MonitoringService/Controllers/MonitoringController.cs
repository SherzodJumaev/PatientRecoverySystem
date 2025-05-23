using Microsoft.AspNetCore.Mvc;
using PRS.MonitoringService.Services;
using PRS.Shared.Models.DTOs.MonitoringDTOs;
using PRS.Shared.Models.Mappers;

namespace PRS.MonitoringService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientMonitoringRecords(int patientId, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _monitoringService.GetPatientMonitoringRecordsAsync(patientId, ct);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoringRecordById(int id, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _monitoringService.GetMonitoringRecordByIdAsync(id, ct);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("patient/{patientId}/recent")]
        public async Task<IActionResult> GetRecentRecords(int patientId, [FromQuery] int hours = 24, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _monitoringService.GetRecentRecordsAsync(patientId, hours, ct);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitoringRecord([FromBody] CreateMonitoringRecordDto createDto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var monitoringRecord = createDto.ToMonitoringRecordFromCreateMonitoringRecordDto();

            var result = await _monitoringService.CreateMonitoringRecordAsync(monitoringRecord, ct);

            return CreatedAtAction(nameof(GetMonitoringRecordById), new { id = result.Id }, result);
        }

        [HttpGet("patient/{patientId}/check-alarms")]
        public async Task<IActionResult> CheckForAlarms(int patientId, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _monitoringService.CheckForAlarmsAsync(patientId, ct);

            if (result)
                return NotFound();

            return Ok(result);
        }
    }
}