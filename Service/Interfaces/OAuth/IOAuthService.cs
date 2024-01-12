using Service.DTOs.OAuth;

namespace Service.Interfaces.OAuth
{
    public interface IOAuthService
    {
        void ForgetPassword(string email);
        bool Login(UserLogin login);
        bool Logout(UserLogin login);
        bool ResetPassword(UserResetPassword userReset);
    }
}