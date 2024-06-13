using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Program
{
    public Guid Id { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? MojorderNumber { get; set; }

    public DateTime? MojorderDate { get; set; }

    public string? MoeorderNumber { get; set; }

    public DateTime? MoeorderDate { get; set; }

    public string? Description { get; set; }

    public string? AdditionalDescription { get; set; }

    public string? Notes { get; set; }

    public bool? Published { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool? IsEnrolled { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
