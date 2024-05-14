namespace AISTN.InternalAppAPI.Models.Details
{
    public class DetailsLogApiRequestDTO
    {
        public long? Id { get; set; }

        public string? Endpoint { get; set; }

        public string? RequestContent { get; set; }

        public string? ResponseContent { get; set; }

        public int? ResponseHttpCode { get; set; }

        public string? IpAddress { get; set; }

        public long? ExceptionId { get; set; }

        public DateTime? RequestTimestamp { get; set; }

        public DateTime? ResponseTimestamp { get; set; }
    }
}
