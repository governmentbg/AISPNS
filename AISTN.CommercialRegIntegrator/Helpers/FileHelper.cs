namespace AISTN.CommercialRegIntegrator.Helpers
{
    public static class FileHelper
    {
        public static string MoveFile(string sourceFilePath, string destinationDirectory, bool overwrite = false)
        {
            try
            {
                // Ensure the destination directory exists
                Directory.CreateDirectory(destinationDirectory);

                // Get the filename from the source file path
                string fileName = Path.GetFileName(sourceFilePath);

                // Combine the destination directory and filename to get the full destination file path
                string destinationFilePath = Path.Combine(destinationDirectory, fileName);

                // Move the file and optionally overwrite it
                File.Move(sourceFilePath, destinationFilePath, overwrite);
                Console.WriteLine($"File moved from {sourceFilePath} to {destinationFilePath}");

                // Return the full path of the destination file
                return destinationFilePath;
            }
            catch (IOException ex)
            {
                // Handle the exception for I/O errors
                Console.WriteLine($"An I/O error occurred: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle the exception for unauthorized access
                Console.WriteLine($"Unauthorized Access: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Return null or an appropriate value indicating that the operation failed
            return null;
        }
    }
}
