using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomCourt
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public int? CourtOfAppeal { get; set; }

    public string? Email { get; set; }

    public string? FaxNumber { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<ImportedDeed> ImportedDeeds { get; set; } = new List<ImportedDeed>();

    public virtual ICollection<OutgoingDocument> OutgoingDocuments { get; set; } = new List<OutgoingDocument>();
}
