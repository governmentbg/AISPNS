using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult SubmitDocument([FromForm] FileDTO doc)
        {
            return Ok(_documentService.AttachDocumentFile(doc));
        }

        [HttpPost]
        public IActionResult GetAllDocumentFiles(int pageNumber, int pageSize, [FromBody] List<Guid> guids)
        {
            return Ok(_documentService.GetAllDocumentFiles(pageNumber, pageSize, guids));
        }

        [HttpGet]
        public FileResult DownloadAttachedFile(Guid id)
        {
            var attachedDocument = _documentService.GetAttachedDocuments(id);

            return File(attachedDocument.Blob?.DocumentContent, attachedDocument?.ContentMimeType, attachedDocument?.FileName);
        }

        [HttpPost]
        public IActionResult GetFileName(Guid id)
        {
            return Ok(_documentService.GetFileName(id));
        }

        [HttpPost]
        public IActionResult DeleteDocumentImplementation(Guid id)
        {
            return Ok(_documentService.DeleteDocument(id));
        }

        [HttpGet]
        public IActionResult GetDocumentContentById(Guid id)
        {
            return Ok(_documentService.GetDocumentContentById(id));
        }

        [HttpPost]
        public IActionResult UpdateDocumentContent(SaveDocumentContentDTO documentContent)
        {
            return Ok(_documentService.UpdateDocumentContent(documentContent));
        }
    }
}
