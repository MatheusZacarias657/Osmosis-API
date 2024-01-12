using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Customer;
using Service.Interfaces.Customer;
using Service.Utils.Enums;

namespace Osmosis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize (Roles = "Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerRegister customer)
        {
            try
            {
                CustomerRegisterStates customerState = customerService.Add(customer);

                if (customerState == CustomerRegisterStates.Created)
                    return Created("", "");

                if (customerState == CustomerRegisterStates.NoExist)
                    throw new Exception("Error on register a new Customer");

                return BadRequest(new { message = String.Format("The {0} already exists", customerState.GetDisplayName()) });
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
                return Ok(customerService.SearchCustomers(filters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CustomerUpdate customer)
        {
            try
            {
                customerService.Update(customer);

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
                customerService.Delete(id);

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
