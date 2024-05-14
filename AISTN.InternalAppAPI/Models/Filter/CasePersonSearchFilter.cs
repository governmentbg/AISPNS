namespace AISTN.InternalAppAPI.Models.Filter
{
    public class CasePersonSearchFilter
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? EGN { get; set; }

        public bool? IsStabilization { get; set; }
    }
}
