using AISTN.Common.Helper;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class MetaDataController : Controller
    {
        private readonly MetaDataService _metaDataService;

        public MetaDataController(MetaDataService metaDataService)
        {
            _metaDataService = metaDataService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_metaDataService.GetUsers());
        }

        [HttpGet]
        public IActionResult GetLogUserActions()
        {
            return Ok(_metaDataService.GetLogUserActions());
        }

        [HttpGet]
        public IActionResult GetSyndics(int pageNumber, int pageSize, string? searchCriteria, Guid? syndicId)
        {
            return Ok(_metaDataService.GetSyndics(pageNumber, pageSize, searchCriteria, syndicId));
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_metaDataService.GetRoles());
        }

        [HttpGet]
        public IActionResult GetCourtsList()
        {
            return Ok(_metaDataService.GetCourtsList());
        }

        [HttpGet]
        public IActionResult GetReportSources()
        {
            return Ok(_metaDataService.GetReportSources());
        }

        [HttpGet]
        public IActionResult GetReportTypes()
        {
            return Ok(_metaDataService.GetReportTypes());
        }

        [HttpGet]
        public IActionResult GetSyndicStatuses()
        {
            return Ok(_metaDataService.GetSyndicStatuses());
        }

        [HttpGet]
        public IActionResult GetSpecialties()
        {
            return Ok(_metaDataService.GetSpecialties());
        }

        [HttpGet]
        public IActionResult GetOrderKinds()
        {
            return Ok(_metaDataService.GetOrderKinds());
        }

        [HttpGet]
        public IActionResult GetInstallmentKinds()
        {
            return Ok(_metaDataService.GetInstallmentKinds());
        }

        [HttpGet]
        public IActionResult GetInstallmentYears()
        {
            return Ok(_metaDataService.GetInstallmentYears());
        }

        [HttpGet]
        public IActionResult GetSignalSenderTypes()
        {
            return Ok(_metaDataService.GetSignalSenderTypes());
        }

        [HttpGet]
        public IActionResult GetCourseKinds()
        {
            return Ok(_metaDataService.GetCourseKinds());
        }

        #region SyndicNomenclatures

        [HttpGet]
        public IActionResult GetInspectionTypes()
        {
            return Ok(_metaDataService.GetInspectionTypes());
        }

        [HttpGet]
        public IActionResult GetAttachedDocumentSignalKinds()
        {
            return Ok(_metaDataService.GetAttachedDocumentSignalKinds());
        }

        [HttpGet]
        public IActionResult GetNomActivityKinds()
        {
            return Ok(_metaDataService.GetNomActivityKinds());
        }

        [HttpGet]
        public IActionResult GetNomReportTypes()
        {
            return Ok(_metaDataService.GetNomReportTypes());
        }

        [HttpGet]
        public IActionResult GetNomCaseReports()
        {
            return Ok(_metaDataService.GetNomCaseReports());
        }

        [HttpGet]
        public IActionResult GetNomSampleKinds()
        {
            return Ok(_metaDataService.GetNomSampleKinds());
        }

        [HttpGet]
        public IActionResult GetSyndicsShortInfo()
        {
            return Ok(_metaDataService.GetSyndicsShortInfo());
        }

        [HttpGet]
        public IActionResult GetCompanySizes()
        {
            return Ok(_metaDataService.GetCompanySizes());
        }

        #endregion

        #region AnnouncementNomenclatures

        [HttpGet]
        public IActionResult GetAnnouncementStatus()
        {
            return Ok(_metaDataService.GetAnnouncementStatus());
        }

        [HttpGet]
        public IActionResult GetSellAnnouncementTemplate()
        {
            return Ok(_metaDataService.GetSellAnnouncementTemplate());
        }

        [HttpGet]
        public IActionResult GetLegalBasis()
        {
            return Ok(_metaDataService.GetLegalBasis());
        }

        [HttpGet]
        public IActionResult GetOfferingLocationType()
        {
            return Ok(_metaDataService.GetOfferingLocationType());
        }

        [HttpGet]
        public IActionResult GetOfferingProcedure()
        {
            return Ok(_metaDataService.GetOfferingProcedure());
        }

        [HttpGet]
        public IActionResult GetOfferingKind()
        {
            return Ok(_metaDataService.GetOfferingKind());
        }

        [HttpGet]
        public IActionResult GetObjectKind()
        {
            return Ok(_metaDataService.GetObjectKind());
        }


        [HttpGet]
        public IActionResult GetObjectType()
        {
            return Ok(_metaDataService.GetObjectType());
        }

        [HttpGet]
        public IActionResult GetPapersForSale()
        {
            return Ok(_metaDataService.GetPapersForSale());
        }

        [HttpGet]
        public IActionResult GetSalesProcedure()
        {
            return Ok(_metaDataService.GetSalesProcedure());
        }

        #endregion

        [HttpGet]
        public IActionResult GetDirectiveTemplateKinds()
        {
            return Ok(_metaDataService.GetDirectiveTemplateKinds());
        }

        [HttpGet]
        public IActionResult GetSyndicTemplateKinds()
        {
            return Ok(_metaDataService.GetTemplateKinds());
        }

        #region AddressNomenclatures

        [HttpGet]
        public IActionResult GetDistricts()
        {
            return Ok(_metaDataService.GetDistricts());
        }

        [HttpGet]
        public IActionResult GetMunicipalities(Guid districtId)
        {
            return Ok(_metaDataService.GetMunicipalities(districtId));
        }

        [HttpGet]
        public IActionResult GetSettlements(Guid municipalityId)
        {
            return Ok(_metaDataService.GetSettlements(municipalityId));
        }

        #endregion

        #region Property

        [HttpGet]
        public IActionResult GetAllPropertyClassKinds()
        {
            return Ok(_metaDataService.GetNomPropertyClasses());
        }

        #endregion

        #region Activity
        [HttpGet]
        public IActionResult GetActivityKinds()
        {
            return Ok(_metaDataService.GetNomActivityKinds());
        }
        #endregion

        #region Report

        [HttpGet]
        public IActionResult GetSyndicReportTypes()
        {
            return Ok(_metaDataService.GetNomCaseReports());
        }

        #endregion

        #region Entity 

        [HttpGet]
        public IActionResult GetEntities(int pageNumber, int pageSize, string? searchCriteria, Guid? entityId)
        {
            return Ok(_metaDataService.GetEntities(pageNumber, pageSize, searchCriteria, entityId));
        }

        [HttpGet]
        public IActionResult GetPersons(int pageNumber, int pageSize, string? searchCriteria, Guid? personId)
        {
            return Ok(_metaDataService.GetPersons(pageNumber, pageSize, searchCriteria, personId));
        }

        #endregion

        #region ActAnnouncement

        [HttpGet]
        public IActionResult GetActAnnouncementCategories(bool isStabilization)
        {
            return Ok(_metaDataService.GetActAnnouncementCategories(isStabilization));
        }

        [HttpGet]
        public IActionResult GetActAnnouncementStatus()
        {
            return Ok(_metaDataService.GetActAnnouncementStatus());
        }

        [HttpGet]
        public IActionResult GetRegisterFields()
        {
            return Ok(_metaDataService.GetRegisterFields());
        }

        [HttpGet]
        public IActionResult GetRegistrationLegalBasis()
        {
            return Ok(_metaDataService.GetRegistrationLegalBasis());
        }

        #endregion

        #region Act

        [HttpGet]
        public IActionResult GetActContractors(bool isStabilization)
        {
            return Ok(_metaDataService.GetActContractors(isStabilization));
        }

        #endregion

        [HttpGet]
        public IActionResult GetSyndicKinds()
        {
            return Ok(_metaDataService.GetSyndicKinds());
        }

        [HttpGet]
        public IActionResult GetRegisterSyndicKinds()
        {
            return Ok(_metaDataService.GetRegisterSyndicKinds());
        }
    }
}
