namespace AISTN.ExternalAppAPI.Models.Filter
{
    public class CaseSearchFilter
    {
        public int? CourtNumber { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public bool? IsStabilization { get; set; }
    }
}
