using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class NomCourseKind
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
