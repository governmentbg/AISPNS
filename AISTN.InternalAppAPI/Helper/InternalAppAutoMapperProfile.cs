using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.InternalAppAPI.Models.Export;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AutoMapper;
using CourseIndexDTO = AISTN.InternalAppAPI.Models.Index.CourseIndexDTO;
using Object = AISTN.Data.DataModel.Object;
using ProgramIndexDTO = AISTN.InternalAppAPI.Models.Index.ProgramIndexDTO;

namespace AISTN.InternalAppAPI.Helper
{
    public class InternalAppAutoMapperProfile : Profile
    {
        public InternalAppAutoMapperProfile()
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
            CreateMap<NomDirectiveTemplateKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomAppealKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectKind, NomObjectKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectType, NomObjectTypeDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region CustomNomenclatures

            CreateMap<NomCourt, NomCourtDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomTemplateKind, NomTemplateKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportType, NomReportTypeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportSource, NomReportSourceDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOrderKind, NomOrderKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomDebtorStatus, NomDebtorStatusDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCaseCode, NomCaseCodeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInstallmentKind, NomInstallmentKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicStatus, NomSyndicStatusDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSellAnnouncementTemplate, SellAnnouncementTemplateDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSellAnnouncementTemplate, SaveSellAnnouncementTemplateDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInstallmentYear, NomInstallmentYearDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSignalSenderType, NomSignalSenderTypeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSpecialty, NomSpecialityDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInspectionType, NomInspectionTypeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicCaseReport, NomSyndicCaseReportDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCourseKind, NomCourseKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOrderPaymentKind, NomOrderPaymentKindDTO>().ReverseMap().PreserveReferences();
            //CreateMap<NomSyndicAction, NomSyndicActionDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActCategory, NomActCategoryDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActContractor, NomActContractorDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomLegalBasis, NomLegalBasisDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActivityKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSampleKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicReportType, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCompanySize, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCompanySize, NomCompanySizeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActAnnouncementCategory, NomActAnnouncementCategoryDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActAnnouncementStatus, NomActAnnouncementStatusDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomRegisterField, NomRegisterFieldDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomRegistrationLegalBasis, NomRegistrationLegalBasisDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActAnnouncementCategory, NomActAnnouncementCategoryDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicKind, NomSyndicKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomRegisterField, NomRegisterFieldDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomRegisterSyndicKind, NomRegisterSyndicKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActKind, NomActKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActReason, NomActReasonDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region DB to DB models

