using System.Text;

namespace ComercialRegReceiver
{
    public static class Base64Converter
    {
        // Converts a string to Base64
        public static string StringToBase64(string input)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytesToEncode);
        }

        // Converts a Base64 encoded string back to its original string
        public static string Base64ToString(string base64EncodedData)
        {
            byte[] decodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        // Reads the content of a file, converts it to Base64, and returns the Base64 string
        public static string FileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        // Converts a Base64 encoded string and writes the content back to a file
        public static void Base64ToFile(string base64EncodedData, string outputFilePath)
        {
            byte[] bytesToWrite = Convert.FromBase64String(base64EncodedData);
            File.WriteAllBytes(outputFilePath, bytesToWrite);
        }

        // New method: Converts the content of a source file to Base64 and writes the Base64 string to an output file
        public static void ConvertFileContentToBase64AndSave(string sourceFilePath, string base64OutputFilePath)
        {
            // Read the source file and convert its content to Base64
            string base64String = FileToBase64(sourceFilePath);
            // Write the Base64 string to the output file
            File.WriteAllText(base64OutputFilePath, base64String);
        }

        // New method: Reads a Base64 encoded file, decodes it, and writes the decoded content to a new file
        public static void DecodeBase64FileContentAndSave(string base64InputFilePath, string decodedOutputFilePath)
        {
            // Read the Base64 encoded content from the input file
            string base64EncodedData = File.ReadAllText(base64InputFilePath);
            // Decode the Base64 content
            byte[] decodedBytes = Convert.FromBase64String(base64EncodedData);
            // Write the decoded bytes to the output file
            File.WriteAllBytes(decodedOutputFilePath, decodedBytes);
        }
    }
}
