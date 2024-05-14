using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class CourseResult
{
    public Guid Id { get; set; }

    public bool? Completed { get; set; }

    public Guid CourseId { get; set; }

    public Guid SyndicId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Syndic Syndic { get; set; } = null!;
}
