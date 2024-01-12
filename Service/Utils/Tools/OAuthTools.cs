using System.Security.Cryptography;
using System.Text;

namespace Service.Utils.Tools
{
    public static class OAuthTools
    {
        public static string EncryptedPassword(string password, string salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainText = Encoding.ASCII.GetBytes(password);
            byte[] byteSalt = Encoding.ASCII.GetBytes(salt);
            byte[] plainTextWithSaltBytes = new byte[plainText.Length + byteSalt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (int i = 0; i < byteSalt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = byteSalt[i];
            }

            return Encoding.ASCII.GetString(algorithm.ComputeHash(plainTextWithSaltBytes));
        }
    }
}
