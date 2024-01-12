using Microsoft.Extensions.Configuration;
using OtpNet;
using Service.Interfaces.OAuth;
using System.Text;

namespace Service.Services.OAuth
{
    public class TemporayPasswordGenerator : ITemporayPasswordGenerator
    {
        private static int _expiresTime;
        private readonly IConfiguration configuration;

        public TemporayPasswordGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
            GetExpiration();
        }

        private void GetExpiration()
        {
            try
            {
                if (_expiresTime != default)
                    return;

                string time = configuration["Settings:PasswordExpiration"];

                if (DateTime.TryParseExact(time, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime convertTime))
                {
                    _expiresTime = convertTime.Hour * 60 + convertTime.Minute;
                }
                else
                {
                    throw new Exception("Invalid time for OTP");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Generate(string encodeKey)
        {
            try
            {
                DateTime date = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                string key = $"{encodeKey}:{date}";
                Totp totp = new Totp(Encoding.ASCII.GetBytes(key), _expiresTime * 60, OtpHashMode.Sha256, 10, new TimeCorrection(date));

                return totp.ComputeTotp(date);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Decode(string otp, string decodeKey)
        {
            try
            {
                DateTime date = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

                for (int i = 0; i <= _expiresTime; i++)
                {
                    string trialKey = $"{decodeKey}:{date}";
                    Totp totp = new Totp(Encoding.ASCII.GetBytes(trialKey), _expiresTime * 60, OtpHashMode.Sha256, 6, new TimeCorrection(date));
                    bool isValid = totp.VerifyTotp(date, otp, out _, new VerificationWindow(-11, 11));

                    if (isValid)
                    {
                        return true;
                    }
                    else
                    {
                        date = date.AddMinutes(-1);
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
