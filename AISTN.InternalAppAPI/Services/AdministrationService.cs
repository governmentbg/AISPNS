using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class AdministrationService : ServiceBase
    {
        private readonly LogAistnContext _logDb;
        private readonly AistnContext _db;
        public AdministrationService(IMapper mapper,
                                     ExceptionLogger logger,
                                     ExcelGenerator excelGenerator,
                                     AistnContext db,
                                     LogAistnContext logDb) : base(mapper, logger, excelGenerator)
        {
            _logDb = logDb;
            _db = db;
        }


        public OperationResult<PagedList<LogApiRequestIndexDTO>> GetApiReguests(int pageNumber, int pageSize, LogApiRequestSearchFilter filters)
        {
            try
            {
                var query = _logDb.LogApiRequests.Where(x => (filters.ResponseHttpCode == null || x.ResponseHttpCode == filters.ResponseHttpCode)
                                                               && (filters.IpAddress == null || x.IpAddress.Contains(filters.IpAddress))
                                                               && (filters.RequestTimestampFrom == null || x.RequestTimestamp >= filters.RequestTimestampFrom)
                                                               && (filters.RequestTimestampTo == null || x.RequestTimestamp <= filters.RequestTimestampTo)
                                                               && (filters.Endpoint == null || x.Endpoint.Contains(filters.Endpoint)))
                                                   .OrderByDescending(x => x.RequestTimestamp)
                                                   .AsQueryable();

                return Success(PagedList<LogApiRequestIndexDTO>.ToPagedList(query.ProjectTo<LogApiRequestIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<LogApiRequestIndexDTO>>(ex);
            }
        }

        public OperationResult<DetailsLogApiRequestDTO> GetApiReguestById(long id)
        {
            try
            {
                var query = _logDb.LogApiRequests.Find(id);
                return Success(_mapper.Map<DetailsLogApiRequestDTO>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsLogApiRequestDTO>(ex);
            }
        }

        public OperationResult<DetailsLogExceptionRequestDTO> GetExceptionById(long id)
        {
            try
            {
                var query = _logDb.LogExceptions.Find(id);
                return Success(_mapper.Map<DetailsLogExceptionRequestDTO>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsLogExceptionRequestDTO>(ex);
            }
        }

        public OperationResult<PagedList<UserActionIndexDTO>> GetUserActions(int pageNumber, int pageSize, UserActionSearchFilter filters)
        {
            var userList = _db.Users;
            var query = _logDb.LogUserActions.Where(x => (filters.UserId == null || x.UserId == filters.UserId)
                                                               && (filters.IpAddress == null || x.IpAddress.Contains(filters.IpAddress))
                                                               && (filters.UserAction == null || x.UserActionType == filters.UserAction)
                                                               && (filters.TimestampFrom == null || x.Timestamp >= filters.TimestampFrom)
                                                               && (filters.TimestampTo == null || x.Timestamp <= filters.TimestampTo))
                                                                                                               .OrderByDescending(x => x.Timestamp)
                                                                                                               .AsQueryable();
            var pagedList = PagedList<UserActionIndexDTO>.ToPagedList(query.ProjectTo<UserActionIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize);

            foreach (var item in pagedList.Items)
            {
                item.UserName = userList.FirstOrDefault(x => x.Id == item.UserId) != null ? userList.FirstOrDefault(x => x.Id == item.UserId).UserName : null;
            }

            return Success(pagedList);
        }


        public OperationResult<DetailsUserActionDTO> GetUserActionById(int id)
        {
            try
            {
                var query = _logDb.LogUserActions.Find(id);
                var userList = _db.Users;

                var mappedUserAction = _mapper.Map<DetailsUserActionDTO>(query);
                mappedUserAction.UserName = userList.FirstOrDefault(x => x.Id == mappedUserAction.UserId) != null ? userList.FirstOrDefault(x => x.Id == mappedUserAction.UserId).UserName : null;

                return Success(mappedUserAction);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsUserActionDTO>(ex);
            }
        }

        public OperationResult<PagedList<LogDbcontextIndexDTO>> GetDBContextLogs(int pageNumber, int pageSize, DBContextLogSearchFilter filters)
        {
            try
            {
                var query = _logDb.LogDbcontexts.AsQueryable();
                return Success(PagedList<LogDbcontextIndexDTO>.ToPagedList(query.ProjectTo<LogDbcontextIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<LogDbcontextIndexDTO>>(ex);
            }
        }

        public OperationResult<DetailsLogDbcontexDTO> GetDBContextLogById(int id)
        {
            try
            {
                var query = _logDb.LogDbcontexts.Find(id);
                return Success(_mapper.Map<DetailsLogDbcontexDTO>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsLogDbcontexDTO>(ex);
            }
        }

        public OperationResult<PagedList<IndexTemplateArticles28DTO>> GetTemplateArticles28(int pageNumber, int pageSize)
        {
            try
            {
                var query = _db.TemplateArticle28s.Include(x => x.DirectiveTemplateKind)
                                                  .Include(x => x.DocumentCollection)
                                                  .ThenInclude(x => x.DocumentContents)
                                                  .Include(x => x.DocumentCollection)
                                                  .ThenInclude(x => x.DocumentContents)
                                                  .ThenInclude(x => x.Blob)
                                                  .AsQueryable();

                return Success(PagedList<IndexTemplateArticles28DTO>.ToPagedList(query.ProjectTo<IndexTemplateArticles28DTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<IndexTemplateArticles28DTO>>(ex);
            }
        }

        public OperationResult<SaveTemplateArticles28DTO> GetTemplateArticles28ById(Guid id)
        {
            try
            {
                var query = _db.TemplateArticle28s.Include(x => x.DirectiveTemplateKind)
                                                  .Include(x => x.DocumentCollection)
                                                  .ThenInclude(x => x.DocumentContents)
                                                  .Include(x => x.DocumentCollection)
                                                  .ThenInclude(x => x.DocumentContents)
                                                  .ThenInclude(x => x.Blob)
                                                  .FirstOrDefault(x => x.Id == id);

                return Success(_mapper.Map<SaveTemplateArticles28DTO>(query));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<SaveTemplateArticles28DTO>(ex);
            }
        }

        public OperationResult<Guid> CreateTemplateArticle28(SaveTemplateArticles28DTO templateArticles)
        {
            try
            {
                var mappedTemplateArticle = _mapper.Map<TemplateArticle28>(templateArticles);

                _db.TemplateArticle28s.Add(mappedTemplateArticle);
                _db.SaveChanges();

                return Success<Guid>(mappedTemplateArticle.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }


        public OperationResult<Guid> UpdateTemplateArticle28(SaveTemplateArticles28DTO templateArticles)
        {
            try
            {
                var mappedTemplateArticle = _mapper.Map<TemplateArticle28>(templateArticles);

                _db.TemplateArticle28s.Update(mappedTemplateArticle);
                _db.SaveChanges();

                return Success<Guid>(mappedTemplateArticle.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<bool> DeleteTemplateArticle28(Guid id)
        {
            try
            {
                var templateArticle = _db.TemplateArticle28s.Find(id);

                if (templateArticle == null)
                {
                    return Exception<bool>(new Exception("Няма намерен образец."));
                }


                _db.TemplateArticle28s.Remove(templateArticle);
                _db.SaveChanges();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<PagedList<AdminTemplateIndexDTO>> GetAdminTemplates(int pageNumber, int pageSize)
        {
            try
            {
                var query = _db.AdminTemplates.Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .Include(x => x.TemplateKind)
                                              .Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .ThenInclude(x => x.Blob)
                                              .AsQueryable();

                return Success(PagedList<AdminTemplateIndexDTO>.ToPagedList(query.ProjectTo<AdminTemplateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<AdminTemplateIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveAdminTemplateDTO> GetAdminTemplatesById(Guid id)
        {
            try
            {
                var query = _db.AdminTemplates.Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .ThenInclude(x => x.Blob)
                                              .FirstOrDefault(x => x.Id == id);

                return Success(_mapper.Map<SaveAdminTemplateDTO>(query));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<SaveAdminTemplateDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateAdminTemplate(SaveAdminTemplateDTO adminTemplate)
        {
            try
            {
                var mappedAdminTemplate = _mapper.Map<AdminTemplate>(adminTemplate);

                _db.AdminTemplates.Add(mappedAdminTemplate);
                _db.SaveChanges();

                return Success<Guid>(mappedAdminTemplate.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }


        public OperationResult<Guid> UpdateAdminTemplate(SaveAdminTemplateDTO adminTemplate)
        {
            try
            {
                var mappedAdminTemplate = _mapper.Map<AdminTemplate>(adminTemplate);

                _db.AdminTemplates.Update(mappedAdminTemplate);
                _db.SaveChanges();

                return Success<Guid>(mappedAdminTemplate.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<bool> DeleteAdminTemplate(Guid id)
        {
            try
            {
                var adminTemplate = _db.AdminTemplates.Find(id);

                if (adminTemplate == null)
                {
                    return Exception<bool>(new Exception("Няма намерен образец."));
                }


                _db.AdminTemplates.Remove(adminTemplate);
                _db.SaveChanges();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<PagedList<ReportTemplateIndexDTO>> GetReportTemplates(int pageNumber, int pageSize)
        {
            try
            {
                var query = _db.ReportTemplates.Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .Include(x => x.ReportType)
                                              .Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .ThenInclude(x => x.Blob)
                                              .AsQueryable();

                return Success(PagedList<ReportTemplateIndexDTO>.ToPagedList(query.ProjectTo<ReportTemplateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<ReportTemplateIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveReportTemplateDTO> GetReportTemplatesById(Guid id)
        {
            try
            {
                var query = _db.ReportTemplates.Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .Include(x => x.DocumentCollection)
                                              .ThenInclude(x => x.DocumentContents)
                                              .ThenInclude(x => x.Blob)
                                              .FirstOrDefault(x => x.Id == id);

                return Success(_mapper.Map<SaveReportTemplateDTO>(query));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<SaveReportTemplateDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateReportTemplate(SaveReportTemplateDTO reportTemplate)
        {
            try
            {
                var mappedReportTemplate = _mapper.Map<ReportTemplate>(reportTemplate);

                _db.ReportTemplates.Add(mappedReportTemplate);
                _db.SaveChanges();

                return Success<Guid>(mappedReportTemplate.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }


        public OperationResult<Guid> UpdateReportTemplate(SaveReportTemplateDTO adminTemplate)
        {
            try
            {
                var mappedReportTemplate = _mapper.Map<ReportTemplate>(adminTemplate);

                _db.ReportTemplates.Update(mappedReportTemplate);
                _db.SaveChanges();

                return Success<Guid>(mappedReportTemplate.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<bool> DeleteReportTemplate(Guid id)
        {
            try
            {
                var reportTemplate = _db.ReportTemplates.Find(id);

                if (reportTemplate == null)
                {
                    return Exception<bool>(new Exception("Няма намерен образец."));
                }


                _db.ReportTemplates.Remove(reportTemplate);
                _db.SaveChanges();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<PagedList<TemplateDocumentIndexDTO>> GetTemplateDocuments(int pageNumber, int pageSize)
        {
            try
            {
                var query = _db.TemplateDocuments.AsQueryable();

                return Success(PagedList<TemplateDocumentIndexDTO>.ToPagedList(query.ProjectTo<TemplateDocumentIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<TemplateDocumentIndexDTO>>(ex);
            }
        }

        public OperationResult<TemplateDocumentIndexDTO> GetTemplateDocumentById(Guid id)
        {
            try
            {
                var template = _db.TemplateDocuments.FirstOrDefault(x => x.Id == id);

                if (template == null)
                {
                    return Exception<TemplateDocumentIndexDTO>(new Exception("Няма намерен шаблон"));
                }

                return Success(_mapper.Map<TemplateDocumentIndexDTO>(template));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<TemplateDocumentIndexDTO>(ex);
            }
        }

        public OperationResult<SaveTemplateDocumentDTO> CreateTemplateDocument(SaveTemplateDocumentDTO templateDocument)
        {
            try
            {
                var templateDocumentTypes = _db.TemplateDocuments.Select(x => x.Type);
                if (templateDocumentTypes.Contains(templateDocument.Type))
                {
                    return Exception<SaveTemplateDocumentDTO>(new Exception("Вече съществува шаблон от този тип"));
                }
                var mappedTemplateDocument = _mapper.Map<TemplateDocument>(templateDocument);

                _db.TemplateDocuments.Add(mappedTemplateDocument);
                _db.SaveChanges();

                return Success(_mapper.Map<SaveTemplateDocumentDTO>(mappedTemplateDocument));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveTemplateDocumentDTO>(ex);
            }
        }

        public OperationResult<Guid> UpdateTemplateDocument(SaveTemplateDocumentDTO templateDocument)
        {
            try
            {
                var template = _db.TemplateDocuments.FirstOrDefault(x => x.Id == templateDocument.Id);

                if (template == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен шаблон"));
                }

                _mapper.Map(templateDocument, template);

                _db.TemplateDocuments.Update(template);
                _db.SaveChanges();

                return Success(template.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> DeleteTemplateDocument(Guid id)
        {
            try
            {
                var templateDocument = _db.TemplateDocuments.FirstOrDefault(x => x.Id == id);

                if (templateDocument == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен шаблон"));
                }

                _db.TemplateDocuments.Remove(templateDocument);
                _db.SaveChanges();

                return Success(templateDocument.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public TemplateDocument GetAttachedTemplateDocument(Guid id)
        {
            try
            {
                var templateDocument = _db.TemplateDocuments.Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.Blob)
                                        .FirstOrDefault(x => x.Id == id);
                return templateDocument!;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        #region DocumentLegalBasis

        public OperationResult<PagedList<DocumentLegalBasisIndexDTO>> GetAllDocumentLegalBases(int pageNumber, int pageSize)
        {
            try
            {
                var query = _db.DocumentLegalBases.Include(x => x.DocumentCollection)
                                                    .ThenInclude(x => x.DocumentContents)
                                                  .AsQueryable();
                return Success(PagedList<DocumentLegalBasisIndexDTO>.ToPagedList(query.ProjectTo<DocumentLegalBasisIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<DocumentLegalBasisIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveDocumentLegalBasisDTO> GetDocumentLegalBasisById(Guid id)
        {
            try
            {
                var documentLegalBasis = _db.DocumentLegalBases.FirstOrDefault(x => x.Id == id);

                if (documentLegalBasis == null)
                {
                    return Exception<SaveDocumentLegalBasisDTO>(new Exception("Няма намерена нормативна уредба."));
                }

                return Success(_mapper.Map<SaveDocumentLegalBasisDTO>(documentLegalBasis));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveDocumentLegalBasisDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateDocumentLegalBasis(SaveDocumentLegalBasisDTO documentLegalBasisDTO)
        {
            try
            {
                var documentLegalBasis = _mapper.Map<DocumentLegalBasis>(documentLegalBasisDTO);

                _db.DocumentLegalBases.Add(documentLegalBasis);
                _db.SaveChanges();

                return Success(documentLegalBasis.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveDocumentLegalBasisDTO> UpdateDocumentLegalBasis(SaveDocumentLegalBasisDTO documentLegalBasisDTO)
        {
            try
            {
                var documentLegalBasis = _db.DocumentLegalBases.FirstOrDefault(x => x.Id == documentLegalBasisDTO.Id);

                if (documentLegalBasis == null)
                {
                    return Exception<SaveDocumentLegalBasisDTO>(new Exception("Не беше намерена нормативна уредба."));
                }

                var mappedDocumentLegalBasis = _mapper.Map(documentLegalBasisDTO, documentLegalBasis);

                _db.DocumentLegalBases.Update(mappedDocumentLegalBasis);
                _db.SaveChanges();

                return Success(_mapper.Map<SaveDocumentLegalBasisDTO>(documentLegalBasis));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveDocumentLegalBasisDTO>(ex);
            }
        }

        public OperationResult<Guid> DeleteDocumentLegalBasis(Guid id)
        {
            try
            {
                var documentLegalBasis = _db.DocumentLegalBases.FirstOrDefault(x => x.Id == id);

                if (documentLegalBasis == null)
                {
                    return Exception<Guid>(new Exception("Няма намерена нормативна уредба."));
                }

                _db.DocumentLegalBases.Remove(documentLegalBasis);
                _db.SaveChanges();

                return Success(documentLegalBasis.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        #endregion
    }
}