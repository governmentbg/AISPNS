using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Act
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public int? Action { get; set; }

    public Guid ActKindId { get; set; }

    public Guid? ActCategoryId { get; set; }

    public Guid? ActContractorId { get; set; }

    public string? Number { get; set; }

    public DateTime Date { get; set; }

    public Guid? ActReasonId { get; set; }

    public short? DebtorStatus { get; set; }

    public string? Text { get; set; }

    public byte[]? Image { get; set; }

    public byte[]? OriginalLetterImage { get; set; }

    public byte[]? RedactedLetterImage { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<ActAnnouncement> ActAnnouncements { get; set; } = new List<ActAnnouncement>();

    public virtual NomActCategory? ActCategory { get; set; }

    public virtual NomActContractor? ActContractor { get; set; }

    public virtual NomActKind ActKind { get; set; } = null!;

    public virtual NomActReason? ActReason { get; set; }

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual Session Session { get; set; } = null!;
}
