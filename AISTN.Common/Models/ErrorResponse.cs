namespace AISTN.Common.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorMessage, List<string>? errors = null)
        {
            ErrorMessage = errorMessage;
            Errors = errors;
        }

        public string ErrorMessage { get; set; }
        public List<string>? Errors { get; set; }
    }
}
