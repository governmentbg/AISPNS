using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class CourseParticipation
{
    public Guid Id { get; set; }

    public Guid CourseApplicationId { get; set; }

    public Guid SyndicId { get; set; }

    public bool? EnrolledDate1 { get; set; }

    public bool? EnrolledDate2 { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public bool? PassedTheCourse { get; set; }

    public virtual CourseApplication CourseApplication { get; set; } = null!;

    public virtual Syndic Syndic { get; set; } = null!;
}
