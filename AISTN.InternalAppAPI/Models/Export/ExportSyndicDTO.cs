namespace AISTN.InternalAppAPI.Models.Export
{
    public class ExportSyndicDTO
    {
        public Guid Id { get; set; }

        public string? Number { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? FullAddress { get; set; }

        public string? Phone { get; set; }

        public string? SyndicSpecialty { get; set; }
    }
}
