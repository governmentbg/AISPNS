using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AISTN.Data.DataModel;

public partial class AistnContext : DbContext
{
    public AistnContext(DbContextOptions<AistnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Act> Acts { get; set; }

    public virtual DbSet<ActAnnouncement> ActAnnouncements { get; set; }

    public virtual DbSet<ActDetail> ActDetails { get; set; }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AdminTemplate> AdminTemplates { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Appeal> Appeals { get; set; }

    public virtual DbSet<Blob> Blobs { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseAssignment> CaseAssignments { get; set; }

    public virtual DbSet<CompaniesRegistration> CompaniesRegistrations { get; set; }

    public virtual DbSet<ContactForm> ContactForms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseApplication> CourseApplications { get; set; }

    public virtual DbSet<CourseParticipation> CourseParticipations { get; set; }

    public virtual DbSet<CourseResult> CourseResults { get; set; }

    public virtual DbSet<DocumentCollection> DocumentCollections { get; set; }

    public virtual DbSet<DocumentContent> DocumentContents { get; set; }

    public virtual DbSet<DocumentLegalBasis> DocumentLegalBases { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<EntityStatisticalDatum> EntityStatisticalData { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<ImportRequest> ImportRequests { get; set; }

    public virtual DbSet<ImportedDeed> ImportedDeeds { get; set; }

    public virtual DbSet<IncomingDocument> IncomingDocuments { get; set; }

    public virtual DbSet<Inspection> Inspections { get; set; }

    public virtual DbSet<Installment> Installments { get; set; }

    public virtual DbSet<Judge> Judges { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageReceiver> MessageReceivers { get; set; }

    public virtual DbSet<NomActAnnouncementCategory> NomActAnnouncementCategories { get; set; }

    public virtual DbSet<NomActAnnouncementStatus> NomActAnnouncementStatuses { get; set; }

    public virtual DbSet<NomActCategory> NomActCategories { get; set; }

    public virtual DbSet<NomActContractor> NomActContractors { get; set; }

    public virtual DbSet<NomActContractorDatum> NomActContractorData { get; set; }

    public virtual DbSet<NomActKind> NomActKinds { get; set; }

    public virtual DbSet<NomActReason> NomActReasons { get; set; }

    public virtual DbSet<NomAction> NomActions { get; set; }

    public virtual DbSet<NomActivityKind> NomActivityKinds { get; set; }

    public virtual DbSet<NomAnnouncementStatus> NomAnnouncementStatuses { get; set; }

    public virtual DbSet<NomAppealKind> NomAppealKinds { get; set; }

    public virtual DbSet<NomAttachedDocumentKind> NomAttachedDocumentKinds { get; set; }

    public virtual DbSet<NomCaseCode> NomCaseCodes { get; set; }

    public virtual DbSet<NomCaseKind> NomCaseKinds { get; set; }

    public virtual DbSet<NomCompanySize> NomCompanySizes { get; set; }

    public virtual DbSet<NomCourseKind> NomCourseKinds { get; set; }

    public virtual DbSet<NomCourt> NomCourts { get; set; }

    public virtual DbSet<NomDebtorStatus> NomDebtorStatuses { get; set; }

    public virtual DbSet<NomDirectiveTemplateKind> NomDirectiveTemplateKinds { get; set; }

    public virtual DbSet<NomDistrict> NomDistricts { get; set; }

    public virtual DbSet<NomIncomingDocumentKind> NomIncomingDocumentKinds { get; set; }

    public virtual DbSet<NomInspectionType> NomInspectionTypes { get; set; }

    public virtual DbSet<NomInstallmentKind> NomInstallmentKinds { get; set; }

    public virtual DbSet<NomInstallmentYear> NomInstallmentYears { get; set; }

    public virtual DbSet<NomInvolvementKind> NomInvolvementKinds { get; set; }

    public virtual DbSet<NomLegalBasis> NomLegalBases { get; set; }

    public virtual DbSet<NomMunicipality> NomMunicipalities { get; set; }

    public virtual DbSet<NomObjectKind> NomObjectKinds { get; set; }

    public virtual DbSet<NomObjectType> NomObjectTypes { get; set; }

    public virtual DbSet<NomOfferingKind> NomOfferingKinds { get; set; }

    public virtual DbSet<NomOfferingLocationType> NomOfferingLocationTypes { get; set; }

    public virtual DbSet<NomOfferingProcedure> NomOfferingProcedures { get; set; }

    public virtual DbSet<NomOrderKind> NomOrderKinds { get; set; }

    public virtual DbSet<NomOrderPaymentKind> NomOrderPaymentKinds { get; set; }

    public virtual DbSet<NomPapersForSale> NomPapersForSales { get; set; }

    public virtual DbSet<NomPropertyClass> NomPropertyClasses { get; set; }

    public virtual DbSet<NomRegisterField> NomRegisterFields { get; set; }

    public virtual DbSet<NomRegisterSyndicKind> NomRegisterSyndicKinds { get; set; }

    public virtual DbSet<NomRegistrationLegalBasis> NomRegistrationLegalBases { get; set; }

    public virtual DbSet<NomReportSource> NomReportSources { get; set; }

    public virtual DbSet<NomReportType> NomReportTypes { get; set; }

    public virtual DbSet<NomSalesProcedure> NomSalesProcedures { get; set; }

    public virtual DbSet<NomSampleKind> NomSampleKinds { get; set; }

    public virtual DbSet<NomSellAnnouncementTemplate> NomSellAnnouncementTemplates { get; set; }

    public virtual DbSet<NomSendToKind> NomSendToKinds { get; set; }

    public virtual DbSet<NomSessionKind> NomSessionKinds { get; set; }

    public virtual DbSet<NomSessionResult> NomSessionResults { get; set; }

    public virtual DbSet<NomSettlement> NomSettlements { get; set; }

    public virtual DbSet<NomSignalDocumentKind> NomSignalDocumentKinds { get; set; }

    public virtual DbSet<NomSignalSenderType> NomSignalSenderTypes { get; set; }

    public virtual DbSet<NomSpecialty> NomSpecialties { get; set; }

    public virtual DbSet<NomSyndicAction> NomSyndicActions { get; set; }

    public virtual DbSet<NomSyndicCaseReport> NomSyndicCaseReports { get; set; }

    public virtual DbSet<NomSyndicKind> NomSyndicKinds { get; set; }

    public virtual DbSet<NomSyndicReportType> NomSyndicReportTypes { get; set; }

    public virtual DbSet<NomSyndicStatus> NomSyndicStatuses { get; set; }

    public virtual DbSet<NomTemplateKind> NomTemplateKinds { get; set; }

    public virtual DbSet<NomTractType> NomTractTypes { get; set; }

    public virtual DbSet<Object> Objects { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OutgoingDocument> OutgoingDocuments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Plea> Pleas { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Receiver> Receivers { get; set; }

    public virtual DbSet<RegisterEntry> RegisterEntries { get; set; }

    public virtual DbSet<RegixFailedLogin> RegixFailedLogins { get; set; }

    public virtual DbSet<RegixSoaprequest> RegixSoaprequests { get; set; }

    public virtual DbSet<RegixSoapsession> RegixSoapsessions { get; set; }

    public virtual DbSet<RegixSoapuser> RegixSoapusers { get; set; }

    public virtual DbSet<RegixUnrecognizedSession> RegixUnrecognizedSessions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportTemplate> ReportTemplates { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sample> Samples { get; set; }

    public virtual DbSet<Sender> Senders { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Session1> Session1s { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Side> Sides { get; set; }

    public virtual DbSet<Signal> Signals { get; set; }

    public virtual DbSet<SignalSender> SignalSenders { get; set; }

    public virtual DbSet<StatTemplate> StatTemplates { get; set; }

    public virtual DbSet<StatisticalReport> StatisticalReports { get; set; }

    public virtual DbSet<SurroundDocument> SurroundDocuments { get; set; }

    public virtual DbSet<Syndic> Syndics { get; set; }

    public virtual DbSet<Syndic2Address> Syndic2Addresses { get; set; }

    public virtual DbSet<TempSession> TempSessions { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<TemplateArticle28> TemplateArticle28s { get; set; }

    public virtual DbSet<TemplateDocument> TemplateDocuments { get; set; }

    public virtual DbSet<Trustee> Trustees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Act>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Act", "caseinfo", tb => tb.HasTrigger("trg_InsertActAnnouncement"));

            entity.HasIndex(e => e.DateCreated, "IX_DateCreated").IsClustered();

            entity.HasIndex(e => e.SessionId, "IX_SessionId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Number).HasMaxLength(20);

            entity.HasOne(d => d.ActCategory).WithMany(p => p.Acts)
                .HasForeignKey(d => d.ActCategoryId)
                .HasConstraintName("FK_Act_nom_ActCategory");

            entity.HasOne(d => d.ActContractor).WithMany(p => p.Acts)
                .HasForeignKey(d => d.ActContractorId)
                .HasConstraintName("FK_Act_nom_ActContractor");

            entity.HasOne(d => d.ActKind).WithMany(p => p.Acts)
                .HasForeignKey(d => d.ActKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Act_nom_ActKind");

            entity.HasOne(d => d.ActReason).WithMany(p => p.Acts)
                .HasForeignKey(d => d.ActReasonId)
                .HasConstraintName("FK_Act_nom_ActReason");

            entity.HasOne(d => d.Session).WithMany(p => p.Acts)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Act_Session");
        });

        modelBuilder.Entity<ActAnnouncement>(entity =>
        {
            entity.ToTable("ActAnnouncement", "registration");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AnnouncedByUserId).HasColumnName("AnnouncedByUserID");
            entity.Property(e => e.AnnouncementStatusId).HasColumnName("AnnouncementStatusID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.RegistrationStatusId).HasColumnName("RegistrationStatusID");

            entity.HasOne(d => d.ActCategory).WithMany(p => p.ActAnnouncements)
                .HasForeignKey(d => d.ActCategoryId)
                .HasConstraintName("FK_ActAnnouncement_nom_ActAnnouncementCategory");

            entity.HasOne(d => d.Act).WithMany(p => p.ActAnnouncements)
                .HasForeignKey(d => d.ActId)
                .HasConstraintName("FK_ActAnnouncement_Act");

            entity.HasOne(d => d.AnnouncedByUser).WithMany(p => p.ActAnnouncements)
                .HasForeignKey(d => d.AnnouncedByUserId)
                .HasConstraintName("FK_ActAnnouncement_User");

            entity.HasOne(d => d.AnnouncementStatus).WithMany(p => p.ActAnnouncementAnnouncementStatuses)
                .HasForeignKey(d => d.AnnouncementStatusId)
                .HasConstraintName("FK_ActAnnouncement_nom_ActAnnouncementStatus");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.ActAnnouncements)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_ActAnnouncement_DocumentCollection");

            entity.HasOne(d => d.RegistrationStatus).WithMany(p => p.ActAnnouncementRegistrationStatuses)
                .HasForeignKey(d => d.RegistrationStatusId)
                .HasConstraintName("FK_ActAnnouncement_nom_ActAnnouncementStatus1");
        });

        modelBuilder.Entity<ActDetail>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("ActDetails", "tr");

            entity.HasIndex(e => e.ImportedDate, "IX_ActDetails").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.ActContainerType).HasMaxLength(100);
            entity.Property(e => e.Date).HasMaxLength(50);
            entity.Property(e => e.DeedId).HasColumnName("DeedID");
            entity.Property(e => e.ImportedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.Title).HasMaxLength(500);

            entity.HasOne(d => d.Deed).WithMany(p => p.ActDetails)
                .HasForeignKey(d => d.DeedId)
                .HasConstraintName("FK_ActDetails_ImportedDeed");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activity", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActivityKindId).HasColumnName("ActivityKindID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.ActivityKind).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_nom_ActivityKind");

            entity.HasOne(d => d.Case).WithMany(p => p.Activities)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Case");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Activities)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Activity_DocumentCollection");

            entity.HasOne(d => d.Report).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK_Activity_Report");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Activities)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_Syndic");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address", "address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.ApartmentNumber).HasMaxLength(50);
            entity.Property(e => e.BuildingNumber).HasMaxLength(50);
            entity.Property(e => e.Ekkate).HasColumnName("EKKATE");
            entity.Property(e => e.EntranceNumber).HasMaxLength(50);
            entity.Property(e => e.FloorNumber).HasMaxLength(50);
            entity.Property(e => e.MunicipalityId).HasColumnName("MunicipalityID");
            entity.Property(e => e.PostCode).HasMaxLength(200);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.SettlementId).HasColumnName("SettlementID");
            entity.Property(e => e.StreetName).HasMaxLength(200);
            entity.Property(e => e.StreetNumber).HasMaxLength(50);

            entity.HasOne(d => d.Municipality).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.MunicipalityId)
                .HasConstraintName("FK_Address_nom_Municipalities");

            entity.HasOne(d => d.Region).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Address_nom_Districts");

            entity.HasOne(d => d.Settlement).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.SettlementId)
                .HasConstraintName("FK_Address_nom_Settlements");
        });

        modelBuilder.Entity<AdminTemplate>(entity =>
        {
            entity.ToTable("AdminTemplate", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.TemplateKindId).HasColumnName("TemplateKindID");
            entity.Property(e => e.TemplateName).HasMaxLength(200);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.AdminTemplates)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_AdminTemplate_DocumentCollection");

            entity.HasOne(d => d.TemplateKind).WithMany(p => p.AdminTemplates)
                .HasForeignKey(d => d.TemplateKindId)
                .HasConstraintName("FK_AdminTemplate_nom_TemplateKind");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.ToTable("Announcement", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Awarded).HasMaxLength(400);
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CaseNumber).HasMaxLength(20);
            entity.Property(e => e.CoownershipSale).HasMaxLength(400);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DebtorEik)
                .HasMaxLength(20)
                .HasColumnName("DebtorEIK");
            entity.Property(e => e.DebtorName).HasMaxLength(300);
            entity.Property(e => e.DeterminationFollowupBuyers).HasMaxLength(400);
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LegalBasisId).HasColumnName("LegalBasisID");
            entity.Property(e => e.LocationTypeId).HasColumnName("LocationTypeID");
            entity.Property(e => e.MortgagedPropertySale).HasMaxLength(400);
            entity.Property(e => e.OfferingKindId).HasColumnName("OfferingKindID");
            entity.Property(e => e.OfferingProcedureId).HasColumnName("OfferingProcedureID");
            entity.Property(e => e.PapersForSaleId).HasColumnName("PapersForSaleID");
            entity.Property(e => e.ParticipationLimitations).HasMaxLength(400);
            entity.Property(e => e.PricePaymentTerms).HasMaxLength(200);
            entity.Property(e => e.Retainer).HasMaxLength(300);
            entity.Property(e => e.RiskTransfer).HasMaxLength(400);
            entity.Property(e => e.SalesProcedureId).HasColumnName("SalesProcedureID");
            entity.Property(e => e.SecondSyndicId).HasColumnName("SecondSyndicID");
            entity.Property(e => e.StartingPrice).HasMaxLength(300);
            entity.Property(e => e.StatusCode).HasDefaultValue(1);
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Address).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Announcement_Address");

            entity.HasOne(d => d.Case).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Announcement2Case");

            entity.HasOne(d => d.Court).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.CourtId)
                .HasConstraintName("FK_Announcement_nom_Court");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Announcements)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Announcement_DocumentCollection");

            entity.HasOne(d => d.LegalBasis).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.LegalBasisId)
                .HasConstraintName("FK_Announcement2LegalBasis");

            entity.HasOne(d => d.LocationType).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.LocationTypeId)
                .HasConstraintName("FK_Announcement2OfferingLocationType");

            entity.HasOne(d => d.OfferingKind).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.OfferingKindId)
                .HasConstraintName("FK_Announcement2OfferingKind");

            entity.HasOne(d => d.OfferingProcedure).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.OfferingProcedureId)
                .HasConstraintName("FK_Announcement2OfferingProcedure");

            entity.HasOne(d => d.PapersForSale).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.PapersForSaleId)
                .HasConstraintName("FK_Announcement2PapersForSale");

            entity.HasOne(d => d.PublishedByNavigation).WithMany(p => p.AnnouncementPublishedByNavigations)
                .HasForeignKey(d => d.PublishedBy)
                .HasConstraintName("FK_Announcement_User");

            entity.HasOne(d => d.SalesProcedure).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.SalesProcedureId)
                .HasConstraintName("FK_Announcement2SalesProcedure");

            entity.HasOne(d => d.SecondSyndic).WithMany(p => p.AnnouncementSecondSyndics)
                .HasForeignKey(d => d.SecondSyndicId)
                .HasConstraintName("FK_Announcement2SecondSyndic");

            entity.HasOne(d => d.SendForCorrectionByNavigation).WithMany(p => p.AnnouncementSendForCorrectionByNavigations)
                .HasForeignKey(d => d.SendForCorrectionBy)
                .HasConstraintName("FK_Announcement_User1");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Announcements)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Announcement2AnnouncementStatus");

            entity.HasOne(d => d.Syndic).WithMany(p => p.AnnouncementSyndics)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Announcement2Syndic");
        });

        modelBuilder.Entity<Appeal>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Appeal", "caseinfo");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActId).HasColumnName("ActID");
            entity.Property(e => e.AppealKindId).HasColumnName("AppealKindID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Act).WithMany(p => p.Appeals)
                .HasForeignKey(d => d.ActId)
                .HasConstraintName("FK_Appeal_Act");

            entity.HasOne(d => d.AppealKind).WithMany(p => p.Appeals)
                .HasForeignKey(d => d.AppealKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appeal_nom_AppealKind");

            entity.HasOne(d => d.Order).WithMany(p => p.Appeals)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Appeal_Order");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Appeals)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Appeal_Syndic");
        });

        modelBuilder.Entity<Blob>(entity =>
        {
            entity.ToTable("Blob", "document");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_Case_1")
                .IsClustered(false);

            entity.ToTable("Case", "caseinfo");

            entity.HasIndex(e => new { e.Number, e.CourtId, e.Year }, "UX_Number_Year_Court").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CaseCodeId).HasColumnName("CaseCodeID");
            entity.Property(e => e.CaseKindId).HasColumnName("CaseKindID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsStabilization).HasDefaultValue(false);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.StatCode).HasMaxLength(20);
            entity.Property(e => e.SyndicCaseReportId).HasColumnName("SyndicCaseReportID");

            entity.HasOne(d => d.CaseCode).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CaseCodeId)
                .HasConstraintName("FK_Case_nom_CaseCode");

            entity.HasOne(d => d.CaseKind).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CaseKindId)
                .HasConstraintName("FK_Case_nom_CaseKind");

            entity.HasOne(d => d.Court).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Case_nom_Court");

            entity.HasOne(d => d.SyndicCaseReport).WithMany(p => p.Cases)
                .HasForeignKey(d => d.SyndicCaseReportId)
                .HasConstraintName("FK_Case_nom_SyndicCaseReport");
        });

        modelBuilder.Entity<CaseAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CaseAssignment", "syndic");

            entity.HasIndex(e => e.Id, "IX_Syndic2Case").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Case).WithMany(p => p.CaseAssignments)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CaseAssignment_Case");

            entity.HasOne(d => d.Syndic).WithMany(p => p.CaseAssignments)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CaseAssignment_Syndic");
        });

        modelBuilder.Entity<CompaniesRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CompaniesRegistration", "tr");

            entity.HasIndex(e => e.Bulstat, "IX_CompaniesRegistration_Bulstat");

            entity.HasIndex(e => e.FieldActionDate, "IX_CompaniesRegistration_FieldActionDate");

            entity.HasIndex(e => e.ImportedDate, "IX_CompaniesRegistration_ImportedDate").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Apartment).HasMaxLength(500);
            entity.Property(e => e.Area).HasMaxLength(1000);
            entity.Property(e => e.AreaEkatte).HasMaxLength(50);
            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.Block).HasMaxLength(500);
            entity.Property(e => e.Bulstat).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(2000);
            entity.Property(e => e.Country).HasMaxLength(1000);
            entity.Property(e => e.CountryCode).HasMaxLength(50);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.District).HasMaxLength(1000);
            entity.Property(e => e.DistrictEkatte).HasMaxLength(50);
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.Entrance).HasMaxLength(1000);
            entity.Property(e => e.Floor).HasMaxLength(500);
            entity.Property(e => e.ForeignPlace).HasMaxLength(1000);
            entity.Property(e => e.HousingEstate).HasMaxLength(1000);
            entity.Property(e => e.ImportRequestId).HasColumnName("ImportRequestID");
            entity.Property(e => e.ImportedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LegalForm).HasMaxLength(50);
            entity.Property(e => e.Municipality).HasMaxLength(1000);
            entity.Property(e => e.MunicipalityEkatte).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(50);
            entity.Property(e => e.Settlement).HasMaxLength(1000);
            entity.Property(e => e.SettlementEkatte)
                .HasMaxLength(50)
                .HasColumnName("SettlementEKATTE");
            entity.Property(e => e.SettlementId).HasColumnName("SettlementID");
            entity.Property(e => e.Street).HasMaxLength(1000);
            entity.Property(e => e.StreetNumber).HasMaxLength(500);

            entity.HasOne(d => d.ImportRequest).WithMany(p => p.CompaniesRegistrations)
                .HasForeignKey(d => d.ImportRequestId)
                .HasConstraintName("FK_CompaniesRegistration_ImportRequest");
        });

        modelBuilder.Entity<ContactForm>(entity =>
        {
            entity.ToTable("ContactForm");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Course", "training");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CourseKindId).HasColumnName("CourseKindID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.LengthDate1).HasMaxLength(200);
            entity.Property(e => e.LengthDate2).HasMaxLength(200);

            entity.HasOne(d => d.Address1Navigation).WithMany(p => p.CourseAddress1Navigations)
                .HasForeignKey(d => d.Address1)
                .HasConstraintName("FK_Course_Address");

            entity.HasOne(d => d.Address2Navigation).WithMany(p => p.CourseAddress2Navigations)
                .HasForeignKey(d => d.Address2)
                .HasConstraintName("FK_Course_Address1");

            entity.HasOne(d => d.CourseKind).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CourseKindId)
                .HasConstraintName("FK_Course_nom_CourseKind");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Courses)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Course_DocumentCollection");

            entity.HasOne(d => d.Program).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Program");
        });

        modelBuilder.Entity<CourseApplication>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CourseApplication", "training");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActualEmail).HasMaxLength(200);
            entity.Property(e => e.ActualPhone).HasMaxLength(200);
            entity.Property(e => e.AlternateCourseDate2).HasComment("TRUE then alternate couse first date are chosen/FALSE then alternate course second dates are chosen");
            entity.Property(e => e.CourseDate1).HasComment("TRUE then couse first date are chosen/FALSE then course second dates are chosen");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AlternateCourse).WithMany(p => p.CourseApplicationAlternateCourses)
                .HasForeignKey(d => d.AlternateCourseId)
                .HasConstraintName("FK_CourseApplication_Course1");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseApplicationCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseApplication_Course");

            entity.HasOne(d => d.Syndic).WithMany(p => p.CourseApplications)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseApplication_Syndic");
        });

        modelBuilder.Entity<CourseParticipation>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CourseParticipation", "training");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CourseApplicationId).HasColumnName("CourseApplicationID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CourseApplication).WithMany(p => p.CourseParticipations)
                .HasForeignKey(d => d.CourseApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseParticipation_CourseApplication");

            entity.HasOne(d => d.Syndic).WithMany(p => p.CourseParticipations)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseParticipation_Syndic");
        });

        modelBuilder.Entity<CourseResult>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CourseResult", "training");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseResults)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseResult_Course");

            entity.HasOne(d => d.Syndic).WithMany(p => p.CourseResults)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseResult_Syndic");
        });

        modelBuilder.Entity<DocumentCollection>(entity =>
        {
            entity.ToTable("DocumentCollection", "document");

            entity.HasIndex(e => e.DocumentCollectionId, "IX_DocumentCollectionID").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DocumentCollectionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DocumentCollectionID");
        });

        modelBuilder.Entity<DocumentContent>(entity =>
        {
            entity.ToTable("DocumentContent", "document");

            entity.HasIndex(e => e.DocumentCollectionId, "IX_DocumentCollectionID");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.AttachedDocumentKindId).HasColumnName("AttachedDocumentKindID");
            entity.Property(e => e.BlobId).HasColumnName("BlobID");
            entity.Property(e => e.ContentMimeType)
                .HasMaxLength(200)
                .HasComment("Тип на файл");
            entity.Property(e => e.Description).HasComment("Описание");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.DocumentDate).HasComment("Дата на документ");
            entity.Property(e => e.FileName)
                .HasMaxLength(250)
                .HasComment("Име на файл");
            entity.Property(e => e.FileSize).HasComment("Размер на файл");
            entity.Property(e => e.SignalDocumentKindId).HasColumnName("SignalDocumentKindID");

            entity.HasOne(d => d.AttachedDocumentKind).WithMany(p => p.DocumentContents)
                .HasForeignKey(d => d.AttachedDocumentKindId)
                .HasConstraintName("FK_DocumentContent_nom_AttachedDocumentKind");

            entity.HasOne(d => d.Blob).WithMany(p => p.DocumentContents)
                .HasForeignKey(d => d.BlobId)
                .HasConstraintName("FK_DocumentContent_Blob");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.DocumentContents)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_DocumentContent_DocumentCollection");

            entity.HasOne(d => d.SignalDocumentKind).WithMany(p => p.DocumentContents)
                .HasForeignKey(d => d.SignalDocumentKindId)
                .HasConstraintName("FK_DocumentContent_nom_SignalDocumentKind");
        });

        modelBuilder.Entity<DocumentLegalBasis>(entity =>
        {
            entity.ToTable("DocumentLegalBasis", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.IsPublished).HasColumnName("isPublished");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.DocumentLegalBases)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_DocumentLegalBasis_DocumentCollection");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Entity", "caseinfo");

            entity.HasIndex(e => e.Bulstat, "IX_Bulstat");

            entity.HasIndex(e => e.DateCreated, "IX_DateCreated").IsClustered();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Bulstat).HasMaxLength(15);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<EntityStatisticalDatum>(entity =>
        {
            entity.ToTable("EntityStatisticalData", "caseinfo");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CompanySize).WithMany(p => p.EntityStatisticalData)
                .HasForeignKey(d => d.CompanySizeId)
                .HasConstraintName("FK_EntityStatisticalData_nom_CompanySize");

            entity.HasOne(d => d.Entity).WithMany(p => p.EntityStatisticalData)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityStatisticalData_Entity");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Experience", "syndic");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.RemovalGrounds).HasMaxLength(400);
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Case).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Experience_Case");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Experience_Syndic");
        });

        modelBuilder.Entity<ImportRequest>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("ImportRequest", "tr");

            entity.HasIndex(e => e.ImportDate, "IX_ImportRequest_Date").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Checksum)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Filename).HasMaxLength(500);
            entity.Property(e => e.ImportDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<ImportedDeed>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("ImportedDeed", "tr");

            entity.HasIndex(e => e.CaseId, "IX_ImportedDeed_CaseID");

            entity.HasIndex(e => e.ImportDate, "IX_ImportedDeed_Date").IsClustered();

            entity.HasIndex(e => e.DeedGuid, "IX_ImportedDeed_DeedGUID");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ActNumber).HasMaxLength(200);
            entity.Property(e => e.ActType).HasMaxLength(50);
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CaseNumber).HasMaxLength(200);
            entity.Property(e => e.CompanyLegalForm).HasMaxLength(200);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyUic)
                .HasMaxLength(20)
                .HasColumnName("CompanyUIC");
            entity.Property(e => e.CourtofAppealId).HasColumnName("CourtofAppealID");
            entity.Property(e => e.DeedGuid)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DeedGUID");
            entity.Property(e => e.DeedRawXml)
                .HasColumnType("xml")
                .HasColumnName("DeedRawXML");
            entity.Property(e => e.FieldEntryNumber).HasMaxLength(100);
            entity.Property(e => e.ImportDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ImportRequestId).HasColumnName("ImportRequestID");

            entity.HasOne(d => d.Case).WithMany(p => p.ImportedDeeds)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_ImportedDeed_Case");

            entity.HasOne(d => d.CourtofAppeal).WithMany(p => p.ImportedDeeds)
                .HasForeignKey(d => d.CourtofAppealId)
                .HasConstraintName("FK_ImportedDeed_nom_Court");

            entity.HasOne(d => d.ImportRequest).WithMany(p => p.ImportedDeeds)
                .HasForeignKey(d => d.ImportRequestId)
                .HasConstraintName("FK_ImportedDeed_ImportRequest");
        });

        modelBuilder.Entity<IncomingDocument>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("IncomingDocument", "caseinfo");

            entity.HasIndex(e => e.CaseId, "IX_CaseId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Case).WithMany(p => p.IncomingDocuments)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_IncomingDocument_Case");

            entity.HasOne(d => d.Kind).WithMany(p => p.IncomingDocuments)
                .HasForeignKey(d => d.KindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IncomingDocument_nom_IncomingDocumentKind");
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Inspection", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.InspectionOrderNumber).HasMaxLength(20);
            entity.Property(e => e.InspectionTypeId).HasColumnName("InspectionTypeID");
            entity.Property(e => e.InspectorName).HasMaxLength(100);
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Inspections)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Inspection_DocumentCollection");

            entity.HasOne(d => d.InspectionType).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.InspectionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inspection2InspectionType");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inspection2Syndic");
        });

        modelBuilder.Entity<Installment>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Installment", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bank).HasMaxLength(401);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.InstallmentKindId).HasColumnName("InstallmentKindID");
            entity.Property(e => e.InstallmentYearId).HasColumnName("InstallmentYearID");
            entity.Property(e => e.Number).HasMaxLength(200);
            entity.Property(e => e.PaymentAccessCode).HasMaxLength(50);
            entity.Property(e => e.PaymentRequestId)
                .HasMaxLength(50)
                .HasColumnName("PaymentRequestID");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Installments)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Installment_DocumentCollection");

            entity.HasOne(d => d.InstallmentKind).WithMany(p => p.Installments)
                .HasForeignKey(d => d.InstallmentKindId)
                .HasConstraintName("FK_Installment_nom_InstallmentKind");

            entity.HasOne(d => d.InstallmentYear).WithMany(p => p.Installments)
                .HasForeignKey(d => d.InstallmentYearId)
                .HasConstraintName("FK_Installment_nom_InstallmentYear");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Installments)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Installment_Syndic");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.Installments)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK_Installment_User");
        });

        modelBuilder.Entity<Judge>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Judge", "caseinfo");

            entity.HasIndex(e => e.CaseId, "IX_CaseId");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EGN");
            entity.Property(e => e.Family).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Rename).HasMaxLength(500);

            entity.HasOne(d => d.Case).WithMany(p => p.Judges)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Judge_Case");

            entity.HasOne(d => d.Session).WithMany(p => p.Judges)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Judge_Session");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Message", "communication");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Number).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.ReplyId).HasColumnName("ReplyID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Subject).HasMaxLength(400);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Messages)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Message_DocumentCollection");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Message_Message1");

            entity.HasOne(d => d.Reply).WithMany(p => p.InverseReply)
                .HasForeignKey(d => d.ReplyId)
                .HasConstraintName("FK_Message_Message");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Sender");
        });

        modelBuilder.Entity<MessageReceiver>(entity =>
        {
            entity.ToTable("MessageReceiver", "communication");

            entity.HasIndex(e => e.Id, "PK_ReceiverMessage").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MessageReceiver_Message");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MessageReceiver_Receiver");
        });

        modelBuilder.Entity<NomActAnnouncementCategory>(entity =>
        {
            entity.ToTable("nom_ActAnnouncementCategory", "registration");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<NomActAnnouncementStatus>(entity =>
        {
            entity.ToTable("nom_ActAnnouncementStatus", "registration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<NomActCategory>(entity =>
        {
            entity.ToTable("nom_ActCategory", "caseinfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomActContractor>(entity =>
        {
            entity.ToTable("nom_ActContractor", "caseinfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomActContractorDatum>(entity =>
        {
            entity.ToTable("nom_ActContractorData", "caseinfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomActKind>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nom_ActKind_1");

            entity.ToTable("nom_ActKind", "caseinfo");

            entity.HasIndex(e => e.Code, "IX_nom_ActKind").IsUnique();

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomActReason>(entity =>
        {
            entity.ToTable("nom_ActReason", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomAction>(entity =>
        {
            entity.ToTable("nom_Action", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<NomActivityKind>(entity =>
        {
            entity.ToTable("nom_ActivityKind", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomAnnouncementStatus>(entity =>
        {
            entity.ToTable("nom_AnnouncementStatus", "announcement");

            entity.HasIndex(e => e.Code, "UQ_Code").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomAppealKind>(entity =>
        {
            entity.ToTable("nom_AppealKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomAttachedDocumentKind>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nom_AttachedDocumentType");

            entity.ToTable("nom_AttachedDocumentKind", "document");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomCaseCode>(entity =>
        {
            entity.ToTable("nom_CaseCode", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomCaseKind>(entity =>
        {
            entity.ToTable("nom_CaseKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<NomCompanySize>(entity =>
        {
            entity.ToTable("nom_CompanySize", "caseinfo");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<NomCourseKind>(entity =>
        {
            entity.ToTable("nom_CourseKind", "training");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomCourt>(entity =>
        {
            entity.ToTable("nom_Court", "caseinfo");

            entity.HasIndex(e => e.Number, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FaxNumber).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomDebtorStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nom_DeptorStatus");

            entity.ToTable("nom_DebtorStatus", "caseinfo");

            entity.HasIndex(e => e.Name, "UX_Name").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<NomDirectiveTemplateKind>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nom_DirectiveTemplate");

            entity.ToTable("nom_DirectiveTemplateKind", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomDistrict>(entity =>
        {
            entity.ToTable("nom_Districts", "address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FullPath).HasMaxLength(500);
            entity.Property(e => e.FullPathName).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.NutsCode).HasMaxLength(200);
        });

        modelBuilder.Entity<NomIncomingDocumentKind>(entity =>
        {
            entity.ToTable("nom_IncomingDocumentKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomInspectionType>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("nom_InspectionType", "syndic");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomInstallmentKind>(entity =>
        {
            entity.ToTable("nom_InstallmentKind", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomInstallmentYear>(entity =>
        {
            entity.ToTable("nom_InstallmentYear", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<NomInvolvementKind>(entity =>
        {
            entity.ToTable("nom_InvolvementKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomLegalBasis>(entity =>
        {
            entity.ToTable("nom_LegalBasis", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomMunicipality>(entity =>
        {
            entity.ToTable("nom_Municipalities", "address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.FullPath).HasMaxLength(500);
            entity.Property(e => e.FullPathName).HasMaxLength(1000);
            entity.Property(e => e.LauCode).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.District).WithMany(p => p.NomMunicipalities)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_nom_Municipalities_nom_Districts");
        });

        modelBuilder.Entity<NomObjectKind>(entity =>
        {
            entity.ToTable("nom_ObjectKind", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomObjectType>(entity =>
        {
            entity.ToTable("nom_ObjectType", "announcement");

            entity.HasIndex(e => e.Code, "UQ_ObjectTypeCode").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomOfferingKind>(entity =>
        {
            entity.ToTable("nom_OfferingKind", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomOfferingLocationType>(entity =>
        {
            entity.ToTable("nom_OfferingLocationType", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomOfferingProcedure>(entity =>
        {
            entity.ToTable("nom_OfferingProcedure", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomOrderKind>(entity =>
        {
            entity.ToTable("nom_OrderKind", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomOrderPaymentKind>(entity =>
        {
            entity.ToTable("nom_OrderPaymentKind", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomPapersForSale>(entity =>
        {
            entity.ToTable("nom_PapersForSale", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomPropertyClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PropertyClass");

            entity.ToTable("nom_PropertyClass", "estate");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomRegisterField>(entity =>
        {
            entity.ToTable("nom_RegisterField", "registration");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<NomRegisterSyndicKind>(entity =>
        {
            entity.ToTable("nom_RegisterSyndicKind", "registration");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomRegistrationLegalBasis>(entity =>
        {
            entity.ToTable("nom_RegistrationLegalBasis", "registration");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Subject).HasMaxLength(200);
        });

        modelBuilder.Entity<NomReportSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_statreports.nom_ReportSource");

            entity.ToTable("nom_ReportSource", "statreports");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<NomReportType>(entity =>
        {
            entity.ToTable("nom_ReportType", "statreports");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<NomSalesProcedure>(entity =>
        {
            entity.ToTable("nom_SalesProcedure", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSampleKind>(entity =>
        {
            entity.ToTable("nom_SampleKind", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSellAnnouncementTemplate>(entity =>
        {
            entity.ToTable("nom_SellAnnouncementTemplate", "announcement");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NomSendToKind>(entity =>
        {
            entity.ToTable("nom_SendToKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSessionKind>(entity =>
        {
            entity.ToTable("nom_SessionKind", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<NomSessionResult>(entity =>
        {
            entity.ToTable("nom_SessionResult", "caseinfo");

            entity.HasIndex(e => e.Code, "UX_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSettlement>(entity =>
        {
            entity.ToTable("nom_Settlements", "address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.FullPath).HasMaxLength(500);
            entity.Property(e => e.FullPathName).HasMaxLength(1000);
            entity.Property(e => e.LauCode).HasMaxLength(10);
            entity.Property(e => e.MunicipalityId).HasColumnName("MunicipalityID");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Order).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Municipality).WithMany(p => p.NomSettlements)
                .HasForeignKey(d => d.MunicipalityId)
                .HasConstraintName("FK_nom_Settlements_nom_Municipalities");
        });

        modelBuilder.Entity<NomSignalDocumentKind>(entity =>
        {
            entity.ToTable("nom_SignalDocumentKind", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSignalSenderType>(entity =>
        {
            entity.ToTable("nom_SignalSenderType", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NomSpecialty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Specialty");

            entity.ToTable("nom_Specialty", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSyndicAction>(entity =>
        {
            entity.ToTable("nom_SyndicAction", "caseinfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSyndicCaseReport>(entity =>
        {
            entity.ToTable("nom_SyndicCaseReport", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSyndicKind>(entity =>
        {
            entity.ToTable("nom_SyndicKind", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<NomSyndicReportType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nom_ReportType");

            entity.ToTable("nom_SyndicReportType", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<NomSyndicStatus>(entity =>
        {
            entity.ToTable("nom_SyndicStatus", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(150);
        });

        modelBuilder.Entity<NomTemplateKind>(entity =>
        {
            entity.ToTable("nom_TemplateKind", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(300);
        });

        modelBuilder.Entity<NomTractType>(entity =>
        {
            entity.HasKey(e => e.ActType);

            entity.ToTable("nom_TRActType", "tr");

            entity.Property(e => e.ActType).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AnnouncementObject");

            entity.ToTable("Object", "announcement");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");

            entity.HasOne(d => d.Address).WithMany(p => p.Objects)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Object_Address");

            entity.HasOne(d => d.Announcement).WithMany(p => p.Objects)
                .HasForeignKey(d => d.AnnouncementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Object_Announcement");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Objects)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Object_DocumentCollection");

            entity.HasOne(d => d.ObjectKind).WithMany(p => p.Objects)
                .HasForeignKey(d => d.ObjectKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Object_nom_ObjectKind");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.Objects)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Object_nom_ObjectType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Order", "syndic");

            entity.HasIndex(e => e.DateCreated, "IX_created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.ExclusionGrounds).HasMaxLength(200);
            entity.Property(e => e.Number).HasMaxLength(100);
            entity.Property(e => e.OrderPaymentKindId).HasColumnName("OrderPaymentKindID");
            entity.Property(e => e.StateGazetteEdition).HasMaxLength(100);
            entity.Property(e => e.StateGazetteYear).HasMaxLength(100);

            entity.HasOne(d => d.OrderKind).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderKindId)
                .HasConstraintName("FK_Order_nom_OrderKind");

            entity.HasOne(d => d.OrderPaymentKind).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderPaymentKindId)
                .HasConstraintName("FK_Order_nom_OrderPaymentKind");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Syndic");
        });

        modelBuilder.Entity<OutgoingDocument>(entity =>
        {
            entity.ToTable("OutgoingDocument", "caseinfo");

            entity.HasIndex(e => e.AppealId, "IX_AppealId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.ReturnResult1).HasMaxLength(50);
            entity.Property(e => e.ReturnResult2).HasMaxLength(50);

            entity.HasOne(d => d.Action).WithMany(p => p.OutgoingDocuments)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK_OutgoingDocument_nom_Action");

            entity.HasOne(d => d.Appeal).WithMany(p => p.OutgoingDocuments)
                .HasForeignKey(d => d.AppealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingDocument_Appeal");

            entity.HasOne(d => d.Court).WithMany(p => p.OutgoingDocuments)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingDocument_nom_Court");

            entity.HasOne(d => d.SendKind).WithMany(p => p.OutgoingDocuments)
                .HasForeignKey(d => d.SendKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingDocument_nom_SendToKind");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Person", "caseinfo");

            entity.HasIndex(e => e.DateCreated, "IX_DateCreated").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EGN");
            entity.Property(e => e.FirstName).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(500);
            entity.Property(e => e.MiddleName).HasMaxLength(500);
        });

        modelBuilder.Entity<Plea>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Plea", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.OrderNumber).HasMaxLength(100);
            entity.Property(e => e.PleaNumber).HasMaxLength(100);
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Pleas)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Plea_DocumentCollection");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Pleas)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plea_Syndic");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Program", "training");

            entity.HasIndex(e => e.DateCreated, "IX_Created");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MoeorderDate).HasColumnName("MOEOrderDate");
            entity.Property(e => e.MoeorderNumber)
                .HasMaxLength(200)
                .HasColumnName("MOEOrderNumber");
            entity.Property(e => e.MojorderDate).HasColumnName("MOJOrderDate");
            entity.Property(e => e.MojorderNumber)
                .HasMaxLength(200)
                .HasColumnName("MOJOrderNumber");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("Property", "estate");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.EntityId).HasColumnName("EntityID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.PropertyClassId).HasColumnName("PropertyClassID");
            entity.Property(e => e.PropertyKindId).HasColumnName("PropertyKindID");
            entity.Property(e => e.PropertyTypeId).HasColumnName("PropertyTypeID");

            entity.HasOne(d => d.Address).WithMany(p => p.Properties)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Property_Address");

            entity.HasOne(d => d.Case).WithMany(p => p.Properties)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Property_Case");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Properties)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Property_DocumentCollection");

            entity.HasOne(d => d.Entity).WithMany(p => p.Properties)
                .HasForeignKey(d => d.EntityId)
                .HasConstraintName("FK_Property_Entity");

            entity.HasOne(d => d.Person).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Property_Person");

            entity.HasOne(d => d.PropertyClass).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyClassId)
                .HasConstraintName("FK_Property_nom_PropertyClass");

            entity.HasOne(d => d.PropertyKind).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyKindId)
                .HasConstraintName("FK_Property_nom_ObjectKind");

            entity.HasOne(d => d.PropertyType).WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyTypeId)
                .HasConstraintName("FK_Property_nom_ObjectType");
        });

        modelBuilder.Entity<Receiver>(entity =>
        {
            entity.ToTable("Receiver", "communication");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Receivers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Receiver_User");
        });

        modelBuilder.Entity<RegisterEntry>(entity =>
        {
            entity.ToTable("RegisterEntry", "registration");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActId).HasColumnName("ActID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AnnouncedByUserId).HasColumnName("AnnouncedByUserID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FieldId).HasColumnName("FieldID");
            entity.Property(e => e.LegalBasisId).HasColumnName("LegalBasisID");
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(100);
            entity.Property(e => e.PreviousEntryId).HasColumnName("PreviousEntryID");
            entity.Property(e => e.ProprietorId).HasColumnName("ProprietorID");
            entity.Property(e => e.RegisterSyndicKindId).HasColumnName("RegisterSyndicKindID");
            entity.Property(e => e.ReplacedByEntryId).HasColumnName("ReplacedByEntryID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Act).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.ActId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisterEntry_Act");

            entity.HasOne(d => d.Address).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_RegisterEntry_Address");

            entity.HasOne(d => d.AnnouncedByUser).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.AnnouncedByUserId)
                .HasConstraintName("FK_RegisterEntry_User");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.RegisterEntries)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_RegisterEntry_DocumentCollection");

            entity.HasOne(d => d.Field).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.FieldId)
                .HasConstraintName("FK_RegisterEntry_nom_RegisterField");

            entity.HasOne(d => d.LegalBasis).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.LegalBasisId)
                .HasConstraintName("FK_RegisterEntry_nom_RegistrationLegalBasis");

            entity.HasOne(d => d.PreviousEntry).WithMany(p => p.InversePreviousEntry)
                .HasForeignKey(d => d.PreviousEntryId)
                .HasConstraintName("FK_RegisterEntry_RegisterEntry1");

            entity.HasOne(d => d.Proprietor).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.ProprietorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisterEntry_Side");

            entity.HasOne(d => d.RegisterSyndicKind).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.RegisterSyndicKindId)
                .HasConstraintName("FK_RegisterEntry_nom_RegisterSyndicKind");

            entity.HasOne(d => d.ReplacedByEntry).WithMany(p => p.InverseReplacedByEntry)
                .HasForeignKey(d => d.ReplacedByEntryId)
                .HasConstraintName("FK_RegisterEntry_RegisterEntry");

            entity.HasOne(d => d.Syndic).WithMany(p => p.RegisterEntries)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_RegisterEntry_Syndic");
        });

        modelBuilder.Entity<RegixFailedLogin>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("RegixFailedLogins", "regix");

            entity.HasIndex(e => e.CreationDate, "IX_RegixFailedLogins").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(200)
                .HasColumnName("IPAddress");
            entity.Property(e => e.ProvidedPassword).HasMaxLength(200);
            entity.Property(e => e.ProvidedUserName).HasMaxLength(200);
        });

        modelBuilder.Entity<RegixSoaprequest>(entity =>
        {
            entity.ToTable("RegixSOAPRequests", "regix");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InvolvmentKind).HasMaxLength(100);
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.StateNumber).HasMaxLength(100);

            entity.HasOne(d => d.Session).WithMany(p => p.RegixSoaprequests)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_RegixSOAPRequests_RegixSOAPSessions");
        });

        modelBuilder.Entity<RegixSoapsession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RegixSOAPToken");

            entity.ToTable("RegixSOAPSessions", "regix");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RegixSoapsessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RegixSOAPSessions_RegixSOAPUsers");
        });

        modelBuilder.Entity<RegixSoapuser>(entity =>
        {
            entity.ToTable("RegixSOAPUsers", "regix");

            entity.HasIndex(e => e.Username, "IX_RegixSOAPUsers").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.HashedPassword).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Salt).HasMaxLength(200);
            entity.Property(e => e.Username).HasMaxLength(200);
        });

        modelBuilder.Entity<RegixUnrecognizedSession>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("RegixUnrecognizedSessions", "regix");

            entity.HasIndex(e => e.CreationDate, "IX_RegixUnrecognizedSessions").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(200)
                .HasColumnName("IPAddress");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("Report", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttachedDocumentCollectionId).HasColumnName("AttachedDocumentCollectionID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.ReportTypeId).HasColumnName("ReportTypeID");
            entity.Property(e => e.SampleId).HasColumnName("SampleID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.AttachedDocumentCollection).WithMany(p => p.ReportAttachedDocumentCollections)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.AttachedDocumentCollectionId)
                .HasConstraintName("FK_Report_AttachedDocumentCollection");

            entity.HasOne(d => d.Case).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Case");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.ReportDocumentCollections)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Report_DocumentCollection");

            entity.HasOne(d => d.ReportType).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_nom_SyndicCaseReport");

            entity.HasOne(d => d.Sample).WithMany(p => p.Reports)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK_Report_Sample");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Reports)
                .HasForeignKey(d => d.SyndicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Syndic");
        });

        modelBuilder.Entity<ReportTemplate>(entity =>
        {
            entity.ToTable("ReportTemplate", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.ReportTypeId).HasColumnName("ReportTypeID");
            entity.Property(e => e.TemplateName).HasMaxLength(200);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.ReportTemplates)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_ReportTemplate_DocumentCollection");

            entity.HasOne(d => d.ReportType).WithMany(p => p.ReportTemplates)
                .HasForeignKey(d => d.ReportTypeId)
                .HasConstraintName("FK_ReportTemplate_nom_SyndicCaseReport");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");

            entity.ToTable("Role", "account");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.ToTable("Sample", "syndic");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttachedDocumentCollectionId).HasColumnName("AttachedDocumentCollectionID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.GeneratedDocumentCollectionId).HasColumnName("GeneratedDocumentCollectionID");
            entity.Property(e => e.SampleKindId).HasColumnName("SampleKindID");

            entity.HasOne(d => d.AttachedDocumentCollection).WithMany(p => p.SampleAttachedDocumentCollections)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.AttachedDocumentCollectionId)
                .HasConstraintName("FK_Sample_DocumentCollection1");

            entity.HasOne(d => d.GeneratedDocumentCollection).WithMany(p => p.SampleGeneratedDocumentCollections)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.GeneratedDocumentCollectionId)
                .HasConstraintName("FK_Sample_DocumentCollection");

            entity.HasOne(d => d.SampleKind).WithMany(p => p.Samples)
                .HasForeignKey(d => d.SampleKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sample_nom_SampleKind");
        });

        modelBuilder.Entity<Sender>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Sender", "communication");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Senders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Sender_User");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Session", "caseinfo");

            entity.HasIndex(e => e.CaseId, "IX_CaseId");

            entity.HasIndex(e => e.DateCreated, "IX_DateCreated").IsClustered();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SyndicActionId).HasColumnName("SyndicActionID");

            entity.HasOne(d => d.Action).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK_Session_nom_Action");

            entity.HasOne(d => d.Case).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Session_Case");

            entity.HasOne(d => d.Result).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK_Session_nom_SessionResult");

            entity.HasOne(d => d.SessionKind).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SessionKindId)
                .HasConstraintName("FK_Session_nom_SessionKind");

            entity.HasOne(d => d.SyndicAction).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SyndicActionId)
                .HasConstraintName("FK_Session_nom_SyndicAction");
        });

        modelBuilder.Entity<Session1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("session1");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.ToTable("Settings", "admin");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.BankName).HasMaxLength(400);
            entity.Property(e => e.Bic)
                .HasMaxLength(100)
                .HasColumnName("BIC");
            entity.Property(e => e.ChiefInspector).HasMaxLength(300);
            entity.Property(e => e.ChiefInspectorDescription).HasMaxLength(400);
            entity.Property(e => e.Iban)
                .HasMaxLength(50)
                .HasColumnName("IBAN");
            entity.Property(e => e.MinisterName).HasMaxLength(300);
        });

        modelBuilder.Entity<Side>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Side", "caseinfo");

            entity.HasIndex(e => e.AppealId, "IX_AppealId");

            entity.HasIndex(e => e.CaseId, "IX_CaseId");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.HasIndex(e => e.SurroundDocumentId, "IX_SurroundDocumentId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DebtorStatusId).HasColumnName("DebtorStatusID");

            entity.HasOne(d => d.Appeal).WithMany(p => p.Sides)
                .HasForeignKey(d => d.AppealId)
                .HasConstraintName("FK_Side_Appeal");

            entity.HasOne(d => d.Case).WithMany(p => p.Sides)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Side_Case");

            entity.HasOne(d => d.DebtorStatus).WithMany(p => p.Sides)
                .HasForeignKey(d => d.DebtorStatusId)
                .HasConstraintName("FK_Side_nom_DebtorStatus");

            entity.HasOne(d => d.Entity).WithMany(p => p.Sides)
                .HasForeignKey(d => d.EntityId)
                .HasConstraintName("FK_Side_Entity");

            entity.HasOne(d => d.InvolvementKind).WithMany(p => p.Sides)
                .HasForeignKey(d => d.InvolvementKindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Side_nom_InvolvementKind");

            entity.HasOne(d => d.Person).WithMany(p => p.Sides)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Side_Person");

            entity.HasOne(d => d.SurroundDocument).WithMany(p => p.Sides)
                .HasForeignKey(d => d.SurroundDocumentId)
                .HasConstraintName("FK_Side_SurroundDocument");
        });

        modelBuilder.Entity<Signal>(entity =>
        {
            entity.ToTable("Signal", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.Number)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Case).WithMany(p => p.Signals)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Signal_Case");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Signals)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Signal_DocumentCollection");

            entity.HasOne(d => d.Sender).WithMany(p => p.Signals)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK_Signal_SignalSender");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Signals)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Signal_Syndic");
        });

        modelBuilder.Entity<SignalSender>(entity =>
        {
            entity.ToTable("SignalSender", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.CitizenshipNumber).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.SignalSenderTypeId).HasColumnName("SignalSenderTypeID");

            entity.HasOne(d => d.Address).WithMany(p => p.SignalSenders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_SignalSender_Address");

            entity.HasOne(d => d.SignalSenderType).WithMany(p => p.SignalSenders)
                .HasForeignKey(d => d.SignalSenderTypeId)
                .HasConstraintName("FK_SignalSender_nom_SignalSenderType");
        });

        modelBuilder.Entity<StatTemplate>(entity =>
        {
            entity.ToTable("StatTemplate", "statreports");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.StatTemplates)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_StatTemplate_DocumentCollection");
        });

        modelBuilder.Entity<StatisticalReport>(entity =>
        {
            entity.ToTable("StatisticalReport", "statreports");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.IdentificationNumbr).HasMaxLength(200);
            entity.Property(e => e.ReportPreparator).HasMaxLength(200);
            entity.Property(e => e.ReportSourceId).HasColumnName("ReportSourceID");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.StatisticalReports)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_StatisticalReport_StatisticalReport1");

            entity.HasOne(d => d.ReportSource).WithMany(p => p.StatisticalReports)
                .HasForeignKey(d => d.ReportSourceId)
                .HasConstraintName("FK_StatisticalReport_nom_ReportSource");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.StatisticalReports)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_StatisticalReport_StatisticalReport");
        });

        modelBuilder.Entity<SurroundDocument>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("SurroundDocument", "caseinfo");

            entity.HasIndex(e => e.CaseId, "IX_CaseId");

            entity.HasIndex(e => e.DateCreated, "IX_DateCreated").IsClustered();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Action).WithMany(p => p.SurroundDocuments)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK_SurroundDocument_nom_Action");

            entity.HasOne(d => d.Case).WithMany(p => p.SurroundDocuments)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_SurroundDocument_Case");
        });

        modelBuilder.Entity<Syndic>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Syndic", "syndic");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.HasIndex(e => e.HashValue, "IX_SyndicUnique").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EGN");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.HashValue)
                .HasMaxLength(64)
                .HasComputedColumnSql("(CONVERT([varbinary](64),hashbytes('SHA2_512',concat([FirstName],[SecondName],[LastName],[EGN]))))", false);
            entity.Property(e => e.LastName).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Number).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.SecondLastName).HasMaxLength(200);
            entity.Property(e => e.SecondName).HasMaxLength(200);

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Syndics)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Syndic_DocumentCollection");

            entity.HasOne(d => d.SpecialtyNavigation).WithMany(p => p.Syndics)
                .HasForeignKey(d => d.Specialty)
                .HasConstraintName("FK_Syndic_nom_Specialty");

            entity.HasOne(d => d.SyndicKind).WithMany(p => p.Syndics)
                .HasForeignKey(d => d.SyndicKindId)
                .HasConstraintName("FK_Syndic_nom_SyndicKind");

            entity.HasOne(d => d.SyndicStatus).WithMany(p => p.Syndics)
                .HasForeignKey(d => d.SyndicStatusId)
                .HasConstraintName("FK_Syndic_nom_SyndicStatus");

            entity.HasOne(d => d.User).WithMany(p => p.Syndics)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Syndic_User");
        });

        modelBuilder.Entity<Syndic2Address>(entity =>
        {
            entity.ToTable("Syndic2Address", "address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Address).WithMany(p => p.Syndic2Addresses)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Syndic2Address_Address");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Syndic2Addresses)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Syndic2Address_Syndic");
        });

        modelBuilder.Entity<TempSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_sessions");

            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Caseid)
                .HasMaxLength(36)
                .HasColumnName("caseid");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Sessionresult).HasColumnName("sessionresult");
            entity.Property(e => e.Text).HasMaxLength(500);
            entity.Property(e => e.Time).HasColumnType("datetime");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Template", "syndic");

            entity.HasIndex(e => e.DateCreated, "IX_Created").IsClustered();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.Templates)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_Template_DocumentCollection");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Templates)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Template_Syndic");

            entity.HasOne(d => d.TemplateKind).WithMany(p => p.Templates)
                .HasForeignKey(d => d.TemplateKindId)
                .HasConstraintName("FK_Template_nom_TemplateKind");
        });

        modelBuilder.Entity<TemplateArticle28>(entity =>
        {
            entity.ToTable("TemplateArticle28", "syndic");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.DirectiveTemplateKindId).HasColumnName("DirectiveTemplateKindID");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.TemplateName).HasMaxLength(200);

            entity.HasOne(d => d.DirectiveTemplateKind).WithMany(p => p.TemplateArticle28s)
                .HasForeignKey(d => d.DirectiveTemplateKindId)
                .HasConstraintName("FK_TemplateArticle28_nom_DirectiveTemplateKind");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.TemplateArticle28s)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_TemplateArticle28_DocumentCollection");
        });

        modelBuilder.Entity<TemplateDocument>(entity =>
        {
            entity.ToTable("TemplateDocument", "document");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.BlobId).HasColumnName("BlobID");
            entity.Property(e => e.ContentMimeType)
                .HasMaxLength(200)
                .HasComment("Тип на файл");
            entity.Property(e => e.Description).HasComment("Описание");
            entity.Property(e => e.DocumentCollectionId).HasColumnName("DocumentCollectionID");
            entity.Property(e => e.DocumentDate).HasComment("Дата на документ");
            entity.Property(e => e.FileName)
                .HasMaxLength(250)
                .HasComment("Име на файл");
            entity.Property(e => e.FileSize).HasComment("Размер на файл");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Blob).WithMany(p => p.TemplateDocuments)
                .HasForeignKey(d => d.BlobId)
                .HasConstraintName("FK_TemplateDocument_Blob");

            entity.HasOne(d => d.DocumentCollection).WithMany(p => p.TemplateDocuments)
                .HasPrincipalKey(p => p.DocumentCollectionId)
                .HasForeignKey(d => d.DocumentCollectionId)
                .HasConstraintName("FK_TemplateDocument_DocumentCollection");
        });

        modelBuilder.Entity<Trustee>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Trustee", "tr");

            entity.HasIndex(e => e.ImportDate, "IX_Trustee_Date").IsClustered();

            entity.HasIndex(e => e.DeedId, "IX_Trustee_Deed");

            entity.HasIndex(e => e.SyndicId, "IX_Trustee_SyndicID");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CountryName).HasMaxLength(200);
            entity.Property(e => e.DeedId).HasColumnName("DeedID");
            entity.Property(e => e.ImportDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Indent).HasMaxLength(50);
            entity.Property(e => e.IndentType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.RecordIncomingNumber).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(200);
            entity.Property(e => e.SyndicId).HasColumnName("SyndicID");

            entity.HasOne(d => d.Deed).WithMany(p => p.Trustees)
                .HasForeignKey(d => d.DeedId)
                .HasConstraintName("FK_Trustee_ImportedDeed");

            entity.HasOne(d => d.Syndic).WithMany(p => p.Trustees)
                .HasForeignKey(d => d.SyndicId)
                .HasConstraintName("FK_Trustee_Syndic");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

            entity.ToTable("User", "account");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Egn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EGN");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.SecondName).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRole_Role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRole_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                        j.ToTable("UserRole", "account");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("RoleID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
