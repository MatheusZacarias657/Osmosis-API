using Service.DTOs.Customer;
using Service.Interfaces.Customer;
using Service.Interfaces.OAuth;
using System.Security.Claims;

namespace Service.Services.OAuth
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IActiveGuidService activeGuidService;
        private readonly ICustomerRoleService customerRoleService;
        private readonly ICustomerService customerService;

        public CustomAuthenticationService(IActiveGuidService activeGuidService, ICustomerRoleService customerRoleService, ICustomerService customerService)
        {
            this.activeGuidService = activeGuidService;
            this.customerRoleService = customerRoleService;
            this.customerService = customerService;
        }

        public List<Claim> AuthorizeUser(string userGuid)
        {
            try
            {
                ActiveGuid session = activeGuidService.FindSessionByGuid(userGuid);
                int userPrincipalRole = customerService.FindRoleByCustomerId(session.id_customer);
                List<string> availableRoles = customerRoleService.GetAvailableRoles(userPrincipalRole);

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, availableRoles[0])
                };

                foreach (string role in availableRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return claims;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
