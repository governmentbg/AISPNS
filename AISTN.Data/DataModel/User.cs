using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? LastModified { get; set; }

    public int? PersonId { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? LastName { get; set; }

    public string? Egn { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<ActAnnouncement> ActAnnouncements { get; set; } = new List<ActAnnouncement>();

    public virtual ICollection<Announcement> AnnouncementPublishedByNavigations { get; set; } = new List<Announcement>();

    public virtual ICollection<Announcement> AnnouncementSendForCorrectionByNavigations { get; set; } = new List<Announcement>();

    public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();

    public virtual ICollection<Receiver> Receivers { get; set; } = new List<Receiver>();

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual ICollection<Sender> Senders { get; set; } = new List<Sender>();

    public virtual ICollection<Syndic> Syndics { get; set; } = new List<Syndic>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
