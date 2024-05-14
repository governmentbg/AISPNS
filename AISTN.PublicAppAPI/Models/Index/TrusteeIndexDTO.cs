namespace AISTN.PublicAppAPI.Models.Index
{
    public class TrusteeIndexDTO
    {
        public Guid Id { get; set; }

        public Guid DeedId { get; set; }

        public string? Name { get; set; }

        public string? IndentType { get; set; }

        public string? Indent { get; set; }

        public string? Status { get; set; }

        public string? CountryName { get; set; }
    }
}
