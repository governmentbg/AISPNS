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
    public class StatisticalReportService : ServiceBase
    {
        private readonly IGenericRepository<StatisticalReport> _statisticalReportRepopository;
        private readonly IGenericRepository<DocumentContent> _documentContentReportRepository;
        private readonly DocumentService _documentService;

        public StatisticalReportService(IMapper mapper, ExceptionLogger logger,
                                        IGenericRepository<StatisticalReport> statisticalReportRepository,
                                        IGenericRepository<DocumentContent> documentContentReportRepository,
                                        DocumentService documentService,
                                        ExcelGenerator excelGenerator)
                    : base(mapper, logger, excelGenerator)
        {
            _statisticalReportRepopository = statisticalReportRepository;
            _documentContentReportRepository = documentContentReportRepository;
            _documentService = documentService;
        }

        public OperationResult<PagedList<StatisticalReportIndexDTO>> SearchStatisticalReport(int pageNumber, int pageSize, StatisticalReportSearchFilter filter)
        {
            try
            {
                var query = GetStatisticalReportQuery(filter);
                return Success(PagedList<StatisticalReportIndexDTO>
                    .ToPagedList(query.ProjectTo<StatisticalReportIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<StatisticalReportIndexDTO>>(ex);
            }
        }

        private IQueryable<StatisticalReport> GetStatisticalReportQuery(StatisticalReportSearchFilter filter)
        {
            return _statisticalReportRepopository.Get(filter: x => x.IsPublished == true
                                                      && ((filter.Type == null) || x.TypeNavigation.Id == filter.Type)
                                                      && ((filter.Source == null) || x.ReportSource.Id == filter.Source)
                                                      && ((filter.FromDate == null) || x.FromDate >= filter.FromDate)
                                                      && ((filter.ToDate == null) || x.ToDate <= filter.ToDate),
                                        include: source => source.Include(x => x.ReportSource)
                                                                 .Include(x => x.TypeNavigation)
                                                                 .Include(x => x.DocumentCollection!))
                                    .AsQueryable().OrderBy(x => x.DateCreated);
        }

        public OperationResult<IEnumerable<StatisticalReportIndexDTO>> GetAllStatisticalReports()
        {
            try
            {
                var statReports = _statisticalReportRepopository.GetAll(x => x.Where(x => x.Published != null)
                                                                      .Include(x => x.TypeNavigation)
                                                                      .Include(x => x.ReportSource)
                                                                      .Include(x => x.DocumentCollection!));

                if (statReports.Count() < 0)
                {
                    return Exception<IEnumerable<StatisticalReportIndexDTO>>(new Exception("Няма намерени отчети."));
                }

                return Success(_mapper.Map<IEnumerable<StatisticalReportIndexDTO>>(statReports));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<IEnumerable<StatisticalReportIndexDTO>>(ex);
            }
        }

        public OperationResult<DetailsStatisticalReportDTO> GetStatisticalReportById(Guid id)
        {
            try
            {
                var statReport = _statisticalReportRepopository.GetById(id, src => src.Where(x => x.Published != null)
                                                                                      .Include(x => x.TypeNavigation)
                                                                                      .Include(x => x.ReportSource)
                                                                                      .Include(x => x.DocumentCollection)
                                                                                      .ThenInclude(x => x.DocumentContents)
                                                                                      .ThenInclude(x => x.AttachedDocumentKind!)
                                                                                      .Include(x => x.DocumentCollection)
                                                                                      .ThenInclude(x => x.DocumentContents)
                                                                                      .ThenInclude(x => x.Blob));

                if (statReport == null)
                {
                    return Exception<DetailsStatisticalReportDTO>(new Exception("Няма намерен отчет."));
                }

                var test = _mapper.Map<DetailsStatisticalReportDTO>(statReport);
                return Success(test);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsStatisticalReportDTO>(ex);
            }
        }
    }
}
