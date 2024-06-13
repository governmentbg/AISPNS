namespace AISTN.PublicAppAPI.Models.Filters
{
    public class SyndicSearchFilter
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? City { get; set; }

        public bool? IsCustodian { get; set; }

        public Guid? Speciality { get; set; }
    }
}
