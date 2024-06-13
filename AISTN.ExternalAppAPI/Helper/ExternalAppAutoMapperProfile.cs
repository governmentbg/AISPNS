using AISTN.Common.Models;
using AISTN.Common.Models.Save;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models;
using AISTN.ExternalAppAPI.Models.Details;
using AISTN.ExternalAppAPI.Models.Export;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Models;
using AISTN.Repository;
using AutoMapper;
using Object = AISTN.Data.DataModel.Object;

namespace AISTN.ExternalAppAPI.Helper
{
    public class ExternalAppAutoMapperProfile : Profile
    {
        public ExternalAppAutoMapperProfile()
        {
            #region Nomenclatures

            CreateMap<NomAction, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActReason, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCaseKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomIncomingDocumentKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInvolvementKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSendToKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSessionKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSessionResult, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomMunicipality, NomMunicipalityDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomDistrict, NomDistrictDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSettlement, NomSettlementDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActivityKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSampleKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicReportType, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInstallmentKind, NomInstallmentKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInstallmentYear, NomInstallmentYearDTO>().ReverseMap().PreserveReferences();
            //CreateMap<NomSyndicAction, NomSyndicActionDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCompanySize, NomCompanySizeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCompanySize, NomenclatureDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region CustomNomenclatures

            CreateMap<NomCourt, NomCourtDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomTemplateKind, NomTemplateKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportType, NomReportTypeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportSource, NomReportSourceDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOrderKind, NomOrderKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSpecialty, NomSpecialityDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomDebtorStatus, NomDebtorStatusDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCaseCode, NomCaseCodeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomPropertyClass, NomPropertyClassDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicCaseReport, NomSyndicCaseReportDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCourseKind, NomCourseKindDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region AnnouncementNomenclatures

            CreateMap<NomAnnouncementStatus, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomLegalBasis, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingKind, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingLocationType, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingProcedure, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectKind, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectType, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomPapersForSale, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSalesProcedure, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Announcement

            CreateMap<Announcement, AnnouncementIndexDTO>().ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                           .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                                           .ForMember(x => x.Syndic, opt => opt.MapFrom(x => x.Syndic))
                                                           .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType))
                                                           .ForMember(x => x.Status, opt => opt.MapFrom(x => x.StatusCodeNavigation.Description))
                                                           .ReverseMap().PreserveReferences();
            CreateMap<Announcement, OpenDataRawAnnouncementDTO>().ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                           .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                                           .ForMember(x => x.Syndic, opt => opt.MapFrom(x => x.Syndic))
                                                           .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType))
                                                           .ForMember(x => x.OfferingKindName, opt => opt.MapFrom(x => x.OfferingKind.Description))
                                                           .ForMember(x => x.Status, opt => opt.MapFrom(x => x.StatusCodeNavigation.Description))
                                                           .ReverseMap().PreserveReferences();

            CreateMap<Announcement, SaveAnnouncementDTO>().ReverseMap().PreserveReferences();

            CreateMap<Object, ObjectIndexDTO>().ForMember(x => x.ObjectType, opt => opt.MapFrom(x => x.ObjectType.Description))
                                                           .ForMember(x => x.ObjectKind, opt => opt.MapFrom(x => x.ObjectKind.Description))
                                                           .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                           .ReverseMap().PreserveReferences();

            CreateMap<Object, SaveObjectDTO>().ReverseMap().PreserveReferences();


            CreateMap<Announcement, CertificateIndexDTO>().ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                                                           .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                                                           .ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate))
                                                           .ForMember(x => x.AnnouncementId, opt => opt.MapFrom(x => x.Id))
                                                           .ForMember(x => x.AnnouncementNumber, opt => opt.MapFrom(x => x.DateCreated.ToString("yyyyMMddHHmmss")))
                                                           .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                                                           .ForMember(x => x.CaseYear, opt => opt.MapFrom(x => x.Case.Year))
                                                           .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.SecondName, x.Syndic.LastName)))
                                                           .ForMember(x => x.DebtorName, opt => opt.MapFrom(x => x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Entity.Name))
                                                           .ReverseMap().PreserveReferences();

            CreateMap<Announcement, DetailsCertificateDTO>().ForMember(x => x.DateCreated, opt => opt.MapFrom(x => x.PublishDate.HasValue ? x.PublishDate.Value.ToString("dd-MM-YYYY") : DateTime.Today.ToString("dd-MM-yyyy")))
                                                            .ForMember(x => x.TimeCreated, opt => opt.MapFrom(x => x.PublishDate.HasValue ? x.PublishDate.Value.ToString("HH:mm") : DateTime.Today.ToString("HH:mm")))
                                                            .ForMember(x => x.AnnouncementNumber, opt => opt.MapFrom(x => x.DateCreated.ToString("yyyyMMddHHmmss")))
                                                            .ForMember(x => x.OfferingDate, opt => opt.MapFrom(x => x.OfferingDate.ToString("dd-MM-yyyy HH:mm")))
                                                            .ForMember(x => x.Address, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                            .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.SecondName, x.Syndic.LastName)))
                                                            .ForMember(x => x.DebtorName, opt => opt.MapFrom(x => String.Join(" ", x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Entity.Name)))
                                                            .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                                                            .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Year))
                                                            .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                                                            .ForMember(x => x.SalesProcedure, opt => opt.MapFrom(x => x.SalesProcedure.Description))
                                                            .ReverseMap().PreserveReferences();

            #endregion

            #region Side

            CreateMap<Side, SideIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Entity, EntityIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Accounts and Users

            CreateMap<User, AccountUserIndexDTO>().ForMember(x => x.RoleNames, opt => opt.MapFrom(x => x.Roles.Select(r => r.Name))).ReverseMap().PreserveReferences();
            CreateMap<User, SaveAccountUserDTO>().ForMember(x => x.RoleIds, opt => opt.MapFrom(x => x.Roles.Select(r => r.Id))).ReverseMap().PreserveReferences();
            CreateMap<User, UserShortInfoDTO>().ForMember(x => x.RoleNames, opt => opt.MapFrom(x => x.Roles.Select(r => r.Name)))
                                               .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedUserName(x))).ReverseMap().PreserveReferences();
            CreateMap<User, CurrentUser>().ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles.Select(x => x.Name)))
                                          .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedUserName(x)))
                                          .ForMember(x => x.IsAuthenticated, opt => opt.MapFrom(x => true))
                                          .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                                          .ReverseMap().PreserveReferences();
            #endregion

            #region Case

            CreateMap<Case, CaseIndexDTO>().ForMember(x => x.CaseKindDescription, opt => opt.MapFrom(x => x.CaseKind.Description))
                                           .ForMember(x => x.Side, opt => opt.MapFrom(x => x.Sides.FirstOrDefault(x => x.IsDebtor))).ReverseMap().PreserveReferences();

            CreateMap<Case, AnnouncementCaseIndexDTO>().ForMember(x => x.Side, opt => opt.MapFrom(x => x.Sides.FirstOrDefault(x => x.IsDebtor)))
                                                       .ReverseMap().PreserveReferences();

            CreateMap<Session, CaseBookIndexDTO>().ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date ?? x.Date.Value))
                                                  .ForMember(x => x.Actvity, opt => opt.MapFrom(x => x.SessionKind.Description ?? x.SessionKind.Description))
                                                  .ForMember(x => x.SessionResult, opt => opt.MapFrom(x => x.Acts.FirstOrDefault().Text))
                                                  .ReverseMap().PreserveReferences();

            CreateMap<SurroundDocument, CaseBookIndexDTO>().ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date))
                                                           .ForMember(x => x.Actvity, opt => opt.MapFrom(x => x.Text))
                                                           .ForMember(x => x.ParticipantName, opt => opt.MapFrom(x => x.Sides.FirstOrDefault().Entity != null ?
                                                                                                                      x.Sides.FirstOrDefault().Entity.Name :
                                                                                                                      String.Join(" ", x.Sides.FirstOrDefault().Person.FirstName,
                                                                                                                                       x.Sides.FirstOrDefault().Person.MiddleName,
                                                                                                                                       x.Sides.FirstOrDefault().Person.LastName))
                                                           )
                                                           .ReverseMap().PreserveReferences();

            CreateMap<EntityStatisticalDatum, SaveEntityStatisticalDataDTO>().ReverseMap().PreserveReferences();


            #endregion

            #region Syndic

            CreateMap<Syndic, SaveSyndicDTO>().ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address)).ReverseMap().PreserveReferences();
            CreateMap<Syndic, SyndicIndexDTO>().ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address)).ReverseMap().PreserveReferences();

            CreateMap<Syndic, SyndicDTO>()
                    .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                    .ForMember(x => x.SyndicStatus, opt => opt.MapFrom(x => x.SyndicStatus.Description))
                    .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Orders.OrderByDescending(x => x.Date).FirstOrDefault()))
                    .ForMember(x => x.InstallmentForYears,
                                opt => opt.MapFrom(x => x.Installments.Any() ? String.Join(",", x.Installments.Select(x => x.PaymentDate.Value.Year).Where(x => x != null)) : null))
                    .ForMember(x => x.LastInstallmentForYear, opt => opt.MapFrom(x => x.Installments.Any() ? x.Installments.Max(x => x.PaymentDate.Value.Year) : default))
                    .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address))
                    .ReverseMap().PreserveReferences();

            CreateMap<Syndic, SyndicShortInfoDTO>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x)))
                .ReverseMap().PreserveReferences();

            CreateMap<CourseParticipation, CourseParticipationDTO>()
                                                .ForMember(x => x.CourseTopic, opt => opt.MapFrom(x => x.CourseApplication.Course.Topic))
                                                .ForMember(x => x.CourseKindName, opt => opt.MapFrom(x => x.CourseApplication.Course.CourseKind.Name))
                                                .ForMember(x => x.LecturerDate1, opt => opt.MapFrom(x => x.CourseApplication.LecturerDate1))
                                                .ForMember(x => x.LecturerDate2, opt => opt.MapFrom(x => x.CourseApplication.LecturerDate2))
                                                .ReverseMap().PreserveReferences();
            #endregion

            #region Order

            CreateMap<Order, OrderIndexDTO>().ForMember(x => x.OrderKindName, opt => opt.MapFrom(x => x.OrderKind.Description))
                                             .ReverseMap().PreserveReferences();

            #endregion

            #region ContactForm

            CreateMap<ContactForm, ContactFormDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Address

            CreateMap<Address, AddressIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Document

            CreateMap<DocumentContent, DocumentContentDTO>().ForMember(x => x.BlobDateCreated, opt => opt.MapFrom(x => x.Blob.DateCreated)).ReverseMap().PreserveReferences();
            CreateMap<DocumentCollection, DocumentCollectionDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomAttachedDocumentKind, NomAttachedDocumentKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSignalDocumentKind, NomSignalDocumentKindDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Message

            CreateMap<MessageReceiver, MessageIndexDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Message.Id))
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.Message.Sender.User)))
                .ForMember(x => x.ReceiverName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.Receiver.User)))
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.Message.DateCreated))
                .ForMember(x => x.ReceivedDate, opt => opt.MapFrom(x => x.Receiver.DateCreated))
                .ForMember(x => x.Subject, opt => opt.MapFrom(x => x.Message.Subject))
                .ForMember(x => x.ReplyId, opt => opt.MapFrom(x => x.Message.ReplyId))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.Message.ParentId))
                .ReverseMap().PreserveReferences();
            CreateMap<Message, SaveMessageDTO>()
                .ForMember(x => x.MessageReceiverIDs, opt => opt.MapFrom(x => x.MessageReceivers.Select(x => x.Receiver.UserId)))
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number.ToString().PadLeft(7, '0')))
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.DateCreated))
                .ForMember(x => x.SeenDate, opt => opt.MapFrom(x => x.MessageReceivers.FirstOrDefault(y => y.MessageId == x.Id).SeenDate))
                .ReverseMap().PreserveReferences();

            CreateMap<MessageReceiver, ReceiverIndexDTO>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Receiver.User.Roles.Select(x => x.Name)))
                .ReverseMap().PreserveReferences();
            CreateMap<Sender, SaveSenderDTO>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.User)))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.User.Roles.Select(x => x.Name)))
                .ReverseMap().PreserveReferences();

            CreateMap<Message, DetailsMessageDTO>()
                .ForMember(x => x.MessageReceiverNames, opt => opt.MapFrom(x => x.MessageReceivers.Select(x => GetConcatenatedUserName(x.Receiver.User))))
                .ForMember(x => x.MessageReceiverIDs, opt => opt.MapFrom(x => x.MessageReceivers.Select(x => x.Receiver.UserId)))
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number.ToString().PadLeft(7, '0')))
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.DateCreated))
                .ForMember(x => x.ReplyId, opt => opt.MapFrom(x => x.ReplyId))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.ParentId))
                .ForMember(x => x.SeenDate, opt => opt.MapFrom(x => x.MessageReceivers.FirstOrDefault(y => y.MessageId == x.Id).SeenDate))
                .ReverseMap().PreserveReferences();

            #endregion

            #region Installment

            CreateMap<Installment, InstallmentIndexDTO>()
                .ForMember(x => x.VerifiedByName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.VerifiedByNavigation)))
                .ForMember(x => x.InstallmentKindName, opt => opt.MapFrom(x => x.InstallmentKind.Description))
                .ForMember(x => x.InstallmentYear, opt => opt.MapFrom(x => x.InstallmentYear.Year))
                .ForMember(x => x.InstallmentYearAmount, opt => opt.MapFrom(x => x.InstallmentYear.Amount))
                .ReverseMap().PreserveReferences();
            CreateMap<Installment, SaveInstallmentDTO>().ForMember(x => x.VerifiedByName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.VerifiedByNavigation)))
                                             .ReverseMap().PreserveReferences();
            #endregion

            #region Template

            CreateMap<Template, TemplateIndexDTO>().ForMember(x => x.TemplateKindName, opt => opt.MapFrom(x => x.TemplateKind.Name));
            CreateMap<SaveTemplateDTO, Template>().ReverseMap().PreserveReferences();

            CreateMap<AdminTemplate, AdminTemplateIndexDTO>().ForMember(x => x.TemplateKind, opt => opt.MapFrom(x => x.TemplateKind!.Name))
                                                             .ForMember(x => x.DocumentContentID, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents
                                                                                                                                        .FirstOrDefault()!.Id));

            CreateMap<Syndic, DetailsSyndicAdminTemplateDTO>().ForMember(x => x.SyndicFullName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x)))
                                                              .ForMember(x => x.SyndicAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Syndic2Addresses.FirstOrDefault().Address)))
                                                              .ForMember(x => x.SyndicIdentifier, opt => opt.MapFrom(x => x.Egn))
                                                              .ForMember(x => x.SyndicPhone, opt => opt.MapFrom(x => x.Phone))
                                                              .ForMember(x => x.SyndicEmail, opt => opt.MapFrom(x => x.Email));

            #endregion

            #region Report
            CreateMap<Report, ReportIndexDTO>().ForMember(x => x.ReportTypeName, opt => opt.MapFrom(x => x.ReportType.Name))
                                                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                                                .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                                                .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                                                .ForMember(x => x.CaseYear, opt => opt.MapFrom(x => x.Case.Year))
                                                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.SecondName, x.Syndic.LastName)))
                                                .ForMember(x => x.DebtorName, opt => opt.MapFrom(x => x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Entity.Name));
            CreateMap<SaveReportDTO, Report>().ReverseMap().PreserveReferences();
            CreateMap<Report, SaveReportDTO>().ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.SecondName, x.Syndic.LastName))).ReverseMap().PreserveReferences();
            #endregion

            #region Programs and Courses

            CreateMap<Data.DataModel.Program, ProgramIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<CourseApplication, CourseApplicationIndexDTO>()
                .ForMember(x => x.CourseName, opt => opt.MapFrom(x => x.Course.Topic))
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ForMember(x => x.MainCourseName, opt => opt.MapFrom(x => x.Course.CourseKind.Name))
                //.ForMember(x => x.AlternateCourseName, opt => opt.MapFrom(x => x.AlternateCourse.CourseKind.Name))
                .ForMember(x => x.ProgramDescription, opt => opt.MapFrom(x => x.Course.Program.Description))
                .ForMember(x => x.ProgramId, opt => opt.MapFrom(x => x.Course.ProgramId))
                .ReverseMap().PreserveReferences();
            CreateMap<Course, CourseIndexDTO>().ForMember(x => x.CourseKindName, opt => opt.MapFrom(x => x.CourseKind.Name))
                .ForMember(x => x.ProgramDescription, opt => opt.MapFrom(x => x.Program.Description)).ReverseMap().PreserveReferences();
            CreateMap<CourseResultIndexDTO, CourseResult>()
                .ForMember(x => x.Course, opt => opt.MapFrom(x => x.Course)).ReverseMap().PreserveReferences();
            CreateMap<SaveCourseApplicationDTO, CourseApplication>().ReverseMap().PreserveReferences();
            CreateMap<CourseParticipation, CourseParticipationIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Address, SaveAddressDTO>().ReverseMap().PreserveReferences();
            CreateMap<Course, SaveCourseDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Property

            CreateMap<Property, SavePropertyDTO>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address))
                .ReverseMap().PreserveReferences();
            CreateMap<Property, PropertyIndexDTO>()
                .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                .ForMember(x => x.EntityName, opt => opt.MapFrom(x => x.Entity.Name))
                .ForMember(x => x.PersonName, opt => opt.MapFrom(x => GetConcatenatedPersonName(x.Person)))
                .ForMember(x => x.PropertyKindName, opt => opt.MapFrom(x => x.PropertyKind.Description))
                .ForMember(x => x.PropertyTypeName, opt => opt.MapFrom(x => x.PropertyType.Description))
                .ReverseMap().PreserveReferences();


            #endregion

            #region Activity

            CreateMap<Activity, ActivityIndexDTO>()
                .ForMember(x => x.ActivityKindName, opt => opt.MapFrom(x => x.ActivityKind.Description))
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ReverseMap().PreserveReferences();
            CreateMap<Activity, SaveActivityDTO>()
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ReverseMap().PreserveReferences();

            #endregion


            #region Entity

            CreateProjection<Entity, EntityDTO>();
            CreateMap<Entity, SaveEntityDTO>().ReverseMap().PreserveReferences();
            CreateProjection<Person, PersonDTO>().ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedPersonName(x)));
            CreateMap<Person, SavePersonDTO>().ReverseMap().PreserveReferences();

            #endregion
        }

        private static string GetConcatenatedAddress(Address source)
        {
            if (source == null) return null;

            List<string> addressParts = new List<string>();
            if (source.Region != null && !string.IsNullOrWhiteSpace(source.Region.Name))
                addressParts.Add(source.Region.Name);
            if (source.Settlement != null && !string.IsNullOrWhiteSpace(source.Settlement.Name))
                addressParts.Add(source.Settlement.Name);
            if (source.Municipality != null && !string.IsNullOrWhiteSpace(source.Municipality.Name))
                addressParts.Add(source.Municipality.Name);
            if (!string.IsNullOrWhiteSpace(source.PostCode))
                addressParts.Add(source.PostCode);
            if (!string.IsNullOrWhiteSpace(source.StreetName))
                addressParts.Add(source.StreetName);
            if (!string.IsNullOrWhiteSpace(source.StreetNumber))
                addressParts.Add(source.StreetNumber);
            if (!string.IsNullOrWhiteSpace(source.BuildingNumber))
                addressParts.Add(source.BuildingNumber);
            if (!string.IsNullOrWhiteSpace(source.EntranceNumber))
                addressParts.Add(source.EntranceNumber);
            if (!string.IsNullOrWhiteSpace(source.FloorNumber))
                addressParts.Add(source.FloorNumber);
            if (!string.IsNullOrWhiteSpace(source.ApartmentNumber))
                addressParts.Add(source.ApartmentNumber);


            return string.Join(", ", addressParts);
        }
        private static string GetConcatenatedUserName(User source)
        {
            if (source == null) return null;
            List<string> userNames = new List<string>();

            if (!String.IsNullOrEmpty(source.FirstName))
            {
                userNames.Add(source.FirstName);
            }
            if (!String.IsNullOrEmpty(source.SecondName))
            {
                userNames.Add(source.SecondName);
            }
            if (!String.IsNullOrEmpty(source.LastName))
            {
                userNames.Add(source.LastName);
            }

            return string.Join(" ", userNames);
        }

        private static string GetConcatenatedPersonName(Person source)
        {
            if (source == null) return null;
            List<string> userNames = new List<string>();

            if (!String.IsNullOrEmpty(source.FirstName))
            {
                userNames.Add(source.FirstName);
            }
            if (!String.IsNullOrEmpty(source.MiddleName))
            {
                userNames.Add(source.MiddleName);
            }
            if (!String.IsNullOrEmpty(source.LastName))
            {
                userNames.Add(source.LastName);
            }

            return string.Join(" ", userNames);
        }

        private static string GetConcatenatedSyndicName(Syndic source)
        {
            if (source == null) return null;
            List<string> syndicNames = new List<string>();

            if (!String.IsNullOrEmpty(source.FirstName))
            {
                syndicNames.Add(source.FirstName);
            }
            if (!String.IsNullOrEmpty(source.SecondName))
            {
                syndicNames.Add(source.SecondName);
            }
            if (!String.IsNullOrEmpty(source.LastName))
            {
                syndicNames.Add(source.LastName);
            }
            if (!String.IsNullOrEmpty(source.SecondLastName))
            {
                syndicNames.Add(source.SecondLastName);
            }


            return string.Join(" ", syndicNames);
        }

    }
}
