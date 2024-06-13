using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult GetMessageById(Guid id)
        {
            return Ok(_messageService.GetMessageById(id));
        }

        [HttpGet]
        public IActionResult GetUnreadMessages()
        {
            return Ok(_messageService.GetUnreadMessages());
        }

        [HttpGet]
        public IActionResult GetSentMessages(int pageNumber, int pageSize)
        {
            return Ok(_messageService.GetSentMessages(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetReceivedMessages(int pageNumber, int pageSize)
        {
            return Ok(_messageService.GetReceivedMessages(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult SendMessage([FromForm] SaveMessageDTO messageDTO)
        {
            return Ok(_messageService.SendMessage(messageDTO));
        }

        [HttpPost]
        public IActionResult ReplyToMessage([FromForm] ReplyToMessageDTO messageDTO)
        {
            return Ok(_messageService.ReplyToMessage(messageDTO));
        }

        [HttpPost]
        public IActionResult GetAllMessageReplies(Guid messageId)
        {
            return Ok(_messageService.GetAllMessageReplies(messageId));
        }
    }
}
