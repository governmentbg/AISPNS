using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Object
{
    public Guid Id { get; set; }

    public Guid ObjectTypeId { get; set; }

    public Guid ObjectKindId { get; set; }

    public Guid? AddressId { get; set; }

    public string? Condition { get; set; }

    public string? Notes { get; set; }

    public string? Value { get; set; }

    public Guid AnnouncementId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Announcement Announcement { get; set; } = null!;

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomObjectKind ObjectKind { get; set; } = null!;

    public virtual NomObjectType ObjectType { get; set; } = null!;
}
