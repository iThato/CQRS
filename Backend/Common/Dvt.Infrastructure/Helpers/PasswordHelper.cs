using System.Security.Cryptography;
using System.Text;

namespace Dvt.Infrastructure.Helpers
{
    public static class PasswordHelper
    {
        public static string GeneratePasswordSalt()
        {
            byte[] salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);
            return Encoding.ASCII.GetString(salt);
        }

        public static string GenerateSaltedHash(string password, string salt)
        {
            var plainText = Encoding.ASCII.GetBytes(password);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = Encoding.ASCII.GetBytes(salt[i].ToString())[0];
            }
            return Encoding.ASCII.GetString(algorithm.ComputeHash(plainTextWithSaltBytes));
        }
    }
}
