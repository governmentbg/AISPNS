using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Judge
{
    public Guid Id { get; set; }

    public Guid? SessionId { get; set; }

    public Guid CaseId { get; set; }

    public string? Name { get; set; }

    public string? Rename { get; set; }

    public string? Family { get; set; }

    public string? Egn { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Session? Session { get; set; }
}
