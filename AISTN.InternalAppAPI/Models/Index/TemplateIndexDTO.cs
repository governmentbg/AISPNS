namespace AISTN.InternalAppAPI.Models.Index
{
    public class TemplateIndexDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? TemplateKindName { get; set; }

        public Guid? DocumentContentID { get; set; }
    }
}
