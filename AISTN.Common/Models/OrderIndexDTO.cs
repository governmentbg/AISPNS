namespace AISTN.Common.Models
{
    public class OrderIndexDTO
    {
        public Guid Id { get; set; }

        public string? OrderKindName { get; set; }

        public string? Number { get; set; }

        public DateTime Date { get; set; }

        public string? StateGazetteEdition { get; set; }

        public string? StateGazetteYear { get; set; }

        public string? Description { get; set; }

        public bool? IsExclusion { get; set; }
    }
}
