using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.DTOs.Customer;
using Service.Interfaces.Customer;
using Service.Interfaces.OAuth;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Osmosis.API.Authentication
{
    public class APIAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILogger<APIAuthentication> logger;
        private readonly ICustomAuthenticationService authenticationService;
        private readonly IActiveGuidService activeGuidService;
        private readonly ICustomerRoleService customerRoleService;
        private readonly ICustomerService customerService;

        public APIAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder, ISystemClock clock,
            ILogger<APIAuthentication> logger, ICustomAuthenticationService authenticationService) : base(options, loggerFactory, encoder, clock)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("token"))
                {
                    return AuthenticateResult.Fail("Missing Authorization Header");
                }

                string userGuid = Request.Headers["token"];

                List<Claim> claims = authenticationService.AuthorizeUser(userGuid);

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return AuthenticateResult.Fail("Failed on Authorization");
            }
        }
    }
}
