using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Course
{
    public Guid Id { get; set; }

    public Guid ProgramId { get; set; }

    public string Topic { get; set; } = null!;

    public DateTime FromDate1 { get; set; }

    public DateTime ToDate1 { get; set; }

    public string? LengthDate1 { get; set; }

    public Guid? Address1 { get; set; }

    public DateTime? FromDate2 { get; set; }

    public DateTime? ToDate2 { get; set; }

    public string? LengthDate2 { get; set; }

    public Guid? Address2 { get; set; }

    public int? MaximumParticipants { get; set; }

    public string? Lecturers { get; set; }

    public string? SubTopics { get; set; }

    public DateTime? ExamDate1 { get; set; }

    public DateTime? ExamDate2 { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? CourseKindId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public virtual Address? Address1Navigation { get; set; }

    public virtual Address? Address2Navigation { get; set; }

    public virtual ICollection<CourseApplication> CourseApplicationAlternateCourses { get; set; } = new List<CourseApplication>();

    public virtual ICollection<CourseApplication> CourseApplicationCourses { get; set; } = new List<CourseApplication>();

    public virtual NomCourseKind? CourseKind { get; set; }

    public virtual ICollection<CourseResult> CourseResults { get; set; } = new List<CourseResult>();

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual Program Program { get; set; } = null!;
}
