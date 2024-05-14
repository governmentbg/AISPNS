using System.Globalization;
using System.Text;

namespace RegixSOAPService
{
    public static class LotusHelper
    {
        public const int TTL_DEFAULT = 1800;
        public const int RT_Success = 0;
        public const int RT_Error = 1;
        public const int RT_Session = 10;
        public const int RT_ResultSet = 11;

        public const int RS_Complete = 0;
        public const int RS_Incomplete = 1;

        public const int ERR_UserPass = 100;
        public const int ERR_InvalidSession = 1000;
        public const int ERR_ExpiredSession = 1001;
        public const int ERR_NotImplemented = 9999;
        public const int ERR_WrongDomain = 1;

        private const string WS_SECRET = "c284e578fe2fd47633b98dd1e10312a9e2dee3f722acc039599012dd305bc986";

        public enum ResponseType
        {
            Success = RT_Success,
            Error = RT_Error,
            Session = RT_Session,
            ResultSet = RT_ResultSet
        }


        public static string Time2String(DateTime iTime)
        {
            // Formatting the DateTime directly to match the desired output:
            // SSMMHHDDMMYYYY
            return iTime.ToString("ssmmHHddMMyyyy");
        }

        public static DateTime? String2Time(string iStr)
        {
            try
            {
                // Assuming the string is in the "yyyyMMddHHmmss" format based on your extraction
                if (DateTime.TryParseExact(iStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
                else
                {
                    // Handle parsing failure if necessary, returning null for now
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception or handle it as needed
                Console.WriteLine($"Error parsing date: {ex.Message}");
                return null;
            }
        }

        //public static bool VerifyAuthToken(string tok)
        //{
        //    try
        //    {
        //        // Simulated decoding and time parsing; implement these methods based on your environment
        //        string payload = Base64ToString(tok.Split('#')[0]);
        //        DateTime tokenTime = ParseTokenTime(payload);
        //        string userName = ExtractUserName(payload);

        //        // Simulate verification; implement according to your security requirements
        //        bool isValidUser = VerifyUser(userName, tok);
        //        if (!isValidUser) return false;

        //        // Check if the token is expired
        //        return IsTokenValid(tokenTime);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error verifying auth token: {ex.Message}");
        //        return false;
        //    }
        //}

        public static string Base64ToString(string base64EncodedData)
        {
            // Convert Base64 String to byte[]
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            // Convert byte[] to String
            string result = Encoding.UTF8.GetString(base64EncodedBytes);
            return result;
        }

    }
}
