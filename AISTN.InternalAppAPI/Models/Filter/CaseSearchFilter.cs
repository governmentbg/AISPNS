namespace AISTN.InternalAppAPI.Models.Filter
{
    public class CaseSearchFilter
    {
        public int? CourtNumber { get; set; }

        public string? CaseNumber { get; set; }

        public short? CaseYear { get; set; }

        public string? EntityName { get; set; }

        public string? Bulstat { get; set; }

        public string? PersonName { get; set; }

        public string? EGN { get; set; }

        public bool? IsStabilization { get; set; }
    }
}
