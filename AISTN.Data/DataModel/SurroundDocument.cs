using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class SurroundDocument
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public Guid? ActionId { get; set; }

    public int Number { get; set; }

    public short Year { get; set; }

    public DateTime Date { get; set; }

    public string? Text { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual NomAction? Action { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual ICollection<Side> Sides { get; set; } = new List<Side>();
}
