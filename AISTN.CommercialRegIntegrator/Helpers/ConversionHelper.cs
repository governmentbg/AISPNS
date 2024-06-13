namespace AISTN.CommercialRegIntegrator.Helpers
{
    public static class ConversionHelper
    {
        /// <summary>
        /// Tries to parse a date string and returns a nullable DateTime object.
        /// Returns null if the input is null, empty, or parsing fails.
        /// </summary>
        /// <param name="dateString">The date string to parse.</param>
        /// <returns>A nullable DateTime object if successful, or null if the input is null, empty, or parsing fails.</returns>
        public static DateTime? ParseDateOrNull(string dateString)
        {
            // Check for null or empty string
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            if (DateTime.TryParse(dateString, out DateTime tempDate))
            {
                return tempDate;
            }
            else
            {
                return null;
            }
        }

        public static int? SafeConvertToInt(string input)
        {
            int result;
            // Try to parse the input string as an integer.
            // If successful, return the parsed integer.
            // If not successful, return null.
            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool? SafeConvertToBool(string input)
        {
            bool result;
            if (bool.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static string GetSafeSubstring(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Length <= maxLength ? input : input.Substring(0, maxLength);
        }


    }
}