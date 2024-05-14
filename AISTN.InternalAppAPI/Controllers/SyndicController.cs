using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class SyndicController : Controller
    {
        private readonly SyndicService _syndicService;
        private readonly ActivityService _activityService;
        private readonly PropertyService _propertyService;

        public SyndicController(SyndicService syndicService, ActivityService activityService, PropertyService propertyService)
        {
            _syndicService = syndicService;
            _activityService = activityService;
            _propertyService = propertyService;
        }

        [HttpPost]
        public IActionResult SearchSyndic(int pageNumber, int pageSize, [FromBody] SyndicSearchFilter filter)
        {
            return Ok(_syndicService.SearchSyndic(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetSyndicById(Guid id)
        {
            return Ok(_syndicService.GetSyndicById(id));
        }

        [HttpGet]
        public IActionResult SendExpirationDateEmail(Guid id)
        {
            return Ok(_syndicService.SendExpirationDateEmail(id));
        }

        [HttpPost]
        public IActionResult LockSyndic(Guid id)
        {
            return Ok(_syndicService.LockSyndic(id));
        }

        [HttpPost]
        public IActionResult UnlockSyndic(Guid id)
        {
            return Ok(_syndicService.UnlockSyndic(id));
        }

        [HttpPost]
        public IActionResult ExportActiveSyndicsToExcel()
        {
            return File(_syndicService.ExportActiveSyndicsToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Active_Syndics {DateTime.Now}.xlsx");
        }

        [HttpPost]
        public IActionResult ExportActiveSyndicsWithoutPaidInstallmentToExcel()
        {
            return File(_syndicService.ExportActiveSyndicsWithoutPaidInstallmentToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Unpaid_Syndics_Installments {DateTime.Now}.xlsx");
        }

        [HttpPost]
        public IActionResult GetSyndicOrders(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicOrders(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetSyndicSignals(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicSignals(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetSyndicAppeals(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicAppeals(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult ExportSyndicOrdersToExcel(Guid syndicId)
        {
            return File(_syndicService.ExportSyndicOrdersToExcel(syndicId), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Syndic_Orders {DateTime.Now}.xlsx");
        }

        [HttpPost]
        public IActionResult GetSyndicCourses(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicCourses(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetSyndicTemplates(Guid id, [FromBody] SyndicTemplateFilter filter, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicTemplates(id, filter, pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetSyndicTemplateById(Guid id)
        {
            return Ok(_syndicService.GetSyndicTemplateById(id));
        }

        [HttpPost]
        public IActionResult GetSyndicReports(Guid id, [FromBody] SyndicReportFilter filter, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicReports(id, filter, pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetSyndicReportById(Guid id)
        {
            return Ok(_syndicService.GetSyndicReportById(id));
        }

        [HttpPost]
        public IActionResult GetSyndicCaseActivities(int pageNumber, int pageSize, ActivitySearchFilter filter)
        {
            return Ok(_activityService.SearchActivities(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetSyndicCaseActivityById(Guid id)
        {
            return Ok(_activityService.GetActivityById(id));
        }

        [HttpPost]
        public IActionResult GetSyndicCaseProperties(int pageNumber, int pageSize, PropertyCaseFilter filter)
        {
            return Ok(_propertyService.GetPropertiesByClassKind(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetSyndicCasePropertyById(Guid id)
        {
            return Ok(_propertyService.GetPropertyById(id));
        }

        [HttpPost]
        public IActionResult ExportSyndicCoursesToExcel(Guid syndicId)
        {
            return File(_syndicService.ExportSyndicCoursesToExcel(syndicId), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Syndic_Courses {DateTime.Now}.xlsx");
        }

        [HttpPost]
        public IActionResult GetSyndicInstallments(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicInstallments(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult ExportSyndicInstallmentsToExcel(Guid syndicId)
        {
            return File(_syndicService.ExportSyndicInstallmentsToExcel(syndicId), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Syndic_installments {DateTime.Now}.xlsx");
        }

        [HttpPost]
        public IActionResult GetSyndicCases(Guid id, [FromBody] CaseSearchFilter filter, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicCases(id, filter, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult CreateSyndic(SaveSyndicDTO syndicDTO)
        {
            return Ok(_syndicService.CreateSyndic(syndicDTO));
        }

        [HttpPost]
        public IActionResult UpdateSyndic(SaveSyndicDTO syndicDto)
        {
            return Ok(_syndicService.UpdateSyndic(syndicDto));
        }

        [HttpPost]
        public IActionResult DeleteSyndic(Guid id)
        {
            return Ok(_syndicService.DeleteSyndic(id));
        }

        [HttpPost]
        public IActionResult CreateSyndicCourseApplication(SaveCourseApplicationDTO courseApplicatioDTO)
        {
            return Ok(_syndicService.CreateSyndicCourseApplication(courseApplicatioDTO));
        }

        [HttpGet]
        public IActionResult GetSyndicCourseResults(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicCourseResults(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult ExportSyndicCourseResultsToExcel(Guid syndicId)
        {
            return File(_syndicService.ExportSyndicCourseResultsToExcel(syndicId), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Syndic_Course_results {DateTime.Now}.xlsx");
        }

        [HttpGet]
        public IActionResult GetSyndicCourseApplications(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicCourseApplications(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult UpdateSyndicCourseApplication(SaveCourseApplicationDTO courseApplicationDTO)
        {
            return Ok(_syndicService.UpdateSyndicCourseApplication(courseApplicationDTO));
        }

        [HttpGet]
        public IActionResult GetSyndicApplicationById(Guid id)
        {
            return Ok(_syndicService.GetSyndicApplicationById(id));
        }

        [HttpGet]
        public IActionResult GetSyndicPleaById(Guid id)
        {
            return Ok(_syndicService.GetSyndicPleaById(id));
        }

        [HttpGet]
        public IActionResult GetSyndicPleas(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicPleas(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult CreatePlea(SavePleaDTO pleaDTO)
        {
            return Ok(_syndicService.CreatePlea(pleaDTO));
        }

        [HttpPost]
        public IActionResult UpdatePlea(SavePleaDTO pleaDTO)
        {
            return Ok(_syndicService.UpdatePlea(pleaDTO));
        }

        [HttpPost]
        public IActionResult CreateCustodian(Guid syndicId)
        {
            return Ok(_syndicService.CreateCustodian(syndicId));
        }
    }
}
