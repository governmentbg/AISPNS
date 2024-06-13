using AISTN.Common.Helper;
using AISTN.Common.Models.Save;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class MetaDataController : Controller
    {
        private readonly MetaDataService _metaDataService;

        public MetaDataController(MetaDataService metaDataService)
        {
            _metaDataService = metaDataService;
        }

        [HttpGet]
        public IActionResult GetCourseKinds()
        {
            return Ok(_metaDataService.GetCourseKinds());
        }

        [HttpGet]
        public IActionResult GetCourtsList()
        {
            return Ok(_metaDataService.GetCourtsList());
        }

        [HttpGet]
        public IActionResult GetSyndicStatuses()
        {
            return Ok(_metaDataService.GetSyndicStatuses());
        }

        [HttpGet]
        public IActionResult GetCompanySizes()
        {
            return Ok(_metaDataService.GetCompanySizes());
        }

        [HttpGet]
        public IActionResult GetSyndics(int pageNumber, int pageSize, string? searchCriteria, Guid? syndicId)
        {
            return Ok(_metaDataService.GetSyndics(pageNumber, pageSize, searchCriteria, syndicId));
        }

        [HttpGet]
        public IActionResult GetSyndicsShortInfo()
        {
            return Ok(_metaDataService.GetSyndicsShortInfo());
        }

        #region AnnouncementNomenclatures

        [HttpGet]
        public IActionResult GetAnnouncementStatus()
        {
            return Ok(_metaDataService.GetAnnouncementStatus());
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

        [HttpGet]
        public IActionResult GetSellAnnouncementTemplate()
        {
            return Ok(_metaDataService.GetSellAnnouncementTemplate());
        }

        #endregion

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

        #region AttachedDocumentKind

        [HttpGet]
        public IActionResult GetAttachedDocumentKinds()
        {
            return Ok(_metaDataService.GetAttachedDocumentKinds());
        }

        #endregion

        #region Users

        [HttpGet]
        public IActionResult GetUserShortInfo()
        {
            return Ok(_metaDataService.GetUserShortInfo());
        }

        #endregion

        #region Installment


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
        #endregion

        #region Templates
        [HttpGet]
        public IActionResult GetSyndicTemplateKinds()
        {
            return Ok(_metaDataService.GetTemplateKinds());
        }

        #endregion

        #region Reports
        [HttpGet]
        public IActionResult GetSyndicReportTypes()
        {
            return Ok(_metaDataService.GetNomCaseReports());
        }

        #endregion

        #region Activity
        [HttpGet]
        public IActionResult GetActivityKinds()
        {
            return Ok(_metaDataService.GetNomActivityKinds());
        }
        #endregion

        #region Entity 

        [HttpGet]
        public IActionResult GetEntities(int pageNumber, int pageSize, string? searchCriteria, Guid? entityId)
        {
            return Ok(_metaDataService.GetEntities(pageNumber, pageSize, searchCriteria, entityId));
        }

        [HttpPost]
        public IActionResult CreateEntity(SaveEntityDTO entity)
        {
            return Ok(_metaDataService.CreateEntity(entity));
        }

        [HttpGet]
        public IActionResult GetPersons(int pageNumber, int pageSize, string? searchCriteria, Guid? personId)
        {
            return Ok(_metaDataService.GetPersons(pageNumber, pageSize, searchCriteria, personId));
        }

        [HttpPost]
        public IActionResult CreatePerson(SavePersonDTO person)
        {
            return Ok(_metaDataService.CreatePerson(person));
        }

        #endregion

        #region Programs

        [HttpGet]
        public IActionResult GetCurrentPrograms()
        {
            return Ok(_metaDataService.GetCurrentPrograms());
        }

        #endregion
    }
}
