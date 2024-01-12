using System.Security.Claims;

namespace Service.Interfaces.OAuth
{
    public interface ICustomAuthenticationService
    {
        List<Claim> AuthorizeUser(string userGuid);
    }
}