using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Helper;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class NomenclatureController : Controller
    {
        private readonly NomenclatureService _nomenclatureService;

        public NomenclatureController(NomenclatureService nomenclatureService)
        {
            _nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        public IActionResult GetNomenclatureEnums()
        {
            return Ok(_nomenclatureService.GetNomenclatureEnums());
        }

        [HttpGet]
        public IActionResult GetNomenclatureById(Guid id, NomenclatureEnums type)
        {
            return Ok(_nomenclatureService.GetNomenclatureById(id, type));
        }

        [HttpGet]
        public IActionResult GetNomenclatures(NomenclatureEnums type)
        {
            return Ok(_nomenclatureService.GetNomenclatures(type));
        }

        [HttpPost]
        public IActionResult InsertNomenclature([FromBody] object nomenclature, NomenclatureEnums type)
        {
            return Ok(_nomenclatureService.InsertNomenclature(nomenclature, type));
        }

        [HttpPost]
        public IActionResult UpdateNomenclature([FromBody] object nomenclature, NomenclatureEnums type)
        {
            return Ok(_nomenclatureService.UpdateNomenclature(nomenclature, type));
        }

        [HttpPost]
        public IActionResult DeleteNomenclature(Guid id, NomenclatureEnums type)
        {
            return Ok(_nomenclatureService.DeleteNomenclature(id, type));
        }
    }
}
