using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.OAuth;
using Service.Interfaces.OAuth;
using Service.Services.Customer;

namespace Osmosis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class OAuthController : ControllerBase
    {
        private readonly IOAuthService authService;
        private readonly ILogger<OAuthController> logger;

        public OAuthController(IOAuthService authService, ILogger<OAuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin login)
        {
            try
            {
                return authService.Login(login) ? Ok() : Forbid();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Logout([FromBody] UserLogin login)
        {
            try
            {
                return authService.Logout(login) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ForgetPassword([FromQuery] string email)
        {
            try
            {
                authService.ForgetPassword(email);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult ResetPassword([FromBody] UserResetPassword userReset)
        {
            try
            {
                return authService.ResetPassword(userReset) ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}
