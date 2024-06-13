using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Syndic
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Number { get; set; }

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string? SecondLastName { get; set; }

    public string LastName { get; set; } = null!;

    public string Egn { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public Guid? Specialty { get; set; }

    public bool? Active { get; set; }

    public bool? IsCustodian { get; set; }

    public bool? Locked { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public byte[]? HashValue { get; set; }

    public Guid? SyndicStatusId { get; set; }

    public Guid? SyndicKindId { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Announcement> AnnouncementSecondSyndics { get; set; } = new List<Announcement>();

    public virtual ICollection<Announcement> AnnouncementSyndics { get; set; } = new List<Announcement>();

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual ICollection<CaseAssignment> CaseAssignments { get; set; } = new List<CaseAssignment>();

    public virtual ICollection<CourseApplication> CourseApplications { get; set; } = new List<CourseApplication>();

    public virtual ICollection<CourseParticipation> CourseParticipations { get; set; } = new List<CourseParticipation>();

    public virtual ICollection<CourseResult> CourseResults { get; set; } = new List<CourseResult>();

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Plea> Pleas { get; set; } = new List<Plea>();

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Signal> Signals { get; set; } = new List<Signal>();

    public virtual NomSpecialty? SpecialtyNavigation { get; set; }

    public virtual ICollection<Syndic2Address> Syndic2Addresses { get; set; } = new List<Syndic2Address>();

    public virtual NomSyndicKind? SyndicKind { get; set; }

    public virtual NomSyndicStatus? SyndicStatus { get; set; }

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Trustee> Trustees { get; set; } = new List<Trustee>();

    public virtual User? User { get; set; }
}
