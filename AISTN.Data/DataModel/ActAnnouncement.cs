using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class ActAnnouncement
{
    public Guid Id { get; set; }

    public Guid? ActCategoryId { get; set; }

    public Guid? ActId { get; set; }

    public string? Description { get; set; }

    public DateTime? AnnouncedDate { get; set; }

    public Guid? AnnouncedByUserId { get; set; }

    public bool? IsEnforcedImmediately { get; set; }

    public int? AppealTerm { get; set; }

    public string? Number { get; set; }

    public DateTime? Date { get; set; }

    public Guid? AnnouncementStatusId { get; set; }

    public Guid? RegistrationStatusId { get; set; }

    public Guid? DocumentCollectionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public virtual Act? Act { get; set; }

    public virtual NomActAnnouncementCategory? ActCategory { get; set; }

    public virtual User? AnnouncedByUser { get; set; }

    public virtual NomActAnnouncementStatus? AnnouncementStatus { get; set; }

    public virtual DocumentCollection? DocumentCollection { get; set; }

    public virtual NomActAnnouncementStatus? RegistrationStatus { get; set; }
}
