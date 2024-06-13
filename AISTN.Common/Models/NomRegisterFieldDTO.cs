namespace AISTN.Common.Models
{
    public class NomRegisterFieldDTO
    {
        public Guid? Id { get; set; }

        public int? Code { get; set; }

        public string? Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public bool? IsStabilization { get; set; }
    }
}
