using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Customer;
using Service.DTOs.Professional;
using Service.Interfaces.Professional;
using Service.Services.Customer;
using Service.Utils.Enums;

namespace Osmosis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService professionalService;
        private readonly ILogger<ProfessionalController> logger;

        public ProfessionalController(IProfessionalService professionalService, ILogger<ProfessionalController> logger)
        {
            this.professionalService = professionalService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProfessionalRegister professional)
        {
            try
            {
                bool registerSucess = professionalService.Add(professional);

                if (!registerSucess)
                    return BadRequest(new { message = "The license already exists" });
                
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
                return Ok(professionalService.SearchProfessionals(filters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProfessionalUpdate professional)
        {
            try
            {
                professionalService.Update(professional);

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
                professionalService.Delete(id);

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
