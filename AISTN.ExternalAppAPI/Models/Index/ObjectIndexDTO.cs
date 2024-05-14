using AISTN.Common.Models;
using AISTN.Data.DataModel;

namespace AISTN.ExternalAppAPI.Models.Index
{
    public class ObjectIndexDTO
    {
        public Guid? Id { get; set; }

        public string? ObjectType { get; set; }

        public string? ObjectKind { get; set; }

        public string? FullAddress { get; set; }

        public string? Condition { get; set; }

        public string? Value { get; set; }

    }
}
