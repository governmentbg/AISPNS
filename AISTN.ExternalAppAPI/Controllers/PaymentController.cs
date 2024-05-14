using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult SendPaymentRequest(Guid installmentId)
        {
            return Ok(_paymentService.SendPaymentRequest(installmentId));
        }

        [HttpPost]
        public IActionResult GetPaymentOrderData(string paymentRequestId)
        {
            return Ok(_paymentService.GetPaymentOrderData(paymentRequestId));
        }

        [HttpPost]
        public IActionResult GetPaymentVPOSData(string paymentRequestId)
        {
            var requestedUri = Request.Scheme + Uri.SchemeDelimiter + Request.Host;
            return Ok(_paymentService.GetVposPaymentData(paymentRequestId, requestedUri));
        }
    }
}
