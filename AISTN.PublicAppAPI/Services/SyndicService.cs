
using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.PublicAppAPI.Models.Details;
using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.PublicAppAPI.Services
{
    [Injectable]
    public class SyndicService : ServiceBase
    {
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly IGenericRepository<TemplateArticle28> _template28Repository;
        private readonly IGenericRepository<DocumentLegalBasis> _documentLegalBasisRepo;
        private readonly DocumentService _documentService;

        public SyndicService(IMapper mapper,
                             ExceptionLogger logger,
                             IGenericRepository<Syndic> syndicRepository,
                             IGenericRepository<TemplateArticle28> template28Repository,
                             DocumentService documentService,
                             ExcelGenerator excelGenerator,
                             IGenericRepository<DocumentLegalBasis> documentLegalBasisRepo) : base(mapper, logger, excelGenerator)
        {
            _syndicRepository = syndicRepository;
            _template28Repository = template28Repository;
            _documentService = documentService;
            _documentLegalBasisRepo = documentLegalBasisRepo;
        }

        public OperationResult<PagedList<SyndicIndexDTO>> SearchSyndic(int pageNumber, int pageSize, SyndicSearchFilter filter)
        {
            try
            {
                var query = GetSyndicQuery(filter);
                return Success(PagedList<SyndicIndexDTO>.ToPagedList(query.ProjectTo<SyndicIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<SyndicIndexDTO>>(ex);
            }
        }

        private IQueryable<Syndic> GetSyndicQuery(SyndicSearchFilter filter)
        {
            return _syndicRepository.Get(filter: x => ((filter.FirstName == null) || x.FirstName.Contains(filter.FirstName))
                                                      && ((filter.LastName == null) || x.LastName.Contains(filter.LastName))
                                                      && ((filter.City == null) || x.Syndic2Addresses.Any(x => x.Address.Settlement.Name.Contains(filter.City)))
                                                      && ((filter.Speciality == null) || x.Specialty == filter.Speciality)
                                                      && ((filter.IsCustodian == null) || x.IsCustodian == filter.IsCustodian)
                                                      && (x.Active == true),
                                        include: source => source.Include(x => x.SpecialtyNavigation)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                    .ThenInclude(x => x.Address!)
                                                                    .ThenInclude(x => x.Region)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                    .ThenInclude(x => x.Address!)
                                                                    .ThenInclude(x => x.Settlement)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                    .ThenInclude(x => x.Address!)
                                                                    .ThenInclude(x => x.Municipality!))
                                                                 .AsQueryable()
                                                                 .OrderBy(x => x.FirstName);
        }

        public OperationResult<DetailsSyndicDTO> GetSyndicById(Guid id)
        {
            try
            {
                var syndic = _syndicRepository.GetById(id, include: source => source.Include(x => x.SpecialtyNavigation)
                                                                                    .Include(x => x.Orders)
                                                                                    .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Region)
                                                                                     .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Settlement)
                                                                                     .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Municipality!));

                return Success(_mapper.Map<DetailsSyndicDTO>(syndic));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsSyndicDTO>(ex);
            }
        }

        public OperationResult<PagedList<IndexTemplateArticles28DTO>> GetSyndicTemplates(int pageNumber, int pageSize)
        {
            try
            {
                var query = _template28Repository.Get(x => x.IsPublished == true, include: source => source.Include(x => x.DirectiveTemplateKind)
                                                                               .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents))
                                                                               .AsQueryable();

                return Success(PagedList<IndexTemplateArticles28DTO>.ToPagedList(query.ProjectTo<IndexTemplateArticles28DTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<IndexTemplateArticles28DTO>>(ex);
            }
        }

        public OperationResult<IndexTemplateArticles28DTO> GetSyndicTemplateById(Guid id)
        {
            try
            {
                var template = _template28Repository.GetById(id, x => x.Include(x => x.DirectiveTemplateKind!));
                return Success(_mapper.Map<IndexTemplateArticles28DTO>(template));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<IndexTemplateArticles28DTO>(ex);
            }
        }

        public OperationResult<PagedList<DocumentLegalBasisIndexDTO>> GetAllDocumentLegalBases(int pageNumber, int pageSize)
        {
            try
            {
                var query = _documentLegalBasisRepo.GetAll(include: src => src.Include(x => x.DocumentCollection)
                                                                                .ThenInclude(x => x.DocumentContents))
                                                                              .AsQueryable();
                return Success(PagedList<DocumentLegalBasisIndexDTO>.ToPagedList(query.ProjectTo<DocumentLegalBasisIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<DocumentLegalBasisIndexDTO>>(ex);
            }
        }
    }
}
