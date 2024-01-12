using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Appointments;
using Service.DTOs.Customer;
using Service.Interfaces.Appointment;
using Service.Interfaces.Customer;
using Service.Services.Customer;
using Service.Utils.Enums;

namespace Osmosis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        private readonly ILogger<AppointmentController> logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            this.appointmentService = appointmentService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentRegister appointment)
        {
            try
            {
                return appointmentService.Add(appointment) ? Created("", "") : BadRequest(new { message = "Failed to Register the Appointment" });
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
                return Ok(appointmentService.SearchAppointments(filters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpGet("AvailableAppointments")]
        public IActionResult AvailableAppointments([FromQuery] int profissionalId, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                return Ok(appointmentService.FindAppointmentsAvailable(profissionalId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] AppointmentUpdate appointment)
        {
            try
            {
                appointmentService.Update(appointment);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}
