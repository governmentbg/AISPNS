using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class Address
{
    public Guid Id { get; set; }

    public Guid? RegionId { get; set; }

    public Guid? MunicipalityId { get; set; }

    public Guid? SettlementId { get; set; }

    public string? PostCode { get; set; }

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? BuildingNumber { get; set; }

    public string? EntranceNumber { get; set; }

    public string? FloorNumber { get; set; }

    public string? ApartmentNumber { get; set; }

    public string? Additional { get; set; }

    public int? Ekkate { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? LastModified { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<Course> CourseAddress1Navigations { get; set; } = new List<Course>();

    public virtual ICollection<Course> CourseAddress2Navigations { get; set; } = new List<Course>();

    public virtual NomMunicipality? Municipality { get; set; }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual NomDistrict? Region { get; set; }

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual NomSettlement? Settlement { get; set; }

    public virtual ICollection<SignalSender> SignalSenders { get; set; } = new List<SignalSender>();

    public virtual ICollection<Syndic2Address> Syndic2Addresses { get; set; } = new List<Syndic2Address>();
}
