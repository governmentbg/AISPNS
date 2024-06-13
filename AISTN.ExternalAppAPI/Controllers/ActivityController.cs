using Microsoft.AspNetCore.Mvc;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Services;
using AISTN.ExternalAppAPI.Models.Save;
using Microsoft.AspNetCore.Authorization;
using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Helper;

namespace AISTN.ExternalAppAPI.Controllers;

[Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
[Route("api/ExternalApp/[controller]/[action]")]
[ApiController]
public class ActivityController : Controller
{
    private readonly ActivityService _activityService;
    public ActivityController(ActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public IActionResult GetActivityById(Guid id)
    {
        return Ok(_activityService.GetActivityById(id));
    }

    [HttpGet]
    public IActionResult GetAll(int pageNumber, int pageSize)
    {
        return Ok(_activityService.GetAllActivities(pageNumber, pageSize));
    }

    [HttpPost]
    public IActionResult SearchActivities(int pageNumber, int pageSize, [FromBody] ActivitySearchFilter filter)
    {
        return Ok(_activityService.SearchActivities(pageNumber, pageSize, filter));
    }

    [HttpPost]
    public IActionResult CreateActivity(SaveActivityDTO activityDTO)
    {
        return Ok(_activityService.CreteActivity(activityDTO));
    }

    [HttpPost]
    public IActionResult UpdateActivity(SaveActivityDTO activityDTO)
    {
        return Ok(_activityService.UpdateActivity(activityDTO));
    }

    [HttpPost]
    public IActionResult DeleteActivity(Guid id)
    {
        return Ok(_activityService.DeleteActivity(id));
    }

    [HttpPost]
    public FileResult GenerateActivityDocument(Guid caseId)
    {
        TemplateDownloadModel file = _activityService.GenerateActivityDocument(caseId);

        return File(file.BlobContent, file.MimeType, file.FileName);
    }
}