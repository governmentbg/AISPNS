using System.Security.Cryptography;
using System.Text;

namespace RegixSOAPService
{
    public class PasswordHelper
    {
        // Method to generate a cryptographically secure salt
        public static string GenerateSalt(int size = 32)
        {
            // Create a buffer
            byte[] saltBytes = new byte[size];

            // Fill the buffer with a cryptographically strong random sequence of values.
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Return the salt as a Base64 string to ensure it is easy to store and handle
            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateSaltedHash(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Combine the password and the salt before hashing
                var saltedPassword = string.Concat(password, salt);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordAsBytes);

                // Convert back to a string (base64 is easy to store and transmit)
                string hash = Convert.ToBase64String(hashBytes);

                return hash;
            }
        }
    }
}
