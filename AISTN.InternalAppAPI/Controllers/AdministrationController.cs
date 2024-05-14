using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly AdministrationService _administrationService;

        public AdministrationController(AdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPost]
        public IActionResult GetApiRequests(int pageNumber, int pageSize, [FromBody] LogApiRequestSearchFilter filters)
        {
            return Ok(_administrationService.GetApiReguests(pageNumber, pageSize, filters));
        }

        [HttpPost]
        public IActionResult GetApiRequestById(long id)
        {
            return Ok(_administrationService.GetApiReguestById(id));
        }

        [HttpPost]
        public IActionResult GetExceptionById(long id)
        {
            return Ok(_administrationService.GetExceptionById(id));
        }

        [HttpPost]
        public IActionResult GetUserActions(int pageNumber, int pageSize, [FromBody] UserActionSearchFilter filters)
        {
            return Ok(_administrationService.GetUserActions(pageNumber, pageSize, filters));
        }

        [HttpPost]
        public IActionResult GetUserActionById(int id)
        {
            return Ok(_administrationService.GetUserActionById(id));
        }


        [HttpPost]
        public IActionResult GetDBContextLogs(int pageNumber, int pageSize, [FromBody] DBContextLogSearchFilter filters)
        {
            return Ok(_administrationService.GetDBContextLogs(pageNumber, pageSize, filters));
        }

        [HttpPost]
        public IActionResult GetDBContextLogById(int id)
        {
            return Ok(_administrationService.GetDBContextLogById(id));
        }


        [HttpPost]
        public IActionResult GetTemplateArticles28(int pageNumber, int pageSize)
        {
            return Ok(_administrationService.GetTemplateArticles28(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetTemplateArticles28ById(Guid id)
        {
            return Ok(_administrationService.GetTemplateArticles28ById(id));
        }

        [HttpPost]
        public IActionResult CreateTemplateArticle28(SaveTemplateArticles28DTO templateArticles)
        {
            return Ok(_administrationService.CreateTemplateArticle28(templateArticles));
        }

        [HttpPost]
        public IActionResult UpdateTemplateArticle28(SaveTemplateArticles28DTO templateArticles)
        {
            return Ok(_administrationService.UpdateTemplateArticle28(templateArticles));
        }

        [HttpPost]
        public IActionResult DeleteTemplateArticle28(Guid id)
        {
            return Ok(_administrationService.DeleteTemplateArticle28(id));
        }

        [HttpPost]
        public IActionResult GetAdminTemplate(int pageNumber, int pageSize)
        {
            return Ok(_administrationService.GetAdminTemplates(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetAdminTemplateById(Guid id)
        {
            return Ok(_administrationService.GetAdminTemplatesById(id));
        }

        [HttpPost]
        public IActionResult CreateAdminTemplate(SaveAdminTemplateDTO adminTemplate)
        {
            return Ok(_administrationService.CreateAdminTemplate(adminTemplate));
        }

        [HttpPost]
        public IActionResult UpdateAdminTemplate(SaveAdminTemplateDTO adminTemplate)
        {
            return Ok(_administrationService.UpdateAdminTemplate(adminTemplate));
        }

        [HttpPost]
        public IActionResult DeleteAdminTemplate(Guid id)
        {
            return Ok(_administrationService.DeleteAdminTemplate(id));
        }

        [HttpPost]
        public IActionResult GetReportTemplate(int pageNumber, int pageSize)
        {
            return Ok(_administrationService.GetReportTemplates(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetReportTemplateById(Guid id)
        {
            return Ok(_administrationService.GetReportTemplatesById(id));
        }

        [HttpPost]
        public IActionResult CreateReportTemplate(SaveReportTemplateDTO reportTemplate)
        {
            return Ok(_administrationService.CreateReportTemplate(reportTemplate));
        }

        [HttpPost]
        public IActionResult UpdateReportTemplate(SaveReportTemplateDTO reportTemplate)
        {
            return Ok(_administrationService.UpdateReportTemplate(reportTemplate));
        }

        [HttpPost]
        public IActionResult DeleteReportTemplate(Guid id)
        {
            return Ok(_administrationService.DeleteReportTemplate(id));
        }

        [HttpPost]
        public IActionResult GetTemplateDocuments(int pageNumber, int pageSize)
        {
            return Ok(_administrationService.GetTemplateDocuments(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetTemplateDocumentById(Guid id)
        {
            return Ok(_administrationService.GetTemplateDocumentById(id));
        }

        [HttpPost]
        public IActionResult CreateTemplateDocument(SaveTemplateDocumentDTO templateDocument)
        {
            return Ok(_administrationService.CreateTemplateDocument(templateDocument));
        }

        [HttpPost]
        public IActionResult UpdateTemplateDocument(SaveTemplateDocumentDTO templateDocument)
        {
            return Ok(_administrationService.UpdateTemplateDocument(templateDocument));
        }

        [HttpPost]
        public IActionResult DeleteTemplateDocument(Guid id)
        {
            return Ok(_administrationService.DeleteTemplateDocument(id));
        }

        [HttpGet]
        public FileResult DownloadAttachedTemplateDocument(Guid id)
        {
            var attachedTemplateDocument = _administrationService.GetAttachedTemplateDocument(id);

            //return File(attachedTemplateDocument.Blob?.DocumentContent, attachedTemplateDocument?.ContentMimeType, attachedTemplateDocument?.FileName);
            var documentContent = attachedTemplateDocument.DocumentCollection.DocumentContents.FirstOrDefault();
            return File(documentContent.Blob.DocumentContent, documentContent.ContentMimeType, documentContent.FileName);
        }

        #region DocumentLegalBasis

        [HttpGet]
        public IActionResult GetAllDocumentLegalBasis(int pageNumber, int pageSize)
        {
            return Ok(_administrationService.GetAllDocumentLegalBases(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetDocumentLegalBasisById(Guid id)
        {
            return Ok(_administrationService.GetDocumentLegalBasisById(id));
        }

        [HttpPost]
        public IActionResult CreateDocumentLegalBasis(SaveDocumentLegalBasisDTO documentLegalBasisDTO)
        {
            return Ok(_administrationService.CreateDocumentLegalBasis(documentLegalBasisDTO));
        }

        [HttpPost]
        public IActionResult UpdateDocumentLegalBasis(SaveDocumentLegalBasisDTO documentLegalBasisDTO)
        {
            return Ok(_administrationService.UpdateDocumentLegalBasis(documentLegalBasisDTO));
        }

        [HttpPost]
        public IActionResult DeleteDocumentLegalBasis(Guid id)
        {
            return Ok(_administrationService.DeleteDocumentLegalBasis(id));
        }

        #endregion

    }
}
