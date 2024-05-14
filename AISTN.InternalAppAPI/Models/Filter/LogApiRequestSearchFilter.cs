namespace AISTN.InternalAppAPI.Models.Filter
{
    public class LogApiRequestSearchFilter
    {
        public string? Endpoint { get; set; }

        public int? ResponseHttpCode { get; set; }

        public string? IpAddress { get; set; }

        public DateTime? RequestTimestampFrom { get; set; }

        public DateTime? RequestTimestampTo { get; set; }
    }
}
