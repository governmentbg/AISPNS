using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Property
{
    public Guid Id { get; set; }

    public Guid? CaseId { get; set; }

    public Guid? PropertyClassId { get; set; }

    public Guid? PropertyTypeId { get; set; }

    public Guid? PropertyKindId { get; set; }

    public Guid? AddressId { get; set; }

    public string? State { get; set; }

    public string? Notes { get; set; }

    public string? Value { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public Guid? PersonId { get; set; }

    public Guid? EntityId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Case? Case { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual Entity? Entity { get; set; }

    public virtual Person? Person { get; set; }

    public virtual NomPropertyClass? PropertyClass { get; set; }

    public virtual NomObjectKind? PropertyKind { get; set; }

    public virtual NomObjectType? PropertyType { get; set; }
}
