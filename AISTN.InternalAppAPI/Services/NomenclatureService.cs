using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Helper;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using Newtonsoft.Json;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class NomenclatureService : ServiceBase
    {
        private readonly IGenericRepository<NomAction> _nomActionRepository;
        private readonly IGenericRepository<NomActKind> _nomActKindRepository;
        private readonly IGenericRepository<NomActReason> _nomActReasonRepository;
        private readonly IGenericRepository<NomAttachedDocumentKind> _nomAttachedDocumentKindRepository;
        private readonly IGenericRepository<NomCaseCode> _nomCaseCodeRepository;
        private readonly IGenericRepository<NomCaseKind> _nomCaseKindRepository;
        private readonly IGenericRepository<NomCourt> _nomCourtRepository;
        private readonly IGenericRepository<NomIncomingDocumentKind> _nomIncomingDocumentKindRepository;
        private readonly IGenericRepository<NomInvolvementKind> _nomInvolvementKindRepository;
        private readonly IGenericRepository<NomOrderKind> _nomOrderKindRepository;
        private readonly IGenericRepository<NomReportSource> _nomReportSourceRepository;
        private readonly IGenericRepository<NomReportType> _nomReportTypeRepository;
        private readonly IGenericRepository<NomSendToKind> _nomSendToKindRepository;
        private readonly IGenericRepository<NomSessionKind> _nomSessionKindRepository;
        private readonly IGenericRepository<NomSessionResult> _nomSessionResultRepository;
        private readonly IGenericRepository<NomSpecialty> _nomSpecialtyRepository;
        private readonly IGenericRepository<NomTemplateKind> _nomTemplateKindRepository;
        private readonly IGenericRepository<NomDebtorStatus> _nomDebtorStatusRepository;
        private readonly IGenericRepository<NomLegalBasis> _nomLegalBasisRepository;
        private readonly IGenericRepository<NomOfferingLocationType> _nomOfferingLocationTypeRepository;
        private readonly IGenericRepository<NomOfferingProcedure> _nomOfferingProcedureRepository;
        private readonly IGenericRepository<NomOfferingKind> _nomOfferingKindRepository;
        private readonly IGenericRepository<NomPapersForSale> _nomPapersForSaleRepository;
        private readonly IGenericRepository<NomSalesProcedure> _nomSalesProcedureRepository;
        private readonly IGenericRepository<NomSyndicStatus> _nomSyndicStatusRepository;
        private readonly IGenericRepository<NomSellAnnouncementTemplate> _nomAnnouncementTemplate;
        private readonly IGenericRepository<NomInstallmentYear> _nomInstallmentYearRepository;
        private readonly IGenericRepository<NomAnnouncementStatus> _nomAnnouncementStatusRepository;
        private readonly IGenericRepository<NomDirectiveTemplateKind> _directiveTemplate;
        private readonly IGenericRepository<NomAppealKind> _nomAppealKindRepository;
        private readonly IGenericRepository<NomSignalSenderType> _nomSignalSenderTypeRepository;
        private readonly IGenericRepository<NomSignalDocumentKind> _nomSignalDocumentKindRepository;
        private readonly IGenericRepository<NomInspectionType> _nomInspectionTypeRepository;
        private readonly IGenericRepository<NomObjectKind> _nomObjectKindRepository;
        private readonly IGenericRepository<NomObjectType> _nomObjectTypeRepository;
        private readonly IGenericRepository<NomSyndicCaseReport> _nomSyndicCaseReportRepository;
        private readonly IGenericRepository<NomCourseKind> _nomCourseKindRepository;
        private readonly IGenericRepository<NomOrderPaymentKind> _nomOrderPaymentKindRepository;
        //private readonly IGenericRepository<NomSyndicAction> _nomSyndicActionRepository;
        //private readonly IGenericRepository<NomActCategory> _nomActCategoryRepository;
        //private readonly IGenericRepository<NomActContractor> _nomActContractorRepository;
        private readonly IGenericRepository<NomActivityKind> _nomActivityKindRepository;
        private readonly IGenericRepository<NomSampleKind> _nomSampleKindRepository;
        private readonly IGenericRepository<NomSyndicReportType> _nomSyndicReportTypeRepository;
        private readonly IGenericRepository<NomCompanySize> _nomCompanySizeRepository;
        private readonly IGenericRepository<NomActAnnouncementCategory> _nomActAnnouncementCategoryRepo;
        private readonly IGenericRepository<NomRegistrationLegalBasis> _nomRegistrationLegalBasis;
        private readonly IGenericRepository<NomRegisterField> _nomRegisterField;
        private readonly IGenericRepository<NomSyndicKind> _nomSyndicKind;
        private readonly IGenericRepository<NomRegisterSyndicKind> _nomRegisterSyndicKind;

        public NomenclatureService(IMapper mapper,
                                   ExceptionLogger logger,
                                   ExcelGenerator excelGenerator,
                                   IGenericRepository<NomAction> nomActionRepository,
                                   IGenericRepository<NomActKind> nomActKindRepository,
                                   IGenericRepository<NomActReason> nomActReasonRepository,
                                   IGenericRepository<NomAttachedDocumentKind> nomAttachedDocumentKindRepository,
                                   IGenericRepository<NomCaseCode> nomCaseCodeRepository,
                                   IGenericRepository<NomCaseKind> nomCaseKindRepository,
                                   IGenericRepository<NomCourt> nomCourtRepository,
                                   IGenericRepository<NomIncomingDocumentKind> nomIncomingDocumentKindRepository,
                                   IGenericRepository<NomInvolvementKind> nomInvolvementKindRepository,
                                   IGenericRepository<NomOrderKind> nomOrderKindRepository,
                                   IGenericRepository<NomReportSource> nomReportSourceRepository,
                                   IGenericRepository<NomReportType> nomReportTypeRepository,
                                   IGenericRepository<NomSendToKind> nomSendToKindRepository,
                                   IGenericRepository<NomSessionKind> nomSessionKindRepository,
                                   IGenericRepository<NomSessionResult> nomSessionResultRepository,
                                   IGenericRepository<NomSpecialty> nomSpecialtyRepository,
                                   IGenericRepository<NomTemplateKind> nomTemplateKindRepository,
                                   IGenericRepository<NomDebtorStatus> nomDebtorStatusRepository,
                                   IGenericRepository<NomLegalBasis> nomLegalBasisRepository,
                                   IGenericRepository<NomOfferingLocationType> nomOfferingLocationTypeRepository,
                                   IGenericRepository<NomOfferingProcedure> nomOfferingProcedureRepository,
                                   IGenericRepository<NomOfferingKind> nomOfferingKindRepository,
                                   IGenericRepository<NomPapersForSale> nomPapersForSaleRepository,
                                   IGenericRepository<NomSalesProcedure> nomSalesProcedureRepository,
                                   IGenericRepository<NomSyndicStatus> nomSyndicStatusRepository,
                                   IGenericRepository<NomSellAnnouncementTemplate> nomAnnouncementTemplate,
                                   IGenericRepository<NomInstallmentYear> nomInstallmentYearRepository,
                                   IGenericRepository<NomAnnouncementStatus> nomAnnouncementStatusRepository,
                                   IGenericRepository<NomDirectiveTemplateKind> directiveTemplate,
                                   IGenericRepository<NomAppealKind> nomAppealKindRepository,
                                   IGenericRepository<NomSignalSenderType> nomSignalSenderTypeRepository,
                                   IGenericRepository<NomSignalDocumentKind> nomSignalDocumentKindRepository,
                                   IGenericRepository<NomInspectionType> nomInspectionTypeRepository,
                                   IGenericRepository<NomObjectKind> nomObjectKindRepository,
                                   IGenericRepository<NomObjectType> nomObjectTypeRepository,
                                   IGenericRepository<NomSyndicCaseReport> nomSyndicCaseReportRepository,
                                   IGenericRepository<NomCourseKind> nomCourseKindRepository,
                                   IGenericRepository<NomOrderPaymentKind> nomOrderPaymentKindRepository,
                                   IGenericRepository<NomActivityKind> nomActivityKindRepository,
                                   IGenericRepository<NomSampleKind> nomSampleKindRepository,
                                   IGenericRepository<NomSyndicReportType> nomSyndicReportTypeRepository,
                                   IGenericRepository<NomCompanySize> nomCompanySizeRepository,
                                   IGenericRepository<NomActAnnouncementCategory> nomActAnnouncementCategoryRepo,
                                   IGenericRepository<NomRegistrationLegalBasis> nomRegistrationLegalBasis, 
                                   IGenericRepository<NomRegisterField> nomRegisterField,
                                   IGenericRepository<NomSyndicKind> nomSyndicKind,
                                   IGenericRepository<NomRegisterSyndicKind> nomRegisterSyndicKind)
                            : base(mapper, logger, excelGenerator)
        {
            _nomActionRepository = nomActionRepository;
            _nomActKindRepository = nomActKindRepository;
            _nomActReasonRepository = nomActReasonRepository;
            _nomAttachedDocumentKindRepository = nomAttachedDocumentKindRepository;
            _nomCaseCodeRepository = nomCaseCodeRepository;
            _nomCaseKindRepository = nomCaseKindRepository;
            _nomCourtRepository = nomCourtRepository;
            _nomIncomingDocumentKindRepository = nomIncomingDocumentKindRepository;
            _nomInvolvementKindRepository = nomInvolvementKindRepository;
            _nomOrderKindRepository = nomOrderKindRepository;
            _nomReportSourceRepository = nomReportSourceRepository;
            _nomReportTypeRepository = nomReportTypeRepository;
            _nomSendToKindRepository = nomSendToKindRepository;
            _nomSessionKindRepository = nomSessionKindRepository;
            _nomSessionResultRepository = nomSessionResultRepository;
            _nomSpecialtyRepository = nomSpecialtyRepository;
            _nomTemplateKindRepository = nomTemplateKindRepository;
            _nomDebtorStatusRepository = nomDebtorStatusRepository;
            _nomLegalBasisRepository = nomLegalBasisRepository;
            _nomOfferingLocationTypeRepository = nomOfferingLocationTypeRepository;
            _nomOfferingProcedureRepository = nomOfferingProcedureRepository;
            _nomOfferingKindRepository = nomOfferingKindRepository;
            _nomPapersForSaleRepository = nomPapersForSaleRepository;
            _nomSalesProcedureRepository = nomSalesProcedureRepository;
            _nomSyndicStatusRepository = nomSyndicStatusRepository;
            _nomAnnouncementTemplate = nomAnnouncementTemplate;
            _nomInstallmentYearRepository = nomInstallmentYearRepository;
            _nomAnnouncementStatusRepository = nomAnnouncementStatusRepository;
            _directiveTemplate = directiveTemplate;
            _nomAppealKindRepository = nomAppealKindRepository;
            _nomSignalSenderTypeRepository = nomSignalSenderTypeRepository;
            _nomSignalDocumentKindRepository = nomSignalDocumentKindRepository;
            _nomInspectionTypeRepository = nomInspectionTypeRepository;
            _nomObjectKindRepository = nomObjectKindRepository;
            _nomObjectTypeRepository = nomObjectTypeRepository;
            _nomSyndicCaseReportRepository = nomSyndicCaseReportRepository;
            _nomCourseKindRepository = nomCourseKindRepository;
            _nomOrderPaymentKindRepository = nomOrderPaymentKindRepository;
            _nomActivityKindRepository = nomActivityKindRepository;
            _nomSampleKindRepository = nomSampleKindRepository;
            _nomSyndicReportTypeRepository = nomSyndicReportTypeRepository;
            _nomCompanySizeRepository = nomCompanySizeRepository;
            _nomActAnnouncementCategoryRepo = nomActAnnouncementCategoryRepo;
            _nomRegistrationLegalBasis = nomRegistrationLegalBasis;
            _nomRegisterField = nomRegisterField;
            _nomSyndicKind = nomSyndicKind;
            _nomRegisterSyndicKind = nomRegisterSyndicKind;
        }

        public OperationResult<List<NomenclatureTypeDTO>> GetNomenclatureEnums()
        {
            string[] names = Enum.GetNames(typeof(NomenclatureEnums));

            for (int i = 0; i < NomenclatureTypeBG.BgNames.Count; i++)
            {
                NomenclatureTypeBG.BgNames[i].Key = names[i];
            }

            return Success(NomenclatureTypeBG.BgNames);
        }

        public OperationResult<List<object>> GetNomenclatures(NomenclatureEnums type)
        {
            try
            {
                switch (type)
                {
                    case NomenclatureEnums.Action:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomActionRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ActKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomActKind>>(_nomActKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ActReason:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomActReason>>(_nomActReasonRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.AttachedDocumentKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomAttachedDocumentKindDTO>>(_nomAttachedDocumentKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.CaseCode:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomCaseCodeDTO>>(_nomCaseCodeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.CaseKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomCaseKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.CourtNumber:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomCourtDTO>>(_nomCourtRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.AppellateCourt:
                        var result = _nomCourtRepository.Get(x => x.Number != 1).OrderBy(x => x.Number);

                        List<AppellateCourtDTO> courts = new List<AppellateCourtDTO>();
                        foreach (var item in result)
                        {
                            courts.Add(new AppellateCourtDTO
                            {
                                Id = item.Id,
                                Number = item.Number,
                                AppellateCourtName = item.Number.ToString().EndsWith("00") ? item.Name : null,
                                CourtName = !item.Number.ToString().EndsWith("00") ? item.Name : null
                            });
                        }

                        return Success(_mapper.Map<List<object>>(courts));

                    case NomenclatureEnums.IncomingDocumentKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomIncomingDocumentKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.InvolvementKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomInvolvementKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.OrderKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomOrderKindDTO>>(_nomOrderKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ReportSource:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomReportSourceDTO>>(_nomReportSourceRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ReportType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomReportTypeDTO>>(_nomReportTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SendToKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomSendToKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SessionKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomSessionKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SessionResult:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomSessionResultRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.Specialty:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSpecialityDTO>>(_nomSpecialtyRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.TemplateKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomTemplateKindDTO>>(_nomTemplateKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.DebtorStatus:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomDebtorStatusDTO>>(_nomDebtorStatusRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.LegalBasis:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomLegalBasisDTO>>(_nomLegalBasisRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.OfferingLocationType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingLocationTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.OfferingProcedure:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingProcedureRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.OfferingKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.PapersForSale:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomPapersForSaleRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SalesProcedure:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomSalesProcedureRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SyndicStatus:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSyndicStatusDTO>>(_nomSyndicStatusRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SellAnnouncementTemplate:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<SellAnnouncementTemplateDTO>>(_nomAnnouncementTemplate.GetAll().OrderBy(x => x.Number))));

                    case NomenclatureEnums.InstallmentYear:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomInstallmentYearDTO>>(_nomInstallmentYearRepository.GetAll().OrderBy(x => x.Year))));

                    case NomenclatureEnums.AnnouncementStatus:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomAnnouncementStatusRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.DirectiveTemplate:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_directiveTemplate.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.AppealKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomAppealKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SignalSenderType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSignalSenderTypeDTO>>(_nomSignalSenderTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SignalDocumentKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSignalDocumentKindDTO>>(_nomSignalDocumentKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.InspectionType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomInspectionTypeDTO>>(_nomInspectionTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ObjectKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomObjectKindDTO>>(_nomObjectKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ObjectType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomObjectTypeDTO>>(_nomObjectTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SyndicCaseReport:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSyndicCaseReportDTO>>(_nomSyndicCaseReportRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.CourseKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomCourseKindDTO>>(_nomCourseKindRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.OrderPaymentKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomOrderPaymentKindDTO>>(_nomOrderPaymentKindRepository.GetAll().OrderBy(x => x.Id))));

                    //case NomenclatureEnums.SyndicAction:
                    //    return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSyndicActionDTO>>(_nomSyndicActionRepository.GetAll().OrderBy(x => x.Id))));

                    //case NomenclatureEnums.ActCategory:
                    //    return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomActCategoryDTO>>(_nomActCategoryRepository.GetAll().OrderBy(x => x.Id))));

                    //case NomenclatureEnums.ActContractor:
                    //    return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomActContractorDTO>>(_nomActContractorRepository.GetAll().OrderBy(x => x.Id))));
                    
                    case NomenclatureEnums.ActivityKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomActivityKindRepository.GetAll().OrderBy(x => x.Id))));
                    
                    case NomenclatureEnums.SampleKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomSampleKindRepository.GetAll().OrderBy(x => x.Id))));
                    
                    case NomenclatureEnums.SyndicReportType:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomenclatureDTO>>(_nomSyndicReportTypeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.CompanySize:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomCompanySizeDTO>>(_nomCompanySizeRepository.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.ActAnnouncementCategory:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomActAnnouncementCategoryDTO>>(_nomActAnnouncementCategoryRepo.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.RegistrationLegalBasis:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomRegistrationLegalBasisDTO>>(_nomRegistrationLegalBasis.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.SyndicKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomSyndicKindDTO>>(_nomSyndicKind.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.RegisterField:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomRegisterFieldDTO>>(_nomRegisterField.GetAll().OrderBy(x => x.Id))));

                    case NomenclatureEnums.RegisterSyndicKind:
                        return Success(_mapper.Map<List<object>>(_mapper.Map<List<NomRegisterSyndicKindDTO>>(_nomRegisterSyndicKind.GetAll().OrderBy(x => x.Id))));

                    default:
                        _logger.LogException(new ArgumentOutOfRangeException(nameof(type), type, null));
                        return Exception<List<object>>(new Exception("В момента тази номенклатура не може да бъде показана."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<object>>(ex);
            }
        }

        public OperationResult<object> GetNomenclatureById(Guid id, NomenclatureEnums type)
        {
            try
            {
                switch (type)
                {
                    case NomenclatureEnums.Action:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomActionRepository.GetById(id))));
                    case NomenclatureEnums.ActKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomActKindRepository.GetById(id))));
                    case NomenclatureEnums.ActReason:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomActReasonRepository.GetById(id))));
                    case NomenclatureEnums.AttachedDocumentKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomAttachedDocumentKindDTO>(_nomAttachedDocumentKindRepository.GetById(id))));
                    case NomenclatureEnums.CaseCode:
                        return Success(_mapper.Map<object>(_mapper.Map<NomCaseCodeDTO>(_nomCaseCodeRepository.GetById(id))));
                    case NomenclatureEnums.CaseKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomCaseKindRepository.GetById(id))));
                    case NomenclatureEnums.CourtNumber:
                        return Success(_mapper.Map<object>(_mapper.Map<NomCourtDTO>(_nomCourtRepository.GetById(id))));
                    case NomenclatureEnums.IncomingDocumentKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomIncomingDocumentKindRepository.GetById(id))));
                    case NomenclatureEnums.InvolvementKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomInvolvementKindRepository.GetById(id))));
                    case NomenclatureEnums.OrderKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomOrderKindDTO>(_nomOrderKindRepository.GetById(id))));
                    case NomenclatureEnums.ReportSource:
                        return Success(_mapper.Map<object>(_mapper.Map<NomReportSourceDTO>(_nomReportSourceRepository.GetById(id))));
                    case NomenclatureEnums.ReportType:
                        return Success(_mapper.Map<object>(_mapper.Map<NomReportTypeDTO>(_nomReportTypeRepository.GetById(id))));
                    case NomenclatureEnums.SendToKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomSendToKindRepository.GetById(id))));
                    case NomenclatureEnums.SessionKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomSessionKindRepository.GetById(id))));
                    case NomenclatureEnums.SessionResult:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomSessionResultRepository.GetById(id))));
                    case NomenclatureEnums.Specialty:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSpecialityDTO>(_nomSpecialtyRepository.GetById(id))));
                    case NomenclatureEnums.TemplateKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomTemplateKindDTO>(_nomTemplateKindRepository.GetById(id))));
                    case NomenclatureEnums.DebtorStatus:
                        return Success(_mapper.Map<object>(_mapper.Map<NomDebtorStatusDTO>(_nomDebtorStatusRepository.GetById(id))));
                    case NomenclatureEnums.LegalBasis:
                        return Success(_mapper.Map<object>(_mapper.Map<NomLegalBasisDTO>(_nomLegalBasisRepository.GetById(id))));
                    case NomenclatureEnums.OfferingLocationType:
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(_nomOfferingLocationTypeRepository.GetById(id))));
                    case NomenclatureEnums.OfferingProcedure:
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(_nomOfferingProcedureRepository.GetById(id))));
                    case NomenclatureEnums.OfferingKind:
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(_nomOfferingKindRepository.GetById(id))));
                    case NomenclatureEnums.PapersForSale:
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(_nomPapersForSaleRepository.GetById(id))));
                    case NomenclatureEnums.SalesProcedure:
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(_nomSalesProcedureRepository.GetById(id))));
                    case NomenclatureEnums.SyndicStatus:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicStatusDTO>(_nomSyndicStatusRepository.GetById(id))));
                    case NomenclatureEnums.SellAnnouncementTemplate:
                        return Success(_mapper.Map<object>(_mapper.Map<SellAnnouncementTemplateDTO>(_nomAnnouncementTemplate.GetById(id))));
                    case NomenclatureEnums.InstallmentYear:
                        return Success(_mapper.Map<object>(_mapper.Map<NomInstallmentYearDTO>(_nomInstallmentYearRepository.GetById(id))));
                    case NomenclatureEnums.AnnouncementStatus:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomAnnouncementStatusRepository.GetById(id))));
                    case NomenclatureEnums.DirectiveTemplate:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_directiveTemplate.GetById(id))));
                    case NomenclatureEnums.AppealKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomAppealKindRepository.GetById(id))));
                    case NomenclatureEnums.SignalSenderType:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSignalSenderTypeDTO>(_nomSignalSenderTypeRepository.GetById(id))));
                    case NomenclatureEnums.SignalDocumentKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSignalDocumentKindDTO>(_nomSignalDocumentKindRepository.GetById(id))));
                    case NomenclatureEnums.InspectionType:
                        return Success(_mapper.Map<object>(_mapper.Map<NomInspectionTypeDTO>(_nomInspectionTypeRepository.GetById(id))));
                    case NomenclatureEnums.ObjectKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomObjectKindDTO>(_nomObjectKindRepository.GetById(id))));
                    case NomenclatureEnums.ObjectType:
                        return Success(_mapper.Map<object>(_mapper.Map<NomObjectTypeDTO>(_nomObjectTypeRepository.GetById(id))));
                    case NomenclatureEnums.SyndicCaseReport:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicCaseReportDTO>(_nomSyndicCaseReportRepository.GetById(id))));
                    case NomenclatureEnums.CourseKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomCourseKindDTO>(_nomCourseKindRepository.GetById(id))));
                    case NomenclatureEnums.OrderPaymentKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomOrderPaymentKindDTO>(_nomOrderPaymentKindRepository.GetById(id))));
                    //case NomenclatureEnums.SyndicAction:
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomSyndicActionDTO>(_nomSyndicActionRepository.GetById(id))));
                    //case NomenclatureEnums.ActCategory:
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomActCategoryDTO>(_nomActCategoryRepository.GetById(id))));
                    //case NomenclatureEnums.ActContractor:
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomActContractorDTO>(_nomActContractorRepository.GetById(id))));
                    case NomenclatureEnums.ActivityKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>((_nomActivityKindRepository).GetById(id))));
                    case NomenclatureEnums.SampleKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomSampleKindRepository.GetById(id))));
                    case NomenclatureEnums.SyndicReportType:
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(_nomSyndicReportTypeRepository.GetById(id))));
                    case NomenclatureEnums.CompanySize:
                        return Success(_mapper.Map<object>(_mapper.Map<NomCompanySizeDTO>(_nomCompanySizeRepository.GetById(id))));
                    case NomenclatureEnums.ActAnnouncementCategory:
                        return Success(_mapper.Map<object>(_mapper.Map<NomActAnnouncementCategoryDTO>(_nomActAnnouncementCategoryRepo.GetById(id))));
                    case NomenclatureEnums.RegistrationLegalBasis:
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegistrationLegalBasisDTO>(_nomRegistrationLegalBasis.GetById(id))));
                    case NomenclatureEnums.SyndicKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicKindDTO>(_nomSyndicKind.GetById(id))));
                    case NomenclatureEnums.RegisterField:
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegisterFieldDTO>(_nomRegisterField.GetById(id))));
                    case NomenclatureEnums.RegisterSyndicKind:
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegisterSyndicKindDTO>(_nomRegisterSyndicKind.GetById(id))));
                    default:
                        _logger.LogException(new ArgumentOutOfRangeException(nameof(type), type, null));
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<object>(ex);
            }
        }

        public OperationResult<object> InsertNomenclature(object nomenclature, NomenclatureEnums type)
        {
            try
            {
                const string successNomenclatureMessage = $"Номенклатурата е добавена.";

                switch (type)
                {
                    case NomenclatureEnums.Action:
                        var dbMappedAction = JsonConvert.DeserializeObject<NomAction>(nomenclature.ToString());
                        _nomActionRepository.Add(dbMappedAction);
                        _nomActionRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedAction)), successNomenclatureMessage);

                    case NomenclatureEnums.ActKind:
                        var dbMappedActKind = JsonConvert.DeserializeObject<NomActKind>(nomenclature.ToString());
                        _nomActKindRepository.Add(dbMappedActKind);
                        _nomActKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedActKind)), successNomenclatureMessage);

                    case NomenclatureEnums.ActReason:
                        var dbMappedActReason = JsonConvert.DeserializeObject<NomActReason>(nomenclature.ToString());
                        _nomActReasonRepository.Add(dbMappedActReason);
                        _nomActReasonRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedActReason)), successNomenclatureMessage);

                    case NomenclatureEnums.AttachedDocumentKind:
                        var dbMappedAttachedDocumentKind = JsonConvert.DeserializeObject<NomAttachedDocumentKind>(nomenclature.ToString());
                        _nomAttachedDocumentKindRepository.Add(dbMappedAttachedDocumentKind);
                        _nomAttachedDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedAttachedDocumentKind)), successNomenclatureMessage);

                    case NomenclatureEnums.CaseCode:
                        var dbMappedCaseCode = JsonConvert.DeserializeObject<NomCaseCode>(nomenclature.ToString());
                        _nomCaseCodeRepository.Add(dbMappedCaseCode);
                        _nomCaseCodeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomCaseCodeDTO>(dbMappedCaseCode)), successNomenclatureMessage);

                    case NomenclatureEnums.CaseKind:
                        var dbMappedCaseKind = JsonConvert.DeserializeObject<NomCaseKind>(nomenclature.ToString());
                        _nomCaseKindRepository.Add(dbMappedCaseKind);
                        _nomCaseKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedCaseKind)), successNomenclatureMessage);

                    case NomenclatureEnums.CourtNumber:
                        var dbMappedCourt = JsonConvert.DeserializeObject<NomCourt>(nomenclature.ToString());
                        _nomCourtRepository.Add(dbMappedCourt);
                        _nomCourtRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedCourt)), successNomenclatureMessage);

                    case NomenclatureEnums.IncomingDocumentKind:
                        var dbMappedIncomingDocumentKind = JsonConvert.DeserializeObject<NomIncomingDocumentKind>(nomenclature.ToString());
                        _nomIncomingDocumentKindRepository.Add(dbMappedIncomingDocumentKind);
                        _nomIncomingDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedIncomingDocumentKind)), successNomenclatureMessage);

                    case NomenclatureEnums.InvolvementKind:
                        var dbMappedInvolvementKind = JsonConvert.DeserializeObject<NomInvolvementKind>(nomenclature.ToString());
                        _nomInvolvementKindRepository.Add(dbMappedInvolvementKind);
                        _nomInvolvementKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedInvolvementKind)), successNomenclatureMessage);

                    case NomenclatureEnums.OrderKind:
                        var dbMappedOrderKind = JsonConvert.DeserializeObject<NomOrderKind>(nomenclature.ToString());
                        _nomOrderKindRepository.Add(dbMappedOrderKind);
                        _nomOrderKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedOrderKind)), successNomenclatureMessage);

                    case NomenclatureEnums.ReportSource:
                        var dbMappedReportSource = JsonConvert.DeserializeObject<NomReportSource>(nomenclature.ToString());
                        _nomReportSourceRepository.Add(dbMappedReportSource);
                        _nomReportSourceRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedReportSource)), successNomenclatureMessage);

                    case NomenclatureEnums.ReportType:
                        var dbMappedReportType = JsonConvert.DeserializeObject<NomReportType>(nomenclature.ToString());
                        _nomReportTypeRepository.Add(dbMappedReportType);
                        _nomReportTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedReportType)), successNomenclatureMessage);

                    case NomenclatureEnums.SendToKind:
                        var dbMappedSendToKind = JsonConvert.DeserializeObject<NomSendToKind>(nomenclature.ToString());
                        _nomSendToKindRepository.Add(dbMappedSendToKind);
                        _nomSendToKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedSendToKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SessionKind:
                        var dbMappedAttachedSessionKind = JsonConvert.DeserializeObject<NomSessionKind>(nomenclature.ToString());
                        _nomSessionKindRepository.Add(dbMappedAttachedSessionKind);
                        _nomSessionKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedAttachedSessionKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SessionResult:
                        var dbMappedSessionResult = JsonConvert.DeserializeObject<NomSessionResult>(nomenclature.ToString());
                        _nomSessionResultRepository.Add(dbMappedSessionResult);
                        _nomSessionResultRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedSessionResult)), successNomenclatureMessage);

                    case NomenclatureEnums.Specialty:
                        var dbMappedSpeciality = JsonConvert.DeserializeObject<NomSpecialty>(nomenclature.ToString());
                        _nomSpecialtyRepository.Add(dbMappedSpeciality);
                        _nomSpecialtyRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedSpeciality)), successNomenclatureMessage);

                    case NomenclatureEnums.TemplateKind:
                        var dbMappedTemplateKind = JsonConvert.DeserializeObject<NomTemplateKind>(nomenclature.ToString());
                        _nomTemplateKindRepository.Add(dbMappedTemplateKind);
                        _nomTemplateKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomTemplateKindDTO>(dbMappedTemplateKind)), successNomenclatureMessage);

                    case NomenclatureEnums.DebtorStatus:
                        var dbMappedDebtorStatus = JsonConvert.DeserializeObject<NomDebtorStatus>(nomenclature.ToString());
                        _nomDebtorStatusRepository.Add(dbMappedDebtorStatus);
                        _nomDebtorStatusRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomDebtorStatusDTO>(dbMappedDebtorStatus)), successNomenclatureMessage);

                    case NomenclatureEnums.LegalBasis:
                        var dbMappedLegalBasis = JsonConvert.DeserializeObject<NomLegalBasis>(nomenclature.ToString());
                        _nomLegalBasisRepository.Add(dbMappedLegalBasis);
                        _nomLegalBasisRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedLegalBasis)), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingLocationType:
                        var dbMappedOfferingLocationType = JsonConvert.DeserializeObject<NomOfferingLocationType>(nomenclature.ToString());
                        _nomOfferingLocationTypeRepository.Add(dbMappedOfferingLocationType);
                        _nomOfferingLocationTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedOfferingLocationType)), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingProcedure:
                        var dbMappedOfferingProcedure = JsonConvert.DeserializeObject<NomOfferingProcedure>(nomenclature.ToString());
                        _nomOfferingProcedureRepository.Add(dbMappedOfferingProcedure);
                        _nomOfferingProcedureRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedOfferingProcedure)), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingKind:
                        var dbMappedOfferingKind = JsonConvert.DeserializeObject<NomOfferingKind>(nomenclature.ToString());
                        _nomOfferingKindRepository.Add(dbMappedOfferingKind);
                        _nomOfferingKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedOfferingKind)), successNomenclatureMessage);

                    case NomenclatureEnums.PapersForSale:
                        var dbMappedPapersForSale = JsonConvert.DeserializeObject<NomPapersForSale>(nomenclature.ToString());
                        _nomPapersForSaleRepository.Add(dbMappedPapersForSale);
                        _nomPapersForSaleRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedPapersForSale)), successNomenclatureMessage);

                    case NomenclatureEnums.SalesProcedure:
                        var dbMappedSalesProcedure = JsonConvert.DeserializeObject<NomSalesProcedure>(nomenclature.ToString());
                        _nomSalesProcedureRepository.Add(dbMappedSalesProcedure);
                        _nomSalesProcedureRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedSalesProcedure)), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicStatus:
                        var dbMappedSyndicStatus = JsonConvert.DeserializeObject<NomSyndicStatus>(nomenclature.ToString());
                        _nomSyndicStatusRepository.Add(dbMappedSyndicStatus);
                        _nomSyndicStatusRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicStatusDTO>(dbMappedSyndicStatus)), successNomenclatureMessage);

                    case NomenclatureEnums.InstallmentYear:
                        var dbMappedInstallmentYear = JsonConvert.DeserializeObject<NomInstallmentYear>(nomenclature.ToString());
                        _nomInstallmentYearRepository.Add(dbMappedInstallmentYear);
                        _nomInstallmentYearRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomInstallmentYearDTO>(dbMappedInstallmentYear)), successNomenclatureMessage);

                    case NomenclatureEnums.SellAnnouncementTemplate:
                        var dbMappedTemplate = JsonConvert.DeserializeObject<NomSellAnnouncementTemplate>(nomenclature.ToString());
                        _nomAnnouncementTemplate.Add(dbMappedTemplate);
                        _nomAnnouncementTemplate.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<SaveSellAnnouncementTemplateDTO>(dbMappedTemplate)), successNomenclatureMessage);

                    case NomenclatureEnums.AnnouncementStatus:
                        var dbMappedAnnouncementStatus = JsonConvert.DeserializeObject<NomAnnouncementStatus>(nomenclature.ToString());
                        _nomAnnouncementStatusRepository.Add(dbMappedAnnouncementStatus);
                        _nomAnnouncementStatusRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedAnnouncementStatus)), successNomenclatureMessage);

                    case NomenclatureEnums.DirectiveTemplate:
                        var dbMappedDirectiveTemaplate = JsonConvert.DeserializeObject<NomDirectiveTemplateKind>(nomenclature.ToString());
                        _directiveTemplate.Add(dbMappedDirectiveTemaplate);
                        _directiveTemplate.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedDirectiveTemaplate)), successNomenclatureMessage);

                    case NomenclatureEnums.AppealKind:
                        var dbMappedAppealKind = JsonConvert.DeserializeObject<NomAppealKind>(nomenclature.ToString());
                        _nomAppealKindRepository.Add(dbMappedAppealKind);
                        _nomAppealKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedAppealKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SignalSenderType:
                        var dbMappedSignalSenderKind = JsonConvert.DeserializeObject<NomSignalSenderType>(nomenclature.ToString());
                        _nomSignalSenderTypeRepository.Add(dbMappedSignalSenderKind);
                        _nomSignalSenderTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomSignalSenderTypeDTO>(dbMappedSignalSenderKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SignalDocumentKind:
                        var dbMappedSignalDocumentKind = JsonConvert.DeserializeObject<NomSignalDocumentKind>(nomenclature.ToString());
                        _nomSignalDocumentKindRepository.Add(dbMappedSignalDocumentKind);
                        _nomSignalDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomSignalDocumentKindDTO>(dbMappedSignalDocumentKind)), successNomenclatureMessage);

                    case NomenclatureEnums.InspectionType:
                        var dbMappedInspectionType = JsonConvert.DeserializeObject<NomInspectionType>(nomenclature.ToString());
                        _nomInspectionTypeRepository.Add(dbMappedInspectionType);
                        _nomInspectionTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomInspectionTypeDTO>(dbMappedInspectionType)), successNomenclatureMessage);

                    case NomenclatureEnums.ObjectKind:
                        var dbMappedObjectKind = JsonConvert.DeserializeObject<NomObjectKind>(nomenclature.ToString());
                        _nomObjectKindRepository.Add(dbMappedObjectKind);
                        _nomObjectKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedObjectKind)), successNomenclatureMessage);

                    case NomenclatureEnums.ObjectType:
                        var dbMappedObjectType = JsonConvert.DeserializeObject<NomObjectType>(nomenclature.ToString());
                        _nomObjectTypeRepository.Add(dbMappedObjectType);
                        _nomObjectTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<AnnouncementNomenclatureDTO>(dbMappedObjectType)), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicCaseReport:
                        var dbMappedSyndicCaseReport = JsonConvert.DeserializeObject<NomSyndicCaseReport>(nomenclature.ToString());
                        _nomSyndicCaseReportRepository.Add(dbMappedSyndicCaseReport);
                        _nomSyndicCaseReportRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicCaseReportDTO>(dbMappedSyndicCaseReport)), successNomenclatureMessage);

                    case NomenclatureEnums.CourseKind:
                        var dbMappedCourseKind = JsonConvert.DeserializeObject<NomCourseKind>(nomenclature.ToString());
                        _nomCourseKindRepository.Add(dbMappedCourseKind);
                        _nomCourseKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomCourseKindDTO>(dbMappedCourseKind)), successNomenclatureMessage);

                    case NomenclatureEnums.OrderPaymentKind:
                        var dbMappedOrderPaymentKind = JsonConvert.DeserializeObject<NomOrderPaymentKind>(nomenclature.ToString());
                        _nomOrderPaymentKindRepository.Add(dbMappedOrderPaymentKind);
                        _nomOrderPaymentKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomOrderPaymentKindDTO>(dbMappedOrderPaymentKind)), successNomenclatureMessage);

                    //case NomenclatureEnums.SyndicAction:
                    //    var dbMappedSyndicAction = JsonConvert.DeserializeObject<NomSyndicAction>(nomenclature.ToString());
                    //    _nomSyndicActionRepository.Add(dbMappedSyndicAction);
                    //    _nomSyndicActionRepository.Save();
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomSyndicActionDTO>(dbMappedSyndicAction)), successNomenclatureMessage);

                    //case NomenclatureEnums.ActCategory:
                    //    var dbMappedActCategory = JsonConvert.DeserializeObject<NomActCategory>(nomenclature.ToString());
                    //    _nomActCategoryRepository.Add(dbMappedActCategory);
                    //    _nomActCategoryRepository.Save();
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomActCategoryDTO>(dbMappedActCategory)), successNomenclatureMessage);

                    //case NomenclatureEnums.ActContractor:
                    //    var dbMappedActContractor = JsonConvert.DeserializeObject<NomActContractor>(nomenclature.ToString());
                    //    _nomActContractorRepository.Add(dbMappedActContractor);
                    //    _nomActContractorRepository.Save();
                    //    return Success(_mapper.Map<object>(_mapper.Map<NomActContractorDTO>(dbMappedActContractor)), successNomenclatureMessage);

                    case NomenclatureEnums.ActivityKind:
                        var dbMappedActivityKind = JsonConvert.DeserializeObject<NomActivityKind>(nomenclature.ToString());
                        _nomActivityKindRepository.Add(dbMappedActivityKind);
                        _nomActivityKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedActivityKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SampleKind:
                        var dbMappedSampleKind = JsonConvert.DeserializeObject<NomSampleKind>(nomenclature.ToString());
                        _nomSampleKindRepository.Add(dbMappedSampleKind);
                        _nomSampleKindRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedSampleKind)), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicReportType:
                        var dbMappedSyndicReportType = JsonConvert.DeserializeObject<NomSyndicReportType>(nomenclature.ToString());
                        _nomSyndicReportTypeRepository.Add(dbMappedSyndicReportType);
                        _nomSyndicReportTypeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomenclatureDTO>(dbMappedSyndicReportType)), successNomenclatureMessage);

                    case NomenclatureEnums.CompanySize:
                        var dbMappedCompanySize = JsonConvert.DeserializeObject<NomCompanySize>(nomenclature.ToString());
                        _nomCompanySizeRepository.Add(dbMappedCompanySize);
                        _nomCompanySizeRepository.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomCompanySizeDTO>(dbMappedCompanySize)), successNomenclatureMessage);

                    case NomenclatureEnums.ActAnnouncementCategory:
                        var dbMappedActAnnouncementCategory = JsonConvert.DeserializeObject<NomActAnnouncementCategory>(nomenclature.ToString());
                        _nomActAnnouncementCategoryRepo.Add(dbMappedActAnnouncementCategory);
                        _nomActAnnouncementCategoryRepo.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomActAnnouncementCategoryDTO>(dbMappedActAnnouncementCategory)), successNomenclatureMessage);

                    case NomenclatureEnums.RegistrationLegalBasis:
                        var dbMappedRegistrationLegalBasis = JsonConvert.DeserializeObject<NomRegistrationLegalBasis>(nomenclature.ToString());
                        _nomRegistrationLegalBasis.Add(dbMappedRegistrationLegalBasis);
                        _nomRegistrationLegalBasis.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegistrationLegalBasisDTO>(dbMappedRegistrationLegalBasis)), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicKind:
                        var dbMappedSyndicKind = JsonConvert.DeserializeObject<NomSyndicKind>(nomenclature.ToString());
                        _nomSyndicKind.Add(dbMappedSyndicKind);
                        _nomSyndicKind.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomSyndicKindDTO>(dbMappedSyndicKind)), successNomenclatureMessage);

                    case NomenclatureEnums.RegisterField:
                        var dbMappedRegisterField = JsonConvert.DeserializeObject<NomRegisterField>(nomenclature.ToString());
                        _nomRegisterField.Add(dbMappedRegisterField);
                        _nomRegisterField.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegisterFieldDTO>(dbMappedRegisterField)), successNomenclatureMessage);

                    case NomenclatureEnums.RegisterSyndicKind:
                        var dbMappedRegisterSyndicKind = JsonConvert.DeserializeObject<NomRegisterSyndicKind>(nomenclature.ToString());
                        _nomRegisterSyndicKind.Add(dbMappedRegisterSyndicKind);
                        _nomRegisterSyndicKind.Save();
                        return Success(_mapper.Map<object>(_mapper.Map<NomRegisterSyndicKindDTO>(dbMappedRegisterSyndicKind)), successNomenclatureMessage);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<object>(ex);
            }
        }

        public OperationResult<object> UpdateNomenclature(object nomenclature, NomenclatureEnums type)
        {
            try
            {
                string successNomenclatureMessage = $"Номенклатурата е редактирана.";

                switch (type)
                {
                    case NomenclatureEnums.Action:
                        var action = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbAction = _nomActionRepository.GetById(action.Id.Value);
                        var dbMappedAction = _mapper.Map(action, dbAction);
                        _nomActionRepository.Update(dbMappedAction);
                        _nomActionRepository.Save();
                        return Success(_mapper.Map<object>(action), successNomenclatureMessage);

                    case NomenclatureEnums.ActKind:
                        var actKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbActKind = _nomActKindRepository.GetById(actKind.Id.Value);
                        var dbMappedActKind = _mapper.Map(actKind, dbActKind);
                        _nomActKindRepository.Update(dbMappedActKind);
                        _nomActKindRepository.Save();
                        return Success(_mapper.Map<object>(actKind), successNomenclatureMessage);

                    case NomenclatureEnums.ActReason:
                        var actReason = JsonConvert.DeserializeObject<NomActReason>(nomenclature.ToString());
                        var dbActReason = _nomActReasonRepository.GetById(actReason.Id);
                        var dbMappedActReason = _mapper.Map(actReason, dbActReason);
                        _nomActReasonRepository.Update(dbMappedActReason);
                        _nomActReasonRepository.Save();
                        return Success(_mapper.Map<object>(actReason), successNomenclatureMessage);

                    case NomenclatureEnums.AttachedDocumentKind:
                        var attachedDocumentKind = JsonConvert.DeserializeObject<NomAttachedDocumentKindDTO>(nomenclature.ToString());
                        var dbAttachedDocumentKind = _nomAttachedDocumentKindRepository.GetById(attachedDocumentKind.Id);
                        var dbMappedAttachedDocumentKind = _mapper.Map(attachedDocumentKind, dbAttachedDocumentKind);
                        _nomAttachedDocumentKindRepository.Update(dbMappedAttachedDocumentKind);
                        _nomAttachedDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(attachedDocumentKind), successNomenclatureMessage);

                    case NomenclatureEnums.CaseCode:
                        var caseCode = JsonConvert.DeserializeObject<NomCaseCodeDTO>(nomenclature.ToString());
                        var dbCaseCode = _nomCaseCodeRepository.GetById(caseCode.Id.Value);
                        var dbMappedCaseCode = _mapper.Map(caseCode, dbCaseCode);
                        _nomCaseCodeRepository.Update(dbMappedCaseCode);
                        _nomCaseCodeRepository.Save();
                        return Success(_mapper.Map<object>(caseCode), successNomenclatureMessage);

                    case NomenclatureEnums.CaseKind:
                        var caseKind = JsonConvert.DeserializeObject<NomCaseKind>(nomenclature.ToString());
                        var dbCaseKind = _nomCaseKindRepository.GetById(caseKind.Id);
                        var dbMappedCaseKind = _mapper.Map(caseKind, dbCaseKind);
                        _nomCaseKindRepository.Update(dbMappedCaseKind);
                        _nomCaseKindRepository.Save();
                        return Success(_mapper.Map<object>(caseKind), successNomenclatureMessage);

                    case NomenclatureEnums.CourtNumber:
                        var court = JsonConvert.DeserializeObject<NomCourtDTO>(nomenclature.ToString());
                        var dbCourt = _nomCourtRepository.GetById(court.Id.Value);
                        var dbMappedCourt = _mapper.Map(court, dbCourt);
                        _nomCourtRepository.Update(dbMappedCourt);
                        _nomCourtRepository.Save();
                        return Success(_mapper.Map<object>(court), successNomenclatureMessage);

                    case NomenclatureEnums.IncomingDocumentKind:
                        var incomingDocumentKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbIncomingDocumentKind = _nomIncomingDocumentKindRepository.GetById(incomingDocumentKind.Id.Value);
                        var dbMappedIncomingDocumentKind = _mapper.Map(incomingDocumentKind, dbIncomingDocumentKind);
                        _nomIncomingDocumentKindRepository.Update(dbMappedIncomingDocumentKind);
                        _nomIncomingDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(incomingDocumentKind), successNomenclatureMessage);

                    case NomenclatureEnums.InvolvementKind:
                        var involvementKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbInvolvementKind = _nomInvolvementKindRepository.GetById(involvementKind.Id.Value);
                        var dbMappedInvolvementKind = _mapper.Map(involvementKind, dbInvolvementKind);
                        _nomInvolvementKindRepository.Update(dbMappedInvolvementKind);
                        _nomInvolvementKindRepository.Save();
                        return Success(_mapper.Map<object>(involvementKind), successNomenclatureMessage);

                    case NomenclatureEnums.OrderKind:
                        var orderKind = JsonConvert.DeserializeObject<NomOrderKindDTO>(nomenclature.ToString());
                        var dbOrderKind = _nomOrderKindRepository.GetById(orderKind.Id);
                        var dbMappedOrderKind = _mapper.Map(orderKind, dbOrderKind);
                        _nomOrderKindRepository.Update(dbMappedOrderKind);
                        _nomOrderKindRepository.Save();
                        return Success(_mapper.Map<object>(orderKind), successNomenclatureMessage);

                    case NomenclatureEnums.ReportSource:
                        var reportSource = JsonConvert.DeserializeObject<NomReportSourceDTO>(nomenclature.ToString());
                        var dbReportSource = _nomReportSourceRepository.GetById(reportSource.Id);
                        var dbMappedReportSource = _mapper.Map(reportSource, dbReportSource);
                        _nomReportSourceRepository.Update(dbMappedReportSource);
                        _nomReportSourceRepository.Save();
                        return Success(_mapper.Map<object>(reportSource), successNomenclatureMessage);

                    case NomenclatureEnums.ReportType:
                        var reportType = JsonConvert.DeserializeObject<NomReportTypeDTO>(nomenclature.ToString());
                        var dbReportType = _nomReportTypeRepository.GetById(reportType.Id);
                        var dbMappedReportType = _mapper.Map(reportType, dbReportType);
                        _nomReportTypeRepository.Update(dbMappedReportType);
                        _nomReportTypeRepository.Save();
                        return Success(_mapper.Map<object>(reportType), successNomenclatureMessage);

                    case NomenclatureEnums.SendToKind:
                        var sendToKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbSendToKind = _nomSendToKindRepository.GetById(sendToKind.Id.Value);
                        var dbMappedSendToKind = _mapper.Map(sendToKind, dbSendToKind);
                        _nomSendToKindRepository.Update(dbMappedSendToKind);
                        _nomSendToKindRepository.Save();
                        return Success(_mapper.Map<object>(sendToKind), successNomenclatureMessage);

                    case NomenclatureEnums.SessionKind:
                        var sessionKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbSessionKind = _nomSessionKindRepository.GetById(sessionKind.Id.Value);
                        var dbMappedSessionKind = _mapper.Map(sessionKind, dbSessionKind);
                        _nomSessionKindRepository.Update(dbMappedSessionKind);
                        _nomSessionKindRepository.Save();
                        return Success(_mapper.Map<object>(sessionKind), successNomenclatureMessage);

                    case NomenclatureEnums.SessionResult:
                        var sessionResult = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbSessionResult = _nomSessionResultRepository.GetById(sessionResult.Id.Value);
                        var dbMappedSessionResult = _mapper.Map(sessionResult, dbSessionResult);
                        _nomSessionResultRepository.Update(dbMappedSessionResult);
                        _nomSessionResultRepository.Save();
                        return Success(_mapper.Map<object>(sessionResult), successNomenclatureMessage);

                    case NomenclatureEnums.Specialty:
                        var speciality = JsonConvert.DeserializeObject<NomSpecialityDTO>(nomenclature.ToString());
                        var dbSpeciality = _nomSpecialtyRepository.GetById(speciality.Id);
                        var dbMappedSpeciality = _mapper.Map(speciality, dbSpeciality);
                        _nomSpecialtyRepository.Update(dbMappedSpeciality);
                        _nomSpecialtyRepository.Save();
                        return Success(_mapper.Map<object>(speciality), successNomenclatureMessage);

                    case NomenclatureEnums.TemplateKind:
                        var templateKind = JsonConvert.DeserializeObject<NomTemplateKindDTO>(nomenclature.ToString());
                        var dbTemplateKind = _nomTemplateKindRepository.GetById(templateKind.Id);
                        var dbMappedTemplateKind = _mapper.Map(templateKind, dbTemplateKind);
                        _nomTemplateKindRepository.Update(dbMappedTemplateKind);
                        _nomTemplateKindRepository.Save();
                        return Success(_mapper.Map<object>(templateKind), successNomenclatureMessage);

                    case NomenclatureEnums.DebtorStatus:
                        var debtorStatus = JsonConvert.DeserializeObject<NomDebtorStatusDTO>(nomenclature.ToString());
                        var dbDebtorStatus = _nomDebtorStatusRepository.GetById(debtorStatus.Id);
                        var dbMappedDebtorStatus = _mapper.Map(debtorStatus, dbDebtorStatus);
                        _nomDebtorStatusRepository.Update(dbMappedDebtorStatus);
                        _nomDebtorStatusRepository.Save();
                        return Success(_mapper.Map<object>(debtorStatus), successNomenclatureMessage);

                    case NomenclatureEnums.LegalBasis:
                        var legalBasis = JsonConvert.DeserializeObject<NomLegalBasisDTO>(nomenclature.ToString());
                        var dbLegalBasis = _nomLegalBasisRepository.GetById(legalBasis.Id);
                        var dbMappedLegalBasis = _mapper.Map(legalBasis, dbLegalBasis);
                        _nomLegalBasisRepository.Update(dbMappedLegalBasis);
                        _nomLegalBasisRepository.Save();
                        return Success(_mapper.Map<object>(legalBasis), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingLocationType:
                        var offeringLocationType = JsonConvert.DeserializeObject<AnnouncementNomenclatureDTO>(nomenclature.ToString());
                        var dbOfferingLocationType = _nomOfferingLocationTypeRepository.GetById(offeringLocationType.Id.Value);
                        var dbMappedOfferingLocationType = _mapper.Map(offeringLocationType, dbOfferingLocationType);
                        _nomOfferingLocationTypeRepository.Update(dbMappedOfferingLocationType);
                        _nomOfferingLocationTypeRepository.Save();
                        return Success(_mapper.Map<object>(offeringLocationType), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingProcedure:
                        var offeringProcedure = JsonConvert.DeserializeObject<AnnouncementNomenclatureDTO>(nomenclature.ToString());
                        var dbOfferingProcedure = _nomOfferingProcedureRepository.GetById(offeringProcedure.Id.Value);
                        var dbMappedOfferingProcedure = _mapper.Map(offeringProcedure, dbOfferingProcedure);
                        _nomOfferingProcedureRepository.Update(dbMappedOfferingProcedure);
                        _nomOfferingProcedureRepository.Save();
                        return Success(_mapper.Map<object>(offeringProcedure), successNomenclatureMessage);

                    case NomenclatureEnums.OfferingKind:
                        var offeringKind = JsonConvert.DeserializeObject<AnnouncementNomenclatureDTO>(nomenclature.ToString());
                        var dbOfferingKind = _nomOfferingKindRepository.GetById(offeringKind.Id.Value);
                        var dbMappedOfferingKind = _mapper.Map(offeringKind, dbOfferingKind);
                        _nomOfferingKindRepository.Update(dbMappedOfferingKind);
                        _nomOfferingKindRepository.Save();
                        return Success(_mapper.Map<object>(offeringKind), successNomenclatureMessage);

                    case NomenclatureEnums.PapersForSale:
                        var papersForSale = JsonConvert.DeserializeObject<AnnouncementNomenclatureDTO>(nomenclature.ToString());
                        var dbPapersForSale = _nomPapersForSaleRepository.GetById(papersForSale.Id.Value);
                        var dbMappedPapersForSale = _mapper.Map(papersForSale, dbPapersForSale);
                        _nomPapersForSaleRepository.Update(dbMappedPapersForSale);
                        _nomPapersForSaleRepository.Save();
                        return Success(_mapper.Map<object>(papersForSale), successNomenclatureMessage);

                    case NomenclatureEnums.SalesProcedure:
                        var salesProcedure = JsonConvert.DeserializeObject<AnnouncementNomenclatureDTO>(nomenclature.ToString());
                        var dbSalesProcedure = _nomSalesProcedureRepository.GetById(salesProcedure.Id.Value);
                        var dbMappedSalesProcedure = _mapper.Map(salesProcedure, dbSalesProcedure);
                        _nomSalesProcedureRepository.Update(dbMappedSalesProcedure);
                        _nomSalesProcedureRepository.Save();
                        return Success(_mapper.Map<object>(salesProcedure), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicStatus:
                        var syndicStatus = JsonConvert.DeserializeObject<NomSyndicStatusDTO>(nomenclature.ToString());
                        var dbSyndicStatus = _nomSyndicStatusRepository.GetById(syndicStatus.Id);
                        var dbMappedSyndicStatus = _mapper.Map(syndicStatus, dbSyndicStatus);
                        _nomSyndicStatusRepository.Update(dbMappedSyndicStatus);
                        _nomSyndicStatusRepository.Save();
                        return Success(_mapper.Map<object>(syndicStatus), successNomenclatureMessage);

                    case NomenclatureEnums.SellAnnouncementTemplate:
                        var sellAnnouncementTemplate = JsonConvert.DeserializeObject<SaveSellAnnouncementTemplateDTO>(nomenclature.ToString());
                        var dbSellAnnouncementTemplate = _nomAnnouncementTemplate.GetById(sellAnnouncementTemplate.Id.Value);
                        var dbMAppedSellAnnouncementTemplate = _mapper.Map(sellAnnouncementTemplate, dbSellAnnouncementTemplate);
                        _nomAnnouncementTemplate.Update(dbSellAnnouncementTemplate);
                        _nomAnnouncementTemplate.Save();
                        return Success(_mapper.Map<object>(dbMAppedSellAnnouncementTemplate), successNomenclatureMessage);

                    case NomenclatureEnums.InstallmentYear:
                        var installmentYear = JsonConvert.DeserializeObject<NomInstallmentYearDTO>(nomenclature.ToString());
                        var dbInstallmentYear = _nomInstallmentYearRepository.GetById(installmentYear.Id.Value);
                        var dbMappedInstallmentYear = _mapper.Map(installmentYear, dbInstallmentYear);
                        _nomInstallmentYearRepository.Update(dbMappedInstallmentYear);
                        _nomInstallmentYearRepository.Save();
                        return Success(_mapper.Map<object>(installmentYear), successNomenclatureMessage);

                    case NomenclatureEnums.AnnouncementStatus:
                        var announcementStatus = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbAnnouncementStatus = _nomAnnouncementStatusRepository.GetById(announcementStatus.Id.Value);
                        var dbMappedAnnouncementStatus = _mapper.Map(announcementStatus, dbAnnouncementStatus);
                        _nomAnnouncementStatusRepository.Update(dbMappedAnnouncementStatus);
                        _nomAnnouncementStatusRepository.Save();
                        return Success(_mapper.Map<object>(announcementStatus), successNomenclatureMessage);

                    case NomenclatureEnums.DirectiveTemplate:
                        var directiveTemplate = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbDirectiveTemplate = _directiveTemplate.GetById(directiveTemplate.Id.Value);
                        var dbMappedDirectiveTemplate = _mapper.Map(directiveTemplate, dbDirectiveTemplate);
                        _directiveTemplate.Update(dbMappedDirectiveTemplate);
                        _directiveTemplate.Save();
                        return Success(_mapper.Map<object>(directiveTemplate), successNomenclatureMessage);

                    case NomenclatureEnums.AppealKind:
                        var appealKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbAppealKind = _nomAppealKindRepository.GetById(appealKind.Id.Value);
                        var dbMappedAppealKind = _mapper.Map(appealKind, dbAppealKind);
                        _nomAppealKindRepository.Update(dbMappedAppealKind);
                        _nomAppealKindRepository.Save();
                        return Success(_mapper.Map<object>(appealKind), successNomenclatureMessage);

                    case NomenclatureEnums.SignalSenderType:
                        var signalSenderType = JsonConvert.DeserializeObject<NomSignalSenderTypeDTO>(nomenclature.ToString());
                        var dbSignalSenderType = _nomSignalSenderTypeRepository.GetById(signalSenderType.Id.Value);
                        var dbMappedSignalSenderType = _mapper.Map(signalSenderType, dbSignalSenderType);
                        _nomSignalSenderTypeRepository.Update(dbMappedSignalSenderType);
                        _nomSignalSenderTypeRepository.Save();
                        return Success(_mapper.Map<object>(signalSenderType), successNomenclatureMessage);

                    case NomenclatureEnums.SignalDocumentKind:
                        var signalDocumentKind = JsonConvert.DeserializeObject<NomSignalDocumentKindDTO>(nomenclature.ToString());
                        var dbSignalDocumentKind = _nomSignalDocumentKindRepository.GetById(signalDocumentKind.Id.Value);
                        var dbMappedSignalDocumentKind = _mapper.Map(signalDocumentKind, dbSignalDocumentKind);
                        _nomSignalDocumentKindRepository.Update(dbMappedSignalDocumentKind);
                        _nomSignalDocumentKindRepository.Save();
                        return Success(_mapper.Map<object>(signalDocumentKind), successNomenclatureMessage);

                    case NomenclatureEnums.InspectionType:
                        var inspectionType = JsonConvert.DeserializeObject<NomInspectionTypeDTO>(nomenclature.ToString());
                        var dbInspectionType = _nomInspectionTypeRepository.GetById(inspectionType.Id.Value);
                        var dbMappedInspectionType = _mapper.Map(inspectionType, dbInspectionType);
                        _nomInspectionTypeRepository.Update(dbMappedInspectionType);
                        _nomInspectionTypeRepository.Save();
                        return Success(_mapper.Map<object>(inspectionType), successNomenclatureMessage);

                    case NomenclatureEnums.ObjectKind:
                        var objectKind = JsonConvert.DeserializeObject<NomObjectKindDTO>(nomenclature.ToString());
                        var dbObjectKind = _nomObjectKindRepository.GetById(objectKind.Id.Value);
                        var dbMappedObjectKind = _mapper.Map(objectKind, dbObjectKind);
                        _nomObjectKindRepository.Update(dbMappedObjectKind);
                        _nomObjectKindRepository.Save();
                        return Success(_mapper.Map<object>(objectKind), successNomenclatureMessage);

                    case NomenclatureEnums.ObjectType:
                        var objectType = JsonConvert.DeserializeObject<NomObjectTypeDTO>(nomenclature.ToString());
                        var dbObjectType = _nomObjectTypeRepository.GetById(objectType.Id.Value);
                        var dbMappedObjectType = _mapper.Map(objectType, dbObjectType);
                        _nomObjectTypeRepository.Update(dbMappedObjectType);
                        _nomObjectTypeRepository.Save();
                        return Success(_mapper.Map<object>(objectType), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicCaseReport:
                        var syndicCaseReport = JsonConvert.DeserializeObject<NomSyndicCaseReportDTO>(nomenclature.ToString());
                        var dbSyndicCaseReport = _nomSyndicCaseReportRepository.GetById(syndicCaseReport.Id.Value);
                        var dbMappedSyndicCaseReport = _mapper.Map(syndicCaseReport, dbSyndicCaseReport);
                        _nomSyndicCaseReportRepository.Update(dbMappedSyndicCaseReport);
                        _nomSyndicCaseReportRepository.Save();
                        return Success(_mapper.Map<object>(syndicCaseReport), successNomenclatureMessage);

                    case NomenclatureEnums.CourseKind:
                        var courseKind = JsonConvert.DeserializeObject<NomCourseKindDTO>(nomenclature.ToString());
                        var dbCourseKind = _nomCourseKindRepository.GetById(courseKind.Id.Value);
                        var dbMappedCourseKind = _mapper.Map(courseKind, dbCourseKind);
                        _nomCourseKindRepository.Update(dbMappedCourseKind);
                        _nomCourseKindRepository.Save();
                        return Success(_mapper.Map<object>(courseKind), successNomenclatureMessage);

                    case NomenclatureEnums.OrderPaymentKind:
                        var orderPaymentKind = JsonConvert.DeserializeObject<NomOrderPaymentKindDTO>(nomenclature.ToString());
                        var dbOrderPaymentKind = _nomOrderPaymentKindRepository.GetById(orderPaymentKind.Id.Value);
                        var dbMappedOrderPaymentKind = _mapper.Map(orderPaymentKind, dbOrderPaymentKind);
                        _nomOrderPaymentKindRepository.Update(dbMappedOrderPaymentKind);
                        _nomOrderPaymentKindRepository.Save();
                        return Success(_mapper.Map<object>(orderPaymentKind), successNomenclatureMessage);

                    //case NomenclatureEnums.SyndicAction:
                    //    var syndicAction = JsonConvert.DeserializeObject<NomSyndicActionDTO>(nomenclature.ToString());
                    //    var dbSyndicAction = _nomSyndicActionRepository.GetById(syndicAction.Id.Value);
                    //    var dbMappedSyndicAction = _mapper.Map(syndicAction, dbSyndicAction);
                    //    _nomSyndicActionRepository.Update(dbMappedSyndicAction);
                    //    _nomSyndicActionRepository.Save();
                    //    return Success(_mapper.Map<object>(syndicAction), successNomenclatureMessage);

                    //case NomenclatureEnums.ActCategory:
                    //    var actCategory = JsonConvert.DeserializeObject<NomActCategoryDTO>(nomenclature.ToString());
                    //    var dbActCategory = _nomActCategoryRepository.GetById(actCategory.Id.Value);
                    //    var dbMappedActCategory = _mapper.Map(actCategory, dbActCategory);
                    //    _nomActCategoryRepository.Update(dbMappedActCategory);
                    //    _nomActCategoryRepository.Save();
                    //    return Success(_mapper.Map<object>(actCategory), successNomenclatureMessage);

                    //case NomenclatureEnums.ActContractor:
                    //    var actContractor = JsonConvert.DeserializeObject<NomActContractorDTO>(nomenclature.ToString());
                    //    var dbActContractor = _nomActContractorRepository.GetById(actContractor.Id.Value);
                    //    var dbMappedActContractor = _mapper.Map(actContractor, dbActContractor);
                    //    _nomActContractorRepository.Update(dbMappedActContractor);
                    //    _nomActContractorRepository.Save();
                    //    return Success(_mapper.Map<object>(actContractor), successNomenclatureMessage);

                    case NomenclatureEnums.ActivityKind:
                        var activityKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbActivityKind = _nomActivityKindRepository.GetById(activityKind.Id.Value);
                        var dbMappedActivityKind = _mapper.Map(activityKind, dbActivityKind);
                        _nomActivityKindRepository.Update(dbMappedActivityKind);
                        _nomActivityKindRepository.Save();
                        return Success(_mapper.Map<object>(activityKind), successNomenclatureMessage);

                    case NomenclatureEnums.SampleKind:
                        var sampleKind = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbSampleKind = _nomSampleKindRepository.GetById(sampleKind.Id.Value);
                        var dbMappedSampleKind = _mapper.Map(sampleKind, dbSampleKind);
                        _nomSampleKindRepository.Update(dbMappedSampleKind);
                        _nomSampleKindRepository.Save();
                        return Success(_mapper.Map<object>(sampleKind), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicReportType:
                        var syndicReportType = JsonConvert.DeserializeObject<NomenclatureDTO>(nomenclature.ToString());
                        var dbSyndicReportType = _nomSyndicReportTypeRepository.GetById(syndicReportType.Id.Value);
                        var dbMappedSyndicReportType = _mapper.Map(syndicReportType, dbSyndicReportType);
                        _nomSyndicReportTypeRepository.Update(dbMappedSyndicReportType);
                        _nomSyndicReportTypeRepository.Save();
                        return Success(_mapper.Map<object>(syndicReportType), successNomenclatureMessage);

                    case NomenclatureEnums.CompanySize:
                        var companySize = JsonConvert.DeserializeObject<NomCompanySizeDTO>(nomenclature.ToString());
                        var dbCompanySize = _nomCompanySizeRepository.GetById(companySize.Id.Value);
                        var dbMappedCompanySize = _mapper.Map(companySize, dbCompanySize);
                        _nomCompanySizeRepository.Update(dbMappedCompanySize);
                        _nomCompanySizeRepository.Save();
                        return Success(_mapper.Map<object>(companySize), successNomenclatureMessage);

                    case NomenclatureEnums.ActAnnouncementCategory:
                        var actAnnouncementCategory = JsonConvert.DeserializeObject<NomActAnnouncementCategoryDTO>(nomenclature.ToString());
                        var dbActAnnouncementCategory = _nomActAnnouncementCategoryRepo.GetById(actAnnouncementCategory.Id.Value);
                        var dbMappedActAnnouncementCategory = _mapper.Map(actAnnouncementCategory, dbActAnnouncementCategory);
                        _nomActAnnouncementCategoryRepo.Update(dbMappedActAnnouncementCategory);
                        _nomActAnnouncementCategoryRepo.Save();
                        return Success(_mapper.Map<object>(actAnnouncementCategory), successNomenclatureMessage);

                    case NomenclatureEnums.RegistrationLegalBasis:
                        var registrationLegalBasis = JsonConvert.DeserializeObject<NomRegistrationLegalBasisDTO>(nomenclature.ToString());
                        var dbRegistrationLegalBasis = _nomRegistrationLegalBasis.GetById(registrationLegalBasis.Id.Value);
                        var dbMappedRegistrationLegalBasis = _mapper.Map(registrationLegalBasis, dbRegistrationLegalBasis);
                        _nomRegistrationLegalBasis.Update(dbMappedRegistrationLegalBasis);
                        _nomRegistrationLegalBasis.Save();
                        return Success(_mapper.Map<object>(registrationLegalBasis), successNomenclatureMessage);

                    case NomenclatureEnums.SyndicKind:
                        var syndicKind = JsonConvert.DeserializeObject<NomSyndicKindDTO>(nomenclature.ToString());
                        var dbSyndicKind = _nomSyndicKind.GetById(syndicKind.Id.Value);
                        var dbMappedSyndicKind = _mapper.Map(syndicKind, dbSyndicKind);
                        _nomSyndicKind.Update(dbMappedSyndicKind);
                        _nomSyndicKind.Save();
                        return Success(_mapper.Map<object>(syndicKind), successNomenclatureMessage);

                    case NomenclatureEnums.RegisterField:
                        var registerField = JsonConvert.DeserializeObject<NomRegisterFieldDTO>(nomenclature.ToString());
                        var dbRegisterField = _nomRegisterField.GetById(registerField.Id.Value);
                        var dbMappedRegisterField = _mapper.Map(registerField, dbRegisterField);
                        _nomRegisterField.Update(dbMappedRegisterField);
                        _nomRegisterField.Save();
                        return Success(_mapper.Map<object>(registerField), successNomenclatureMessage);

                    case NomenclatureEnums.RegisterSyndicKind:
                        var registerSyndicKind = JsonConvert.DeserializeObject<NomRegisterSyndicKindDTO>(nomenclature.ToString());
                        var dbRegisterSyndicKind = _nomRegisterSyndicKind.GetById(registerSyndicKind.Id.Value);
                        var dbMappedRegisterSyndicKind = _mapper.Map(registerSyndicKind, dbRegisterSyndicKind);
                        _nomRegisterSyndicKind.Update(dbMappedRegisterSyndicKind);
                        _nomRegisterSyndicKind.Save();
                        return Success(_mapper.Map<object>(registerSyndicKind), successNomenclatureMessage);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<object>(ex);
            }
        }

        public OperationResult<bool> DeleteNomenclature(Guid id, NomenclatureEnums type)
        {
            try
            {
                switch (type)
                {
                    case NomenclatureEnums.Action:
                        _nomActionRepository.Remove(id);
                        _nomActionRepository.Save();
                        break;
                    case NomenclatureEnums.ActKind:
                        _nomActKindRepository.Remove(id);
                        _nomActKindRepository.Save();
                        break;
                    case NomenclatureEnums.ActReason:
                        _nomActReasonRepository.Remove(id);
                        _nomActReasonRepository.Save();
                        break;
                    case NomenclatureEnums.AttachedDocumentKind:
                        _nomAttachedDocumentKindRepository.Remove(id);
                        _nomAttachedDocumentKindRepository.Save();
                        break;
                    case NomenclatureEnums.CaseCode:
                        _nomCaseCodeRepository.Remove(id);
                        _nomCaseCodeRepository.Save();
                        break;
                    case NomenclatureEnums.CaseKind:
                        _nomCaseKindRepository.Remove(id);
                        _nomCaseKindRepository.Save();
                        break;
                    case NomenclatureEnums.CourtNumber:
                        _nomCourtRepository.Remove(id);
                        _nomCourtRepository.Save();
                        break;
                    case NomenclatureEnums.IncomingDocumentKind:
                        _nomIncomingDocumentKindRepository.Remove(id);
                        _nomIncomingDocumentKindRepository.Save();
                        break;
                    case NomenclatureEnums.InvolvementKind:
                        _nomInvolvementKindRepository.Remove(id);
                        _nomInvolvementKindRepository.Save();
                        break;
                    case NomenclatureEnums.OrderKind:
                        _nomOrderKindRepository.Remove(id);
                        _nomOrderKindRepository.Save();
                        break;
                    case NomenclatureEnums.ReportSource:
                        _nomReportSourceRepository.Remove(id);
                        _nomReportSourceRepository.Save();
                        break;
                    case NomenclatureEnums.ReportType:
                        _nomReportTypeRepository.Remove(id);
                        _nomReportTypeRepository.Save();
                        break;
                    case NomenclatureEnums.SendToKind:
                        _nomSendToKindRepository.Remove(id);
                        _nomSendToKindRepository.Save();
                        break;
                    case NomenclatureEnums.SessionKind:
                        _nomSessionKindRepository.Remove(id);
                        _nomSessionKindRepository.Save();
                        break;
                    case NomenclatureEnums.SessionResult:
                        _nomSessionResultRepository.Remove(id);
                        _nomSessionResultRepository.Save();
                        break;
                    case NomenclatureEnums.Specialty:
                        _nomSpecialtyRepository.Remove(id);
                        _nomSpecialtyRepository.Save();
                        break;
                    case NomenclatureEnums.TemplateKind:
                        _nomTemplateKindRepository.Remove(id);
                        _nomTemplateKindRepository.Save();
                        break;
                    case NomenclatureEnums.DebtorStatus:
                        _nomDebtorStatusRepository.Remove(id);
                        _nomDebtorStatusRepository.Save();
                        break;
                    case NomenclatureEnums.LegalBasis:
                        _nomLegalBasisRepository.Remove(id);
                        _nomLegalBasisRepository.Save();
                        break;
                    case NomenclatureEnums.OfferingLocationType:
                        _nomOfferingLocationTypeRepository.Remove(id);
                        _nomOfferingLocationTypeRepository.Save();
                        break;
                    case NomenclatureEnums.OfferingProcedure:
                        _nomOfferingProcedureRepository.Remove(id);
                        _nomOfferingProcedureRepository.Save();
                        break;
                    case NomenclatureEnums.OfferingKind:
                        _nomOfferingKindRepository.Remove(id);
                        _nomOfferingKindRepository.Save();
                        break;
                    case NomenclatureEnums.PapersForSale:
                        _nomPapersForSaleRepository.Remove(id);
                        _nomPapersForSaleRepository.Save();
                        break;
                    case NomenclatureEnums.SalesProcedure:
                        _nomSalesProcedureRepository.Remove(id);
                        _nomSalesProcedureRepository.Save();
                        break;
                    case NomenclatureEnums.SyndicStatus:
                        _nomSyndicStatusRepository.Remove(id);
                        _nomSyndicStatusRepository.Save();
                        break;
                    case NomenclatureEnums.SellAnnouncementTemplate:
                        _nomAnnouncementTemplate.Remove(id);
                        _nomAnnouncementTemplate.Save();
                        break;
                    case NomenclatureEnums.InstallmentYear:
                        _nomInstallmentYearRepository.Remove(id);
                        _nomInstallmentYearRepository.Save();
                        break;
                    case NomenclatureEnums.AnnouncementStatus:
                        _nomAnnouncementStatusRepository.Remove(id);
                        _nomAnnouncementStatusRepository.Save();
                        break;
                    case NomenclatureEnums.DirectiveTemplate:
                        _directiveTemplate.Remove(id);
                        _directiveTemplate.Save();
                        break;
                    case NomenclatureEnums.AppealKind:
                        _nomAppealKindRepository.Remove(id);
                        _nomAppealKindRepository.Save();
                        break;
                    case NomenclatureEnums.SignalSenderType:
                        _nomSignalSenderTypeRepository.Remove(id);
                        _nomSignalSenderTypeRepository.Save();
                        break;
                    case NomenclatureEnums.SignalDocumentKind:
                        _nomSignalDocumentKindRepository.Remove(id);
                        _nomSignalDocumentKindRepository.Save();
                        break;
                    case NomenclatureEnums.InspectionType:
                        _nomInspectionTypeRepository.Remove(id);
                        _nomInspectionTypeRepository.Save();
                        break;
                    case NomenclatureEnums.ObjectKind:
                        _nomObjectKindRepository.Remove(id);
                        _nomObjectKindRepository.Save();
                        break;
                    case NomenclatureEnums.ObjectType:
                        _nomObjectTypeRepository.Remove(id);
                        _nomObjectTypeRepository.Save();
                        break;
                    case NomenclatureEnums.SyndicCaseReport:
                        _nomSyndicCaseReportRepository.Remove(id);
                        _nomSyndicCaseReportRepository.Save();
                        break;
                    case NomenclatureEnums.CourseKind:
                        _nomCourseKindRepository.Remove(id);
                        _nomCourseKindRepository.Save();
                        break;
                    case NomenclatureEnums.OrderPaymentKind:
                        _nomOrderPaymentKindRepository.Remove(id);
                        _nomOrderPaymentKindRepository.Save();
                        break;
                    //case NomenclatureEnums.SyndicAction:
                    //    _nomSyndicActionRepository.Remove(id);
                    //    _nomSyndicActionRepository.Save();
                    //    break;
                    //case NomenclatureEnums.ActCategory:
                    //    _nomActCategoryRepository.Remove(id);
                    //    _nomActCategoryRepository.Save();
                    //    break;
                    //case NomenclatureEnums.ActContractor:
                    //    _nomActContractorRepository.Remove(id);
                    //    _nomActContractorRepository.Save();
                    //    break;
                    case NomenclatureEnums.ActivityKind:
                        _nomActivityKindRepository.Remove(id);
                        _nomActivityKindRepository.Save();
                        break; ;
                    case NomenclatureEnums.SampleKind:
                        _nomSampleKindRepository.Remove(id);
                        _nomSampleKindRepository.Save();
                        break;
                    case NomenclatureEnums.SyndicReportType:
                        _nomSyndicReportTypeRepository.Remove(id);
                        _nomSyndicReportTypeRepository.Save();
                        break;
                    case NomenclatureEnums.CompanySize:
                        _nomCompanySizeRepository.Remove(id);
                        _nomCompanySizeRepository.Save();
                        break;
                    case NomenclatureEnums.ActAnnouncementCategory:
                        _nomActAnnouncementCategoryRepo.Remove(id);
                        _nomActAnnouncementCategoryRepo.Save();
                        break;
                    case NomenclatureEnums.RegistrationLegalBasis:
                        _nomRegistrationLegalBasis.Remove(id);
                        _nomRegistrationLegalBasis.Save();
                        break;
                    case NomenclatureEnums.SyndicKind:
                        _nomSyndicKind.Remove(id);
                        _nomSyndicKind.Save();
                        break;
                    case NomenclatureEnums.RegisterField:
                        _nomRegisterField.Remove(id);
                        _nomRegisterField.Save();
                        break;
                    case NomenclatureEnums.RegisterSyndicKind:
                        _nomRegisterSyndicKind.Remove(id);
                        _nomRegisterSyndicKind.Save();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                return Success(true, "Номенклатурата е премахната.");
            }
            catch (NullReferenceException)
            {
                return Error<bool>("Несъщесвуваща номенклатура");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
    }
}
