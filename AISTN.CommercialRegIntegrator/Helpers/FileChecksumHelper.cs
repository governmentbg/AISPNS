using System.Security.Cryptography;
using System.Text;

namespace AISTN.CommercialRegIntegrator.Helpers
{
    internal static class FileChecksumHelper
    {
        
        public static string CalculateChecksum(string content)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(content);

                    // Using SHA1 to calculate the checksum.
                    using (SHA1 sha1 = SHA1.Create())
                    {
                        byte[] checksum = sha1.ComputeHash(buffer);
                        return BitConverter.ToString(checksum).Replace("-", String.Empty);
                    }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions (file not found, no permission, etc.)
                throw;
               
            }
        }

        

    }
}
