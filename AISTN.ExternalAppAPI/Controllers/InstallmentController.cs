using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    //[Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class InstallmentController : Controller
    {
        private readonly InstallmentService _installmentService;

        public InstallmentController(InstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpPost]
        public IActionResult GetSyndicInstallments(int pageNumber, int pageSize)
        {
            return Ok(_installmentService.GetSyndicInstallments(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetInstallmentById(Guid id)
        {
            return Ok(_installmentService.GetInstallmentById(id));
        }

        [HttpPost]
        public IActionResult CreateInstallment(SaveInstallmentDTO installmentDTO)
        {
            return Ok(_installmentService.CreateInstallment(installmentDTO));
        }

        [HttpPost]
        public IActionResult UpdateInstallment(SaveInstallmentDTO installmentDTO)
        {
            return Ok(_installmentService.UpdateInstallment(installmentDTO));
        }
    }
}