            CreateMap<Syndic, User>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(x => x.SecondName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Egn))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => x.Active))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap().PreserveReferences();

            #endregion

            #region Accounts and Users

            CreateMap<User, AccountUserIndexDTO>().ForMember(x => x.RoleNames, opt => opt.MapFrom(x => x.Roles.Select(r => r.Name))).ReverseMap().PreserveReferences();
            CreateMap<User, SaveAccountUserDTO>().ForMember(x => x.RoleIds, opt => opt.MapFrom(x => x.Roles.Select(r => r.Id))).ReverseMap().PreserveReferences();
            CreateMap<User, CurrentUser>().ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles.Select(x => x.Name)))
                                          .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedUserName(x)))
                                          .ForMember(x => x.IsAuthenticated, opt => opt.MapFrom(x => true))
                                          .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                                          .ReverseMap().PreserveReferences();
            CreateMap<User, UserShortInfoDTO>().ForMember(x => x.RoleNames, opt => opt.MapFrom(x => x.Roles.Select(r => r.Name)))
                                               .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedUserName(x))).ReverseMap().PreserveReferences();

            #endregion

            #region Signal

            CreateMap<Signal, SignalIndexDTO>()
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x => x.Sender.Name))
                .ForMember(x => x.SenderCitizenshipNumber, opt => opt.MapFrom(x => x.Sender.CitizenshipNumber))
                .ForMember(x => x.SenderEmail, opt => opt.MapFrom(x => x.Sender.Email))
                .ForMember(x => x.SenderPhone, opt => opt.MapFrom(x => x.Sender.Phone))
                .ForMember(x => x.SenderType, opt => opt.MapFrom(x => x.Sender.SignalSenderType.Name))
                .ForMember(x => x.SenderAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Sender.Address)))
                .ForMember(x => x.ReceiverName, opt => opt.MapFrom(x => x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Person.FirstName))
                .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Case.Number))
                .ForMember(x => x.CaseStatCode, opt => opt.MapFrom(x => x.Case.StatCode))
                .ForMember(x => x.CaseYear, opt => opt.MapFrom(x => x.Case.Year))
                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                .ReverseMap().PreserveReferences();
            CreateMap<Signal, SaveSignalDTO>().ReverseMap().PreserveReferences();
            CreateMap<SignalSender, SaveSignalSenderDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Appeal

            CreateMap<Appeal, SaveAppealDTO>().ReverseMap().PreserveReferences();
            CreateMap<Appeal, AppealIndexDTO>()
                .ForMember(x => x.AppealKind, opt => opt.MapFrom(x => x.AppealKind.Description))
                .ForMember(x => x.OrderNumber, opt => opt.MapFrom(x => x.Order.Number))
                .ForMember(x => x.OrderDate, opt => opt.MapFrom(x => x.Order.Date))
                .ReverseMap().PreserveReferences();

            #endregion

            #region Entity

            CreateMap<Entity, EntityIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Entity, DetailsEntityDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Person

            CreateMap<Person, PersonIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Person, DetailsPersonDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Syndic

            CreateMap<Syndic, SyndicIndexDTO>()
                .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Syndic2Addresses.FirstOrDefault().Address)))
                .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                .ForMember(x => x.SyndicStatus, opt => opt.MapFrom(x => x.SyndicStatus.Description))
                .ForMember(x => x.InstallmentForYears,
                           opt => opt.MapFrom(x => x.Installments.Any() ? String.Join(",", x.Installments.Select(x => x.PaymentDate.Value.Year).Where(x => x != null)) : null))
                .ForMember(x => x.LastInstallmentForYear, opt => opt.MapFrom(x => x.Installments.Any() ? x.Installments.Max(x => x.PaymentDate.Value.Year.ToString()) : default))
                .ReverseMap().PreserveReferences();

            CreateMap<Syndic, SaveSyndicDTO>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address))
                .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Orders.OrderByDescending(x => x.Date).FirstOrDefault()))
                .ReverseMap().PreserveReferences();

            CreateMap<Syndic, SyndicDTO>()
                .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                .ForMember(x => x.SyndicStatus, opt => opt.MapFrom(x => x.SyndicStatus.Description))
                .ForMember(x => x.InstallmentForYears,
                           opt => opt.MapFrom(x => x.Installments.Any() ? String.Join(",", x.Installments.Select(x => x.PaymentDate.Value.Year).Where(x => x != null)) : null))
                .ForMember(x => x.LastInstallmentForYear, opt => opt.MapFrom(x => x.Installments.Any() ? x.Installments.Max(x => x.PaymentDate.Value.Year) : default))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address))
                .ReverseMap().PreserveReferences();

            CreateMap<Inspection, InspectionIndexDTO>().ForMember(x => x.InspectionTypeDescription, opt => opt.MapFrom(x => x.InspectionType.Description))
                                                       .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.SecondName, x.Syndic.LastName)))
                                                       .ReverseMap().PreserveReferences();
            CreateMap<Inspection, SaveInspectionDTO>().ReverseMap().PreserveReferences();

            CreateMap<Syndic, DetailsSyndicDTO>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Syndic2Addresses.FirstOrDefault().Address))
                .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                .ForMember(x => x.SyndicStatus, opt => opt.MapFrom(x => x.SyndicStatus.Description))
                .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Orders.OrderByDescending(x => x.DateCreated).FirstOrDefault()))
                .ForMember(x => x.InstallmentForYears,
                           opt => opt.MapFrom(x => x.Installments.Any() ? String.Join(",", x.Installments.Select(x => x.PaymentDate.Value.Year).Where(x => x != null)) : null))
                .ForMember(x => x.LastInstallmentForYear, opt => opt.MapFrom(x => x.Installments.Any() ? x.Installments.Max(x => x.PaymentDate.Value.Year) : default))
                .ReverseMap().PreserveReferences();

            CreateMap<Syndic, SyndicShortInfoDTO>().ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x)))
                                                   .ReverseMap().PreserveReferences();

            CreateMap<Syndic, ExportSyndicDTO>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x)))
                .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Syndic2Addresses.FirstOrDefault().Address)))
                .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                .ReverseMap().PreserveReferences();
            #endregion

            #region Case

            CreateMap<Case, CaseIndexDTO>()
                .ForMember(x => x.CaseKindDescription, opt => opt.MapFrom(x => x.CaseKind.Description))
                .ForMember(x => x.Side, opt => opt.MapFrom(x => x.Sides.FirstOrDefault(x => x.IsDebtor)))
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

            CreateMap<Case, DetailsCaseDTO>()
                .ForMember(x => x.Sides, opt => opt.MapFrom(x => x.Sides))
                .ForMember(x => x.Court, opt => opt.MapFrom(x => x.Court))
                .ForMember(x => x.CaseKind, opt => opt.MapFrom(x => x.CaseKind))
                .ReverseMap().PreserveReferences();

            CreateMap<Case, SaveCaseDTO>()
                .ForMember(x => x.Debtor, opt => opt.MapFrom(x => x.Sides.FirstOrDefault(x => x.IsDebtor)))
                .ReverseMap().PreserveReferences();


            CreateMap<Session, DetailsSessionDTO>()
                .ForMember(x => x.SessionKind, opt => opt.MapFrom(x => x.SessionKind))
                .ForMember(x => x.Action, opt => opt.MapFrom(x => x.Action))
                .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                .ReverseMap().PreserveReferences();

            CreateMap<Judge, DetailsJudgeDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Order

            CreateMap<Order, OrderIndexDTO>().ForMember(x => x.OrderKindName, opt => opt.MapFrom(x => x.OrderKind.Description))
                                             .ReverseMap().PreserveReferences();
            CreateMap<Order, SaveOrderDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Installment

            CreateMap<Installment, InstallmentIndexDTO>()
                .ForMember(x => x.VerifiedByName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.VerifiedByNavigation)))
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ForMember(x => x.InstallmentKindName, opt => opt.MapFrom(x => x.InstallmentKind.Description))
                .ForMember(x => x.InstallmentYear, opt => opt.MapFrom(x => x.InstallmentYear.Year))
                .ForMember(x => x.InstallmentYearAmount, opt => opt.MapFrom(x => x.InstallmentYear.Amount))
                .ReverseMap().PreserveReferences();
            CreateMap<Installment, SaveInstallmentDTO>().ForMember(x => x.VerifiedByName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.VerifiedByNavigation)))
                                                        .ReverseMap().PreserveReferences();

            #endregion

            #region Course

            CreateMap<Course, CourseIndexDTO>().ForMember(x => x.CourseKindName, opt => opt.MapFrom(x => x.CourseKind.Name)).ReverseMap().PreserveReferences();
            CreateMap<Course, SaveCourseDTO>().ReverseMap().PreserveReferences();
            CreateMap<CourseApplication, CourseApplicationIndexDTO>()
                .ForMember(x => x.CourseName, opt => opt.MapFrom(x => x.Course.Topic))
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ForMember(x => x.MainCourseName, opt => opt.MapFrom(x => x.Course.Topic))
                .ForMember(x => x.AlternateCourseName, opt => opt.MapFrom(x => x.AlternateCourse.Topic))
                .ForMember(x => x.ProgramDescription, opt => opt.MapFrom(x => x.Course.Topic))
                .ForMember(x => x.ProgramId, opt => opt.MapFrom(x => x.Course.ProgramId))
                .ReverseMap().PreserveReferences();
            CreateMap<CourseApplication, SaveCourseApplicationDTO>().ReverseMap().PreserveReferences();
            CreateMap<CourseParticipation, CourseParticipationDTO>()
                                                .ForMember(x => x.CourseTopic, opt => opt.MapFrom(x => x.CourseApplication.Course.Topic))
                                                .ForMember(x => x.LecturerDate1, opt => opt.MapFrom(x => x.CourseApplication.LecturerDate1))
                                                .ForMember(x => x.LecturerDate2, opt => opt.MapFrom(x => x.CourseApplication.LecturerDate2))
                                                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                                                .ReverseMap().PreserveReferences();
            CreateMap<CourseParticipation, SaveCourseParticipantDTO>().ReverseMap().PreserveReferences();

            CreateMap<CourseResult, CourseResultIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Program

            CreateMap<Data.DataModel.Program, ProgramIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Data.DataModel.Program, SaveProgramDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Document

            CreateMap<DocumentContent, DocumentContentDTO>().ForMember(x => x.BlobDateCreated, opt => opt.MapFrom(x => x.Blob.DateCreated)).ReverseMap().PreserveReferences();
            CreateMap<DocumentCollection, DocumentCollectionDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomAttachedDocumentKind, NomAttachedDocumentKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSignalDocumentKind, NomSignalDocumentKindDTO>().ReverseMap().PreserveReferences();

            CreateMap<DocumentContent, SaveDocumentContentDTO>().ReverseMap().PreserveReferences();
            #endregion

            #region Role

            CreateMap<Role, RoleDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region ImportedDeed

            CreateMap<ImportedDeed, ImportedDeedIndexDTO>()
                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                .ReverseMap().PreserveReferences();

            CreateMap<Trustee, TrusteeIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Side

            CreateMap<Side, SideIndexDTO>().ReverseMap().PreserveReferences();

            CreateMap<Side, DetailsSideDTO>()
                .ForMember(x => x.InvolvementKind, opt => opt.MapFrom(x => x.InvolvementKind))
                .ForMember(x => x.DebtorStatus, opt => opt.MapFrom(x => x.DebtorStatus.Description))
                .ForMember(x => x.Person, opt => opt.MapFrom(x => x.Person))
                .ForMember(x => x.Entity, opt => opt.MapFrom(x => x.Entity))

                .ReverseMap().PreserveReferences();

            #endregion

            #region StatisticalReport

            CreateMap<StatisticalReport, SaveStatisticalReportDTO>().ReverseMap().PreserveReferences();
            CreateMap<StatisticalReport, StatisticalReportIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Address

            CreateMap<Address, AddressIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Address, SaveAddressDTO>().ReverseMap().PreserveReferences();

            CreateMap<Address, DetailsAddressIndexDTO>().ForMember(x => x.SettlementName, opt => opt.MapFrom(x => x.Settlement.Name))
                                                        .ForMember(x => x.RegionName, opt => opt.MapFrom(x => x.Region.Name))
                                                        .ForMember(x => x.MunicipalityName, opt => opt.MapFrom(x => x.Municipality.Name))
                                                        .ReverseMap().PreserveReferences();

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
                                                          .ForMember(x => x.Case, opt => opt.MapFrom(x => x.Case))
                                                          .ForMember(x => x.Syndic, opt => opt.MapFrom(x => x.Syndic))
                                                          .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType))
                                                          .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType))
                                                          .ReverseMap().PreserveReferences();

            CreateMap<Object, ObjectIndexDTO>().ForMember(x => x.ObjectType, opt => opt.MapFrom(x => x.ObjectType.Description))
                                                          .ForMember(x => x.ObjectKind, opt => opt.MapFrom(x => x.ObjectKind.Description))
                                                          .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                          .ReverseMap().PreserveReferences();

            CreateMap<Announcement, DetailsAnnouncementDTO>().ForMember(x => x.LegalBasis, opt => opt.MapFrom(x => x.LegalBasis.Description))
                                                             .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType.Description))
                                                             .ForMember(x => x.OfferingProcedure, opt => opt.MapFrom(x => x.OfferingProcedure.Description))
                                                             .ForMember(x => x.OfferingKind, opt => opt.MapFrom(x => x.OfferingKind.Description))
                                                             .ForMember(x => x.PapersForSale, opt => opt.MapFrom(x => x.PapersForSale.Description))
                                                             .ForMember(x => x.SalesProcedure, opt => opt.MapFrom(x => x.SalesProcedure.Description))
                                                             .ForMember(x => x.Status, opt => opt.MapFrom(x => x.StatusCodeNavigation.Description))
                                                             .ReverseMap().PreserveReferences();

            CreateMap<Object, DetailsObjectDTO>().ForMember(x => x.ObjectKind, opt => opt.MapFrom(x => x.ObjectKind.Description))
                                                             .ForMember(x => x.ObjectType, opt => opt.MapFrom(x => x.ObjectType.Description))
                                                             .ReverseMap().PreserveReferences();

            #endregion

            #region Template

            CreateMap<Template, TemplateIndexDTO>().ForMember(x => x.TemplateKindName, opt => opt.MapFrom(x => x.TemplateKind!.Name));

            CreateMap<AdminTemplate, AdminTemplateIndexDTO>().ForMember(x => x.TemplateKind, opt => opt.MapFrom(x => x.TemplateKind!.Name))
                                                             .ForMember(x => x.DocumentContentID, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents
                                                                                                                                        .FirstOrDefault()!.Id));
            CreateMap<AdminTemplate, SaveAdminTemplateDTO>()
                .ForMember(x => x.DocumentCollectionId, opt => opt.MapFrom(x => x.DocumentCollectionId)).ReverseMap().PreserveReferences()
                .ReverseMap().PreserveReferences();
            #endregion

            #region Report

            CreateMap<Report, ReportIndexDTO>().ForMember(x => x.ReportTypeName, opt => opt.MapFrom(x => x.ReportType!.Name))
                                               .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                                               .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)));

            CreateMap<Report, DetailsSyndicReportDTO>().ForMember(x => x.ReportTypeName, opt => opt.MapFrom(x => x.ReportType!.Name))
                                                      .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                                                      .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                                                      .ForMember(x => x.DebtorName, opt => opt.MapFrom(x => x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Entity.Name))
                                                      .ForMember(x => x.DebtorBulstat, opt => opt.MapFrom(x => x.Case.Sides.FirstOrDefault(x => x.IsDebtor).Entity.Bulstat));

            #endregion

            #region Syndic Report Template

            CreateMap<ReportTemplate, ReportTemplateIndexDTO>().ForMember(x => x.ReportType, opt => opt.MapFrom(x => x.ReportType!.Name))
                                                             .ForMember(x => x.DocumentContentID, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents
                                                                                                                                        .FirstOrDefault()!.Id));
            CreateMap<ReportTemplate, SaveReportTemplateDTO>()
                .ForMember(x => x.DocumentCollectionId, opt => opt.MapFrom(x => x.DocumentCollectionId)).ReverseMap().PreserveReferences()
                .ReverseMap().PreserveReferences();
            #endregion

            #region Administration

            CreateMap<LogApiRequest, LogApiRequestIndexDTO>()
                .ForMember(x => x.RequestContent, opt => opt.MapFrom(x => x.RequestContent.Substring(0, 300)))
                .ForMember(x => x.ResponseContent, opt => opt.MapFrom(x => x.ResponseContent.Substring(0, 300)))
                .ReverseMap().PreserveReferences();
            CreateMap<LogApiRequest, DetailsLogApiRequestDTO>().ReverseMap().PreserveReferences();


            CreateMap<LogUserAction, UserActionIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<LogUserAction, DetailsUserActionDTO>().ReverseMap().PreserveReferences();

            CreateMap<LogDbcontext, LogDbcontextIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<LogDbcontext, DetailsLogDbcontexDTO>().ReverseMap().PreserveReferences();

            CreateMap<TemplateArticle28, IndexTemplateArticles28DTO>()
                .ForMember(x => x.DirectiveTemplateKindName, opt => opt.MapFrom(x => x.DirectiveTemplateKind.Description))
                .ForMember(x => x.DocumentContentId, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents.FirstOrDefault().Id)).ReverseMap().PreserveReferences();
            CreateMap<TemplateArticle28, SaveTemplateArticles28DTO>()
                .ForMember(x => x.DocumentCollectionId, opt => opt.MapFrom(x => x.DocumentCollectionId)).ReverseMap().PreserveReferences()
                .ReverseMap().PreserveReferences();
            #endregion

            #region Plea

            CreateMap<Plea, SavePleaDTO>().ReverseMap().PreserveReferences();
            CreateMap<Plea, PleaIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Setting

            CreateMap<Setting, SettingDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region TemplateDocument

            CreateMap<TemplateDocument, TemplateDocumentIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<TemplateDocument, SaveTemplateDocumentDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Activity

            CreateMap<Activity, ActivityIndexDTO>()
                .ForMember(x => x.ActivityKindName, opt => opt.MapFrom(x => x.ActivityKind.Description))
                .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ReverseMap().PreserveReferences();


            #endregion

            #region Property
            CreateMap<Property, DetailsPropertyDTO>()
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

            #region Entity

            CreateProjection<Entity, EntityDTO>();

            #endregion

            #region Person

            CreateProjection<Person, PersonDTO>().ForMember(x => x.FullName, opt => opt.MapFrom(x => GetConcatenatedPersonName(x)));

            #endregion

            #region EntityStatisticalData

            CreateMap<EntityStatisticalDatum, DetailsEntityStatisticalDataDTO>().ForMember(x => x.CompanySizeName, opt => opt.MapFrom(x => x.CompanySize.Description)).ReverseMap().PreserveReferences();

            #endregion

            #region LogException

            CreateMap<LogException, DetailsLogExceptionRequestDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Act

            CreateMap<Act, ActAnnouncementIndexDTO>()
                .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Session.Case.Number))
                .ForMember(x => x.CaseYear, opt => opt.MapFrom(x => x.Session.Case.Year))
                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Session.Case.Court.Name))
                .ForMember(x => x.AnnouncementStatus, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().AnnouncementStatus.Description))
                .ForMember(x => x.RegistrationStatus, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().RegistrationStatus.Description))
                .ForMember(x => x.ProprietorName, opt => opt.MapFrom(x => x.Session.Case.Sides.FirstOrDefault(x => x.Entity.Name != null).Entity.Name))
                .ReverseMap().PreserveReferences();
            CreateMap<Act, SaveActAnnouncementDTO>().ReverseMap().PreserveReferences();
            CreateMap<Act, SaveActDTO>().ReverseMap().PreserveReferences();

            CreateMap<Act, DetailsActDTO>()
                .ForMember(x => x.ActKind, opt => opt.MapFrom(x => x.ActKind.Description))
                .ForMember(x => x.ActCategory, opt => opt.MapFrom(x => x.ActCategory.Description))
                .ForMember(x => x.ActContractor, opt => opt.MapFrom(x => x.ActContractor.Description))
                .ForMember(x => x.ActReason, opt => opt.MapFrom(x => x.ActReason.Description))
                .ForMember(x => x.CaseNumber, opt => opt.MapFrom(x => x.Session.Case.Number))
                .ForMember(x => x.CaseYear, opt => opt.MapFrom(x => x.Session.Case.Year))
                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Session.Case.Court.Name))
                .ForMember(x => x.ProprietorName, opt => opt.MapFrom(x => x.Session.Case.Sides.FirstOrDefault(x => x.Entity.Name != null).Entity.Name))
                .ForMember(x => x.AnnouncementStatus, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().AnnouncementStatus.Description))
                .ForMember(x => x.AnnouncementStatusId, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().AnnouncementStatus.Id))
                .ForMember(x => x.RegistrationStatus, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().RegistrationStatus.Description))
                .ForMember(x => x.RegistrationStatusId, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().RegistrationStatus.Id))
                .ForMember(x => x.ActAnnouncementId, opt => opt.MapFrom(x => x.ActAnnouncements.FirstOrDefault().Id))
                .ReverseMap().PreserveReferences();

            CreateMap<RegisterEntry, RegisterEntryIndexDTO>()
                .ForMember(x => x.ActKind, opt => opt.MapFrom(x => x.Act.ActKind.Description))
                .ForMember(x => x.ActNumber, opt => opt.MapFrom(x => x.Act.Number))
                .ForMember(x => x.AnnouncedByUser, opt => opt.MapFrom(x => GetConcatenatedUserName(x.AnnouncedByUser)))
                .ForMember(x => x.LegalBasisName, opt => opt.MapFrom(x => x.LegalBasis.Description))
                .ForMember(x => x.FieldName, opt => opt.MapFrom(x => x.Field.Description))
                .ReverseMap().PreserveReferences();
            CreateMap<RegisterEntry, SaveRegisterEntryDTO>().ReverseMap().PreserveReferences();
            CreateMap<RegisterEntry, RegisterEntryDTO>()
                .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                .ForMember(x => x.FieldName, opt => opt.MapFrom(x => x.Field.Description))
                .ForMember(x => x.LegalBasisName, opt => opt.MapFrom(x => x.LegalBasis.Description))
                .ForMember(x => x.RegisterSyndicKindName, opt => opt.MapFrom(x => x.RegisterSyndicKind.Description))
                .ForMember(x => x.SyndicFullName, opt => opt.MapFrom(x => GetConcatenatedSyndicName(x.Syndic)))
                .ReverseMap().PreserveReferences();

            CreateMap<ActAnnouncement, DetailsActAnnouncementDTO>()
                .ForMember(x => x.ActCategory, opt => opt.MapFrom(x => x.Act.ActCategory.Description))
                .ForMember(x => x.AnnouncementStatus, opt => opt.MapFrom(x => x.AnnouncementStatus.Description))
                .ForMember(x => x.RegistrationStatus, opt => opt.MapFrom(x => x.RegistrationStatus.Description))
                .ReverseMap().PreserveReferences();
            CreateMap<ActAnnouncement, SaveActAnnouncementDTO>().ReverseMap().PreserveReferences();

            CreateMap<ActAnnouncement, ActAnnouncementDTO>()
                .ForMember(x => x.ActCategoryname, opt => opt.MapFrom(x => x.ActCategory.Description))
                .ForMember(x => x.AnnouncedByUserName, opt => opt.MapFrom(x => GetConcatenatedUserName(x.AnnouncedByUser)))
                .ReverseMap().PreserveReferences();

            #endregion

            #region DocumentLegalBasis

            CreateMap<DocumentLegalBasis, SaveDocumentLegalBasisDTO>().ReverseMap().PreserveReferences();

            CreateMap<DocumentLegalBasis, DocumentLegalBasisIndexDTO>()
                .ForMember(x => x.DocumentContentId, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents.FirstOrDefault().Id))
                .ReverseMap().PreserveReferences();

            #endregion

            #region Session

            CreateMap<Session, SessionDTO>().ReverseMap().PreserveReferences();

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
    }
}
