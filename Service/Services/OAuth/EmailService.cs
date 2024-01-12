using Microsoft.Extensions.Configuration;
using Service.DTOs.Email;
using Service.Interfaces.HttpFactory;
using Service.Interfaces.OAuth;

namespace Service.Services.OAuth
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpFactoryRequests httpFactory;

        public EmailService(IConfiguration configuration, IHttpFactoryRequests httpFactory)
        {
            this.configuration = configuration;
            this.httpFactory = httpFactory;
        }

        public void SendEmail(string email, Dictionary<string, string> parameters)
        {
            try
            {
                Uri url = httpFactory.BuildUri(configuration["Settings:Brevo:Url"], "/v3/smtp/email");

                Dictionary<string, string> headers = new()
                {
                    { "api-key", configuration["Settings:Brevo:api-key"]}
                };

                EmailBrevoPayload body = new EmailBrevoPayload()
                {
                    sender = new EmailContact() { email = configuration["Settings:Brevo:Email"], name = configuration["Settings:Brevo:Name"] },
                    to = new List<EmailContact> { new EmailContact { email = email, name = email } },
                    templateId = Convert.ToInt32(configuration["Settings:Brevo:TemplateId"]),
                    @params = parameters
                };

                HttpResponseMessage httpResponse = httpFactory.DefaultPostRequest(url, body, headers).Result;

                if (!httpResponse.IsSuccessStatusCode)
                    throw new Exception($"The Brevo API return a error: {httpResponse}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
