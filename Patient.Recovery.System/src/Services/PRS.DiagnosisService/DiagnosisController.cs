using Microsoft.AspNetCore.Mvc;
using PRS.DiagnosisService.Services;
using PRS.Shared.Models.DTOs.DiagnosisDTOs;
using PRS.Shared.Models.Mappers;

namespace PRS.DiagnosisService
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisService _diagnosisService;
        private readonly ILogger<DiagnosisController> _logger;

        public DiagnosisController(IDiagnosisService diagnosisService, ILogger<DiagnosisController> logger)
        {
            _diagnosisService = diagnosisService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosisResponse>>> GetAllDiagnoses()
        {
            try
            {
                var diagnoses = await _diagnosisService.GetAllDiagnosesAsync();
                return Ok(diagnoses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all diagnoses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosisResponse>> GetDiagnosis(int id)
        {
            try
            {
                var diagnosis = await _diagnosisService.GetDiagnosisByIdAsync(id);
                if (diagnosis == null)
                    return NotFound($"Diagnosis with ID {id} not found");

                return Ok(diagnosis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving diagnosis {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<DiagnosisResponse>>> GetPatientDiagnoses(int patientId)
        {
            try
            {
                var diagnoses = await _diagnosisService.GetDiagnosesByPatientIdAsync(patientId);
                return Ok(diagnoses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving diagnoses for patient {patientId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DiagnosisResponse>> CreateDiagnosis([FromBody] CreateDiagnosisRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dModel = request.ToDiagnosisFromCreateDiagnosisRequest();
                var diagnosis = await _diagnosisService.CreateDiagnosisAsync(dModel);
                return CreatedAtAction(nameof(GetDiagnosis), new { id = diagnosis.Id }, diagnosis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating diagnosis");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiagnosisResponse>> UpdateDiagnosis(int id, [FromBody] DiagnosisUpdateRequest request)
        {
            try
            {
                var dModel = request.ToDiagnosisFromDiagnosisUpdateRequest();
                var updatedDiagnosis = await _diagnosisService.UpdateDiagnosisAsync(id, dModel);
                if (updatedDiagnosis == null)
                    return NotFound($"Diagnosis with ID {id} not found");

                return Ok(updatedDiagnosis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating diagnosis {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiagnosis(int id)
        {
            try
            {
                var result = await _diagnosisService.DeleteDiagnosisAsync(id);
                if (!result)
                    return NotFound($"Diagnosis with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting diagnosis {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}