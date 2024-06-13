namespace AISTN.InternalAppAPI.Models.Filter
{
    public class SyndicSearchFilter
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Egn { get; set; }

        public Guid? SyndicStatusID { get; set; }

        public bool? Active { get; set; }

        public string? City { get; set; }

        public bool? IsCustodian { get; set; }
    }
}
