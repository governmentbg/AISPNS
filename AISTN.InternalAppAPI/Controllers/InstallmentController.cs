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
    public class InstallmentController : Controller
    {
        private readonly InstallmentService _installmentService;

        public InstallmentController(InstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpPost]
        public IActionResult SearchInstallment(InstallmentSearchFilter filter, int pageNumber, int pageSize)
        {
            return Ok(_installmentService.SearchInstallment(filter, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult ExportSearchInstallmentToExcel(InstallmentSearchFilter filter)
        {
            return File(_installmentService.ExportSearchInstallmentToExcel(filter), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Installment_Search_installment {DateTime.Now}.xlsx");
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

        [HttpPost]
        public IActionResult DeleteInstallment(Guid id)
        {
            return Ok(_installmentService.DeleteInstallment(id));
        }
    }
}
