using Microsoft.AspNetCore.Mvc;
using PRS.DiagnoosisService.Services;
using PRS.Shared.Models.DTOs.DiagnosisDTOs;
using PRS.Shared.Models.Mappers;

namespace PRS.DiagnoosisService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisService _diagnosisService;

        public DiagnosisController(IDiagnosisService diagnosisService)
        {
            _diagnosisService = diagnosisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiagnoses()
        {
            var diagnoses = await _diagnosisService.GetAllDiagnosesAsync();
            return Ok(diagnoses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosisById(int id)
        {
            var diagnosis = await _diagnosisService.GetDiagnosisByIdAsync(id);
            if (diagnosis == null)
                return NotFound();

            return Ok(diagnosis);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetDiagnosesByPatientId(int patientId)
        {
            var diagnoses = await _diagnosisService.GetDiagnosesByPatientIdAsync(patientId);
            return Ok(diagnoses);
        }

        // [HttpGet("status/{status}")]
        // public async Task<ActionResult> GetDiagnosesByStatus(string status)
        // {
        //     var diagnoses = await _diagnosisService.GetDiagnosisByIdAsync(status);
        //     return Ok(diagnoses);
        // }

        // [HttpGet("severity/{severity}")]
        // public async Task<ActionResult> GetDiagnosesBySeverity(string severity)
        // {
        //     var diagnoses = await _diagnosisService.GetDiagnosesBySeverityAsync(severity);
        //     return Ok(diagnoses);
        // }

        [HttpPost]
        public async Task<IActionResult> CreateDiagnosis([FromBody] CreateDiagnosisRequest createDiagnosisDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dModel = createDiagnosisDto.ToDiagnosisFromCreateDiagnosisRequest();

                var diagnosis = await _diagnosisService.CreateDiagnosisAsync(dModel);
                return CreatedAtAction(nameof(GetDiagnosisById), new { id = diagnosis.Id }, diagnosis);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnosis([FromRoute] int id, [FromBody] DiagnosisUpdateRequest updateDiagnosisDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dModel = updateDiagnosisDto.ToDiagnosisFromDiagnosisUpdateRequest();

            var diagnosis = await _diagnosisService.UpdateDiagnosisAsync(id, dModel);
            if (diagnosis == null)
                return NotFound();

            return Ok(diagnosis);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis([FromRoute] int id)
        {
            var result = await _diagnosisService.DeleteDiagnosisAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}