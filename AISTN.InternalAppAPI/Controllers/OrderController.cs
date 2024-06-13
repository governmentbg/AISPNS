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
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult SearchOrder(int pageNumber, int pageSize, [FromBody] OrderSearchFilter filter)
        {
            return Ok(_orderService.SearchOrder(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetOrderById(Guid id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpPost]
        public IActionResult CreateOrder(SaveOrderDTO orderDTO)
        {
            return Ok(_orderService.CreateOrder(orderDTO));
        }

        [HttpPost]
        public IActionResult UpdateOrder(SaveOrderDTO orderDTO)
        {
            return Ok(_orderService.UpdateOrder(orderDTO));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Guid id)
        {
            return Ok(_orderService.DeleteOrder(id));
        }
    }
}
