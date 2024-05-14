namespace AISTN.PublicAppAPI.Models.Details
{
    public class DetailsPersonDTO
    {
        public Guid? Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
