using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Patient;
using Service.Interfaces.Patient;

namespace Osmosis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly ILogger<PatientController> logger;

        public PatientController(IPatientService patientService, ILogger<PatientController> logger)
        {
            this.patientService = patientService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PatientRegister patient)
        {
            try
            {
                bool registerSucess = patientService.Add(patient);

                if (!registerSucess)
                    return BadRequest(new { message = "The patient already exists" });
                
                return Created("", "");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Read([FromQuery] Dictionary<string, string>? filters)
        {
            try
            {
                return Ok(patientService.SearchPatients(filters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] PatientUpdate patient)
        {
            try
            {
                patientService.Update(patient);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                patientService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}
