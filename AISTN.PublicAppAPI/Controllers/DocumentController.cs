using AISTN.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    [Route("api/PublicApp/[controller]/[action]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;
        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public FileResult DownloadAttachedFile(Guid id)
        {
            var attachedDocument = _documentService.GetAttachedDocuments(id);

            return File(attachedDocument.Blob?.DocumentContent, attachedDocument?.ContentMimeType, attachedDocument?.FileName);
        }
    }
}
