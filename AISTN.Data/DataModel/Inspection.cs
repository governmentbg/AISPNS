using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Inspection
{
    public Guid Id { get; set; }

    public DateTime InspectionDate { get; set; }

    public Guid SyndicId { get; set; }

    public string InspectorName { get; set; } = null!;

    public Guid InspectionTypeId { get; set; }

    public string? InspectionOrderNumber { get; set; }

    public DateTime? InspectionOrderDate { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletionTime { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomInspectionType InspectionType { get; set; } = null!;

    public virtual Syndic Syndic { get; set; } = null!;
}
