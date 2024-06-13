namespace AISTN.InternalAppAPI.Models.Filter
{
    public class SyndicTemplateFilter
    {
        public Guid? TemplateKindId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
