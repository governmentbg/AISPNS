using System;
using System.Collections.Generic;

namespace AISTN.Data.DataModel;

public partial class DocumentCollection
{
    public Guid Id { get; set; }

    public Guid DocumentCollectionId { get; set; }

    public virtual ICollection<ActAnnouncement> ActAnnouncements { get; set; } = new List<ActAnnouncement>();

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<AdminTemplate> AdminTemplates { get; set; } = new List<AdminTemplate>();

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<DocumentContent> DocumentContents { get; set; } = new List<DocumentContent>();

    public virtual ICollection<DocumentLegalBasis> DocumentLegalBases { get; set; } = new List<DocumentLegalBasis>();

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();

    public virtual ICollection<Plea> Pleas { get; set; } = new List<Plea>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<RegisterEntry> RegisterEntries { get; set; } = new List<RegisterEntry>();

    public virtual ICollection<Report> ReportAttachedDocumentCollections { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportDocumentCollections { get; set; } = new List<Report>();

    public virtual ICollection<ReportTemplate> ReportTemplates { get; set; } = new List<ReportTemplate>();

    public virtual ICollection<Sample> SampleAttachedDocumentCollections { get; set; } = new List<Sample>();

    public virtual ICollection<Sample> SampleGeneratedDocumentCollections { get; set; } = new List<Sample>();

    public virtual ICollection<Signal> Signals { get; set; } = new List<Signal>();

    public virtual ICollection<StatTemplate> StatTemplates { get; set; } = new List<StatTemplate>();

    public virtual ICollection<StatisticalReport> StatisticalReports { get; set; } = new List<StatisticalReport>();

    public virtual ICollection<Syndic> Syndics { get; set; } = new List<Syndic>();

    public virtual ICollection<TemplateArticle28> TemplateArticle28s { get; set; } = new List<TemplateArticle28>();

    public virtual ICollection<TemplateDocument> TemplateDocuments { get; set; } = new List<TemplateDocument>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
