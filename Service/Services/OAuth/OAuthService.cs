using Repository.Entities.Customer;
using Service.DTOs.OAuth;
using Service.Interfaces.Customer;
using Service.Interfaces.OAuth;
using Service.Utils.Tools;

namespace Service.Services.OAuth
{
    public class OAuthService : IOAuthService
    {
        private readonly ICustomerService customerService;
        private readonly IActiveGuidService activeGuidService;
        private readonly ITemporayPasswordGenerator passwordGenerator;
        private readonly IEmailService emailService;

        public OAuthService(ICustomerService customerService, IActiveGuidService activeGuidService, ITemporayPasswordGenerator passwordGenerator, IEmailService emailService)
        {
            this.customerService = customerService;
            this.activeGuidService = activeGuidService;
            this.passwordGenerator = passwordGenerator;
            this.emailService = emailService;
        }

        public bool Login(UserLogin login)
        {
            try
            {
                CustomerEntity customer = customerService.FindCustomerByLoginOrEmail(login.username, login.username);

                if (customer == null)
                    return false;

                string submittedPassword = OAuthTools.EncryptedPassword(login.password, customer.login);

                if (!submittedPassword.Equals(customer.password))
                    return false;

                activeGuidService.AddNewGuid(customer.id);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Logout(UserLogin login)
        {
            try
            {
                CustomerEntity customer = customerService.FindCustomerByLoginOrEmail(login.username, login.username);

                if (customer == null)
                    return false;

                string submittedPassword = OAuthTools.EncryptedPassword(login.password, customer.login);

                if (!submittedPassword.Equals(customer.password))
                    return false;

                activeGuidService.RemoveBrowserSessionByUser(customer.id, login.browser);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ForgetPassword(string email)
        {
            try
            {
                CustomerEntity customer = customerService.FindCustomerByLoginOrEmail(email, email);

                if (customer == null)
                    throw new Exception("User doesn't exist");

                string temporaryPassword = passwordGenerator.Generate(customer.email);

                Dictionary<string, string> emailParameters = new Dictionary<string, string>()
                {
                    {"password", temporaryPassword }
                };

                emailService.SendEmail(email, emailParameters);
                activeGuidService.RemoveAllSessionsByUser(customer.id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(UserResetPassword userReset)
        {
            try
            {
                CustomerEntity customer = customerService.FindCustomerByLoginOrEmail(userReset.email, userReset.email);

                if (customer == null)
                    throw new Exception("User doesn't exist");

                bool validPassword = passwordGenerator.Decode(userReset.temporaryPassword, userReset.email);

                if (validPassword)
                    customerService.UpdatePassword(customer.id, userReset.password);

                return validPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
