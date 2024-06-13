using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class MessageAttachment
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }

    public int? DocumentKind { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }
}
