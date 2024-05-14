using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Models.Save;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace AISTN.Common.Services
{
    [Injectable]
    public class MetaDataService : ServiceBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly IGenericRepository<NomAction> _nomAction;
        private readonly IGenericRepository<NomActKind> _nomActKind;
        private readonly IGenericRepository<NomActReason> _nomActReason;
        private readonly IGenericRepository<NomCaseCode> _nomCaseCode;
        private readonly IGenericRepository<NomCaseKind> _nomCaseKind;
        private readonly IGenericRepository<NomCompanySize> _nomComanySize;
        private readonly IGenericRepository<NomCourt> _nomCourt;
        private readonly IGenericRepository<NomIncomingDocumentKind> _nomIncomingDocumentKind;
        private readonly IGenericRepository<NomInvolvementKind> _nomInvolvementKind;
        private readonly IGenericRepository<NomSendToKind> _nomSendToKind;
        private readonly IGenericRepository<NomSessionKind> _nomSessionKind;
        private readonly IGenericRepository<NomSessionResult> _nomSessionResult;
        private readonly IGenericRepository<NomReportSource> _nomReportSource;
        private readonly IGenericRepository<NomReportType> _nomReportType;
        private readonly IGenericRepository<NomSpecialty> _nomSpecialty;
        private readonly IGenericRepository<NomTemplateKind> _nomTemplateKind;
        private readonly IGenericRepository<NomOrderKind> _nomOrderKind;
        private readonly IGenericRepository<NomInspectionType> _nomInspectionType;
        private readonly IGenericRepository<NomAnnouncementStatus> _nomAnnouncementStatus;
        private readonly IGenericRepository<NomLegalBasis> _nomLegalBasis;
        private readonly IGenericRepository<NomOfferingKind> _nomOfferingKind;
        private readonly IGenericRepository<NomOfferingLocationType> _nomOfferingLocationType;
        private readonly IGenericRepository<NomOfferingProcedure> _nomOfferingProcedure;
        private readonly IGenericRepository<NomObjectKind> _nomObjectKind;
        private readonly IGenericRepository<NomObjectType> _nomObjectType;
        private readonly IGenericRepository<NomPapersForSale> _nomPapersForSale;
        private readonly IGenericRepository<NomSalesProcedure> _nomSalesProcedure;
        private readonly IGenericRepository<NomDistrict> _nomDistricts;
        private readonly IGenericRepository<NomMunicipality> _nomMunicipalities;
        private readonly IGenericRepository<NomSettlement> _nomSettlements;
        private readonly IGenericRepository<NomInstallmentKind> _nomInstallmentKind;
        private readonly IGenericRepository<NomSyndicStatus> _nomSyndicStatus;
        private readonly IGenericRepository<NomSellAnnouncementTemplate> _nomAnnouncementTemplate;
        private readonly IGenericRepository<NomInstallmentYear> _nomInstallmentYear;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<NomAttachedDocumentKind> _attachedDocumentKind;
        private readonly IGenericRepository<NomDirectiveTemplateKind> _directiveTemplate;
        private readonly IGenericRepository<NomSignalSenderType> _nomSignalSenderType;
        private readonly IGenericRepository<NomSignalDocumentKind> _nomSignalDocumentKind;
        private readonly IGenericRepository<NomCourseKind> _nomCourseKind;
        private readonly IGenericRepository<NomActivityKind> _nomActivityKind;
        private readonly IGenericRepository<NomSampleKind> _nomSampleKind;
        private readonly IGenericRepository<NomSyndicReportType> _nomSyndicReportType;
        private readonly IGenericRepository<NomSyndicCaseReport> _nomSyndicCaseReport;
        private readonly IGenericRepository<NomPropertyClass> _nomPropertyClassRepository;
        private readonly IGenericRepository<NomSyndicAction> _nomSyndicActionRepository;
        private readonly IGenericRepository<Entity> _entityRepository;
        private readonly IGenericRepository<Person> _personRepository;
        private readonly IGenericRepository<Program> _programRepository;
        private readonly IGenericRepository<NomActAnnouncementCategory> _nomActAnnouncementCategory;
        private readonly IGenericRepository<NomActAnnouncementStatus> _nomActAnnouncementStatus;
        private readonly IGenericRepository<NomRegisterField> _nomRegisterField;
        private readonly IGenericRepository<NomRegistrationLegalBasis> _nomRegistrationLegalBasis;
        private readonly IGenericRepository<NomActContractor> _nomActContractor;
        private readonly IGenericRepository<NomSyndicKind> _nomSyndicKind;
        private readonly IGenericRepository<NomRegisterSyndicKind> _nomRegisterSyndicKind;


        public MetaDataService(IMapper mapper,
            ExceptionLogger logger,
            IGenericRepository<Syndic> syndicRepository,
            IGenericRepository<NomAction> nomAction,
            IGenericRepository<NomActKind> nomActKind,
            IGenericRepository<NomActReason> nomActReason,
            IGenericRepository<NomCaseCode> nomCaseCode,
            IGenericRepository<NomCaseKind> nomCaseKind,
            IGenericRepository<NomCompanySize> nomCompanySize,
            IGenericRepository<NomCourt> nomCourt,
            IGenericRepository<NomIncomingDocumentKind> nomIncomingDocumentKind,
            IGenericRepository<NomInvolvementKind> nomInvolvementKind,
            IGenericRepository<NomSendToKind> nomSendToKind,
            IGenericRepository<NomSessionKind> nomSessionKind,
            IGenericRepository<NomSessionResult> nomSessionResult,
            IGenericRepository<User> userRepository,
            IGenericRepository<NomReportSource> nomReportSource,
            IGenericRepository<NomReportType> nomReportType,
            IGenericRepository<NomSpecialty> nomSpecialty,
            IGenericRepository<NomOrderKind> nomOrderKind,
            IGenericRepository<NomTemplateKind> nomTemplateKind,
            IGenericRepository<NomInspectionType> nomInspectionType,
            IGenericRepository<NomAnnouncementStatus> nomAnnouncementStatus,
            IGenericRepository<NomLegalBasis> nomLegalBasis,
            IGenericRepository<NomOfferingKind> nomOfferingKind,
            IGenericRepository<NomOfferingLocationType> nomOfferingLocationType,
            IGenericRepository<NomOfferingProcedure> nomOfferingProcedure,
            IGenericRepository<NomObjectKind> nomObjectKind,
            IGenericRepository<NomObjectType> nomObjectType,
            IGenericRepository<NomPapersForSale> nomPapersForSale,
            IGenericRepository<NomSalesProcedure> nomSalesProcedure,
            IGenericRepository<NomDistrict> nomDistricts,
            IGenericRepository<NomMunicipality> nomMunicipalities,
            IGenericRepository<NomSettlement> nomSettlements,
            IGenericRepository<NomInstallmentKind> nomInstallmentKind,
            IGenericRepository<NomSyndicStatus> nomSyndicStatus,
            IGenericRepository<NomSellAnnouncementTemplate> nomAnnouncementTemplate,
            IGenericRepository<NomInstallmentYear> nomInstallmentYear,
            IGenericRepository<Role> roleRepository,
            IGenericRepository<NomAttachedDocumentKind> attachedDocumentKind,
            ExcelGenerator excelGenerator,
            IGenericRepository<NomDirectiveTemplateKind> directiveTemplate,
            IGenericRepository<NomSignalSenderType> nomSignalSenderType,
            IGenericRepository<NomSignalDocumentKind> nomSignalDocumentKind,
            IGenericRepository<NomCourseKind> nomCourseKind,
            IGenericRepository<NomSampleKind> nomSampleKind,
            IGenericRepository<NomActivityKind> nomActivityKind,
            IGenericRepository<NomSyndicReportType> nomSyndicReportType,
            IGenericRepository<NomSyndicCaseReport> nomSyndicCaseReport,
            IGenericRepository<NomPropertyClass> nomPropertyClassRepository,
            IGenericRepository<NomSyndicAction> nomSyndicActionRepository,
            IGenericRepository<Entity> entityRepository,
            IGenericRepository<Person> personRepository,
            IGenericRepository<Program> programRepository,
            IGenericRepository<NomActAnnouncementCategory> nomActAnnouncementCategory,
            IGenericRepository<NomActAnnouncementStatus> nomActAnnouncementStatus,
            IGenericRepository<NomRegisterField> nomRegisterField,
            IGenericRepository<NomRegistrationLegalBasis> nomRegistrationLegalBasis,
            IGenericRepository<NomActContractor> nomActContractor,
            IGenericRepository<NomSyndicKind> nomSyndicKind,
            IGenericRepository<NomRegisterSyndicKind> nomRegisterSyndicKind) : base(mapper, logger, excelGenerator)
        {
            _userRepository = userRepository;
            _syndicRepository = syndicRepository;
            _nomAction = nomAction;
            _nomActKind = nomActKind;
            _nomActReason = nomActReason;
            _nomCaseCode = nomCaseCode;
            _nomCaseKind = nomCaseKind;
            _nomComanySize = nomCompanySize;
            _nomCourt = nomCourt;
            _nomIncomingDocumentKind = nomIncomingDocumentKind;
            _nomInvolvementKind = nomInvolvementKind;
            _nomSendToKind = nomSendToKind;
            _nomSessionKind = nomSessionKind;
            _nomSessionResult = nomSessionResult;
            _nomReportSource = nomReportSource;
            _nomReportType = nomReportType;
            _nomSpecialty = nomSpecialty;
            _nomTemplateKind = nomTemplateKind;
            _nomOrderKind = nomOrderKind;
            _nomInspectionType = nomInspectionType;
            _nomAnnouncementStatus = nomAnnouncementStatus;
            _nomLegalBasis = nomLegalBasis;
            _nomOfferingKind = nomOfferingKind;
            _nomOfferingLocationType = nomOfferingLocationType;
            _nomOfferingProcedure = nomOfferingProcedure;
            _nomObjectKind = nomObjectKind;
            _nomObjectType = nomObjectType;
            _nomPapersForSale = nomPapersForSale;
            _nomSalesProcedure = nomSalesProcedure;
            _nomDistricts = nomDistricts;
            _nomMunicipalities = nomMunicipalities;
            _nomSettlements = nomSettlements;
            _nomInstallmentKind = nomInstallmentKind;
            _nomSyndicStatus = nomSyndicStatus;
            _nomAnnouncementTemplate = nomAnnouncementTemplate;
            _nomInstallmentYear = nomInstallmentYear;
            _roleRepository = roleRepository;
            _attachedDocumentKind = attachedDocumentKind;
            _directiveTemplate = directiveTemplate;
            _nomSignalSenderType = nomSignalSenderType;
            _nomSignalDocumentKind = nomSignalDocumentKind;
            _nomCourseKind = nomCourseKind;
            _nomSampleKind = nomSampleKind;
            _nomActivityKind = nomActivityKind;
            _nomSyndicReportType = nomSyndicReportType;
            _nomSyndicCaseReport = nomSyndicCaseReport;
            _nomPropertyClassRepository = nomPropertyClassRepository;
            _nomSyndicActionRepository = nomSyndicActionRepository;
            _entityRepository = entityRepository;
            _personRepository = personRepository;
            _programRepository = programRepository;
            _nomActAnnouncementCategory = nomActAnnouncementCategory;
            _nomActAnnouncementStatus = nomActAnnouncementStatus;
            _nomRegisterField = nomRegisterField;
            _nomRegistrationLegalBasis = nomRegistrationLegalBasis;
            _nomActContractor = nomActContractor;
            _nomSyndicKind = nomSyndicKind;
            _nomRegisterSyndicKind = nomRegisterSyndicKind;
        }

        #region Users

        public OperationResult<List<UserShortInfoDTO>> GetUsers()
        {
            try
            {
                var result = _userRepository.GetAll();

                return Success(_mapper.Map<List<UserShortInfoDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<UserShortInfoDTO>>(ex);
            }
        }

        #endregion

        #region UserActions
        public OperationResult<List<string>> GetLogUserActions()
        {
            try
            {
                var enums = new List<string>();

                foreach (var action in Enum.GetNames(typeof(eUserActionType)))
                {
                    enums.Add(action);
                }

                return Success(enums);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<string>>(ex);
            }
        }

        #endregion

        #region Syndics

        public OperationResult<PagedList<SyndicDTO>> GetSyndics(int pageNumber, int pageSize, string? searchCriteria, Guid? syndicId)
        {
            try
            {
                var query = _syndicRepository.Get(filter: x => ((searchCriteria == null) || x.FirstName.Contains(searchCriteria))
                                                                 || x.SecondName.Contains(searchCriteria)
                                                                 || x.LastName.Contains(searchCriteria)
                                                                 || x.Egn.Contains(searchCriteria),
                                                                 source => source.Include(x => x.Syndic2Addresses)
                                                                 .ThenInclude(x => x.Address)).Where(x => x.Id != syndicId)
                                                                 .OrderBy(x => x.FirstName)
                                                                 .ToList();
                if (syndicId.HasValue)
                {
                    query.Insert(0, _syndicRepository.GetById(syndicId.Value));
                }



                return Success(PagedList<SyndicDTO>.ToPagedList(_mapper.Map<List<SyndicDTO>>(query).AsQueryable(), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<SyndicDTO>>(ex);
            }
        }

        public OperationResult<List<SyndicShortInfoDTO>> GetSyndicsShortInfo()
        {
            try
            {
                var query = _syndicRepository.GetAll().OrderBy(x => x.FirstName);

                return Success(_mapper.Map<List<SyndicShortInfoDTO>>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<SyndicShortInfoDTO>>(ex);
            }
        }

        // Returns SyndicDTO (which must be identical to SaveSyndicDTO) with information for the current user
        public OperationResult<SyndicDTO> GetUserInfo(HttpContext context)
        {
            try
            {
                var username = context.User.Identity?.Name;
                var ip = context.Connection.RemoteIpAddress;
                Guid? userId = null;

                if (username != null && ip != null)
                {
                    if (username.Split("\\").Length > 1)
                    {
                        username = username.Split("\\")[1].ToLower();
                    }

                    var user = _userRepository.Get(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
                    userId = user.Id;

                }
                else
                {
                    // TODO: Remove dummy id when we no longer use the dummy
                    userId = Guid.Parse("B488DA43-C8B7-4CBE-A5B7-09079D298933");
                }

                var syndicEntity = _syndicRepository.Get(x => x.UserId == userId.Value,
                        source => source
                            .Include(x => x.SpecialtyNavigation)
                            .Include(x => x.SyndicStatus)
                            .Include(x => x.Installments)
                            .Include(x => x.Syndic2Addresses)
                                .ThenInclude(x => x.Address)
                            .Include(x => x.Orders))
                    .SingleOrDefault();

                return Success(_mapper.Map<SyndicDTO>(syndicEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SyndicDTO>(ex);
            }
        }

        public OperationResult<List<UserShortInfoDTO>> GetUserShortInfo()
        {
            try
            {
                var query = _userRepository.Get(filter: x => x.Roles.Any(x => (x.Name == "MEIEmployee") || (x.Name == "Employee")), source => source.Include(x => x.Roles)); ;

                return Success(_mapper.Map<List<UserShortInfoDTO>>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<UserShortInfoDTO>>(ex);
            }
        }


        #endregion

        #region Roles

        public OperationResult<List<RoleDTO>> GetRoles()
        {
            try
            {
                var result = _roleRepository.GetAll();

                return Success(_mapper.Map<List<RoleDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<RoleDTO>>(ex);
            }
        }

        #endregion

        #region Case Info Nomenclatures

        public OperationResult<List<NomenclatureDTO>> GetActions()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomAction.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetActKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomActKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetActReasons()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomActReason.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomCaseCodeDTO>> GetCaseCodes()
        {
            try
            {
                return Success(_mapper.Map<List<NomCaseCodeDTO>>(_nomCaseCode.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomCaseCodeDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetCaseKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomCaseKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomCourtDTO>> GetCourtsList()
        {
            try
            {
                return Success(_mapper.Map<List<NomCourtDTO>>(_nomCourt.GetAll().OrderBy(x => x.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomCourtDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetIncomingDocumentKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomIncomingDocumentKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetInvolvementKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomInvolvementKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetSendToKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomSendToKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetSessionKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomSessionKind.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetSessionResults()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomSessionResult.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomReportSourceDTO>> GetReportSources()
        {
            try
            {
                return Success(_mapper.Map<List<NomReportSourceDTO>>(_nomReportSource.GetAll().OrderBy(x => x.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomReportSourceDTO>>(ex);
            }
        }

        public OperationResult<List<NomReportTypeDTO>> GetReportTypes()
        {
            try
            {
                return Success(_mapper.Map<List<NomReportTypeDTO>>(_nomReportType.GetAll().OrderBy(x => x.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomReportTypeDTO>>(ex);
            }
        }

        public OperationResult<List<NomSyndicStatusDTO>> GetSyndicStatuses()
        {
            try
            {
                return Success(_mapper.Map<List<NomSyndicStatusDTO>>(_nomSyndicStatus.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSyndicStatusDTO>>(ex);
            }
        }

        public OperationResult<List<NomCompanySizeDTO>> GetCompanySizes()
        {
            try
            {
                return Success(_mapper.Map<List<NomCompanySizeDTO>>(_nomComanySize.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomCompanySizeDTO>>(ex);
            }
        }

        #endregion

        #region Syndic Nomenclatures

        public OperationResult<List<NomSpecialityDTO>> GetSpecialties()
        {
            try
            {
                return Success(_mapper.Map<List<NomSpecialityDTO>>(_nomSpecialty.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSpecialityDTO>>(ex);
            }
        }

        public OperationResult<List<NomOrderKindDTO>> GetOrderKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomOrderKindDTO>>(_nomOrderKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomOrderKindDTO>>(ex);
            }
        }

        public OperationResult<List<NomInstallmentKindDTO>> GetInstallmentKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomInstallmentKindDTO>>(_nomInstallmentKind.GetAll().OrderBy(x => x.Id)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomInstallmentKindDTO>>(ex);
            }
        }

        public OperationResult<List<NomInstallmentYearDTO>> GetInstallmentYears()
        {
            try
            {
                return Success(_mapper.Map<List<NomInstallmentYearDTO>>(_nomInstallmentYear.GetAll().OrderByDescending(x => x.Year)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomInstallmentYearDTO>>(ex);
            }
        }

        public OperationResult<List<NomTemplateKindDTO>> GetTemplateKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomTemplateKindDTO>>(_nomTemplateKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomTemplateKindDTO>>(ex);
            }
        }

        public OperationResult<List<NomInspectionTypeDTO>> GetInspectionTypes()
        {
            try
            {
                return Success(_mapper.Map<List<NomInspectionTypeDTO>>(_nomInspectionType.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomInspectionTypeDTO>>(ex);
            }
        }

        public OperationResult<List<NomSellAnnouncementTemplate>> GetSellAnnouncementTemplate()
        {
            try
            {
                return Success(_mapper.Map<List<NomSellAnnouncementTemplate>>(_nomAnnouncementTemplate.GetAll().OrderBy(x => x.Number)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSellAnnouncementTemplate>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetDirectiveTemplateKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_directiveTemplate.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomSignalSenderTypeDTO>> GetSignalSenderTypes()
        {
            try
            {
                return Success(_mapper.Map<List<NomSignalSenderTypeDTO>>(_nomSignalSenderType.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSignalSenderTypeDTO>>(ex);
            }
        }

        public OperationResult<List<NomCourseKindDTO>> GetCourseKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomCourseKindDTO>>(_nomCourseKind.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomCourseKindDTO>>(ex);
            }
        }

        #endregion

        #region Announcement Nomenclatures

        public OperationResult<List<NomenclatureDTO>> GetAnnouncementStatus()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomAnnouncementStatus.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetLegalBasis()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomLegalBasis.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetOfferingKind()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetOfferingLocationType()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingLocationType.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetOfferingProcedure()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomOfferingProcedure.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetObjectKind()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomObjectKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<NomenclatureDTO>> GetObjectType()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomObjectType.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetPapersForSale()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomPapersForSale.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        public OperationResult<List<AnnouncementNomenclatureDTO>> GetSalesProcedure()
        {
            try
            {
                return Success(_mapper.Map<List<AnnouncementNomenclatureDTO>>(_nomSalesProcedure.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<AnnouncementNomenclatureDTO>>(ex);
            }
        }

        #endregion

        #region Address Nomenclatures

        public OperationResult<List<NomDistrictDTO>> GetDistricts()
        {
            try
            {
                return Success(_mapper.Map<List<NomDistrictDTO>>(_nomDistricts.GetAll().OrderBy(x => x.ViewOrder)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomDistrictDTO>>(ex);
            }
        }

        public OperationResult<List<NomMunicipalityDTO>> GetMunicipalities(Guid districtId)
        {
            try
            {
                return Success(_mapper.Map<List<NomMunicipalityDTO>>(_nomMunicipalities.Get(filter: x => x.DistrictId == districtId)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomMunicipalityDTO>>(ex);
            }
        }

        public OperationResult<List<NomSettlementDTO>> GetSettlements(Guid municipalityId)
        {
            try
            {
                return Success(_mapper.Map<List<NomSettlementDTO>>(_nomSettlements.Get(filter: x => x.MunicipalityId == municipalityId)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSettlementDTO>>(ex);
            }
        }

        #endregion

        #region AttachedDocumentKind
        public OperationResult<List<NomAttachedDocumentKindDTO>> GetAttachedDocumentKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomAttachedDocumentKindDTO>>(_attachedDocumentKind.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomAttachedDocumentKindDTO>>(ex);
            }
        }

        public OperationResult<List<NomSignalDocumentKindDTO>> GetAttachedDocumentSignalKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomSignalDocumentKindDTO>>(_nomSignalDocumentKind.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSignalDocumentKindDTO>>(ex);
            }
        }
        #endregion

        #region Activity Kind Nomenclatures

        public OperationResult<List<NomenclatureDTO>> GetNomActivityKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomActivityKind.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }


        #endregion

        #region Sample Kind Nomenclatures

        public OperationResult<List<NomenclatureDTO>> GetNomSampleKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomenclatureDTO>>(_nomSampleKind.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomenclatureDTO>>(ex);
            }
        }

        #endregion

        #region Syndic Report Type Nomencaltures

        public OperationResult<List<NomReportTypeDTO>> GetNomReportTypes()
        {
            try
            {
                return Success(_mapper.Map<List<NomReportTypeDTO>>(_nomReportType.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomReportTypeDTO>>(ex);
            }
        }

        #endregion

        #region Syndic Case Report Nomencaltures

        public OperationResult<List<NomSyndicCaseReportDTO>> GetNomCaseReports()
        {
            try
            {
                return Success(_mapper.Map<List<NomSyndicCaseReportDTO>>(_nomSyndicCaseReport.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSyndicCaseReportDTO>>(ex);
            }
        }

        #endregion

        #region Property

        public OperationResult<List<NomPropertyClassDTO>> GetNomPropertyClasses()
        {
            try
            {
                return Success(_mapper.Map<List<NomPropertyClassDTO>>(_nomPropertyClassRepository.GetAll()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomPropertyClassDTO>>(ex);
            }
        }

        #endregion

        #region Entity

        public OperationResult<PagedList<EntityDTO>> GetEntities(int pageNumber, int pageSize, string searchCriteria, Guid? entityId)
        {
            try
            {
                var query = _entityRepository.Get(filter: x => ((searchCriteria == null) || x.Name.Contains(searchCriteria) || x.Bulstat.Contains(searchCriteria)))
                                             .AsQueryable()
                                             .ProjectTo<EntityDTO>(_mapper.ConfigurationProvider);

                if (entityId.HasValue)
                {
                    query = _entityRepository.Get(filter: x => (x.Id == entityId))
                                             .AsQueryable()
                                             .ProjectTo<EntityDTO>(_mapper.ConfigurationProvider);
                }

                return Success(PagedList<EntityDTO>.ToPagedList(query, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<EntityDTO>>(ex);
            }

        }


        public OperationResult<SaveEntityDTO> CreateEntity(SaveEntityDTO entity)
        {
            try
            {
                var mappedEntity = _mapper.Map<Entity>(entity);
                _entityRepository.Add(mappedEntity);
                _entityRepository.Save();
                entity.Id = mappedEntity.Id;
                return Success(entity, "Юридическото лице е създадено");

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveEntityDTO>(ex);
            }
        }

        public OperationResult<PagedList<PersonDTO>> GetPersons(int pageNumber, int pageSize, string searchCriteria, Guid? personId)
        {
            try
            {
                var query = _personRepository.Get(filter: x => ((searchCriteria == null) || x.FirstName.Contains(searchCriteria))
                                                                 || x.MiddleName.Contains(searchCriteria)
                                                                 || x.LastName.Contains(searchCriteria)
                                                                 || x.Egn.Contains(searchCriteria))
                              .AsQueryable()
                              .ProjectTo<PersonDTO>(_mapper.ConfigurationProvider);

                if (personId.HasValue)
                {
                    query = _personRepository.Get(filter: x => (x.Id == personId))
                                             .AsQueryable()
                                             .ProjectTo<PersonDTO>(_mapper.ConfigurationProvider);
                }

                return Success(PagedList<PersonDTO>.ToPagedList(query, pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<PersonDTO>>(ex);
            }
        }
        public OperationResult<SavePersonDTO> CreatePerson(SavePersonDTO person)
        {
            try
            {
                var mapperdPerson = _mapper.Map<Person>(person);
                _personRepository.Add(mapperdPerson);
                _personRepository.Save();
                person.Id = mapperdPerson.Id;
                return Success(person, "Физическото лице е създадено");

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SavePersonDTO>(ex);
            }
        }

        #endregion

        #region Programs
        public OperationResult<List<ProgramIndexDTO>> GetCurrentPrograms()
        {
            try
            {
                var startOfYearDate = new DateTime(DateTime.Now.Year, 1, 1);

                return Success(_mapper.Map<List<ProgramIndexDTO>>(_programRepository.Get(filter: x => x.FromDate > startOfYearDate, x => x.Include(x => x.Courses)).AsQueryable()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<ProgramIndexDTO>>(ex);
            }
        }
        #endregion

        #region ActAnnouncement

        public OperationResult<List<NomActAnnouncementCategoryDTO>> GetActAnnouncementCategories(bool isStabilization)
        {
            try
            {
                return Success(_mapper.Map<List<NomActAnnouncementCategoryDTO>>(_nomActAnnouncementCategory.Get(x => x.IsStabilization == isStabilization)
                                                                                                                               .OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomActAnnouncementCategoryDTO>>(ex);
            }
        }

        public OperationResult<List<NomActAnnouncementStatusDTO>> GetActAnnouncementStatus()
        {
            try
            {
                return Success(_mapper.Map<List<NomActAnnouncementStatusDTO>>(_nomActAnnouncementStatus.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomActAnnouncementStatusDTO>>(ex);
            }
        }

        public OperationResult<List<NomRegisterFieldDTO>> GetRegisterFields()
        {
            try
            {
                return Success(_mapper.Map<List<NomRegisterFieldDTO>>(_nomRegisterField.GetAll().OrderBy(x => x.Code)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomRegisterFieldDTO>>(ex);
            }
        }

        public OperationResult<List<NomRegistrationLegalBasisDTO>> GetRegistrationLegalBasis()
        {
            try
            {
                return Success(_mapper.Map<List<NomRegistrationLegalBasisDTO>>(_nomRegistrationLegalBasis.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomRegistrationLegalBasisDTO>>(ex);
            }
        }

        #endregion

        #region Act

        public OperationResult<List<NomRegisterFieldDTO>> GetActContractors(bool isStabilization)
        {
            try
            {
                return Success(_mapper.Map<List<NomRegisterFieldDTO>>(_nomRegisterField.Get(x => x.IsStabilization == isStabilization)
                                                                                                .OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomRegisterFieldDTO>>(ex);
            }
        }

        #endregion

        public OperationResult<List<NomSyndicKindDTO>> GetSyndicKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomSyndicKindDTO>>(_nomSyndicKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomSyndicKindDTO>>(ex);
            }
        }

        public OperationResult<List<NomRegisterSyndicKindDTO>> GetRegisterSyndicKinds()
        {
            try
            {
                return Success(_mapper.Map<List<NomRegisterSyndicKindDTO>>(_nomRegisterSyndicKind.GetAll().OrderBy(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<NomRegisterSyndicKindDTO>>(ex);
            }
        }
    }
}
