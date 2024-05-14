using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class DocumentLegalBasis
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public bool? IsPublished { get; set; }

    public DateTime? Date { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }
}
