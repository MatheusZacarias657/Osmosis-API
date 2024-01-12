namespace Service.Interfaces.OAuth
{
    public interface IEmailService
    {
        void SendEmail(string email, Dictionary<string, string> parameters);
    }
}