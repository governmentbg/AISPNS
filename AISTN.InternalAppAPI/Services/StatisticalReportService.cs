using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class StatisticalReportService : ServiceBase
    {
        private readonly IGenericRepository<StatisticalReport> _statisticalReportRepo;
        private readonly DocumentService _documentService;

        public StatisticalReportService(IMapper mapper, ExceptionLogger logger,
                                        IGenericRepository<StatisticalReport> statisticalReportRepo,
                                        UserService userService,
                                        IHttpContextAccessor contextAccessor,
                                        DocumentService documentService,
                                        ExcelGenerator excelGenerator)
                    : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _statisticalReportRepo = statisticalReportRepo;
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
            return _statisticalReportRepo.Get(filter: x => ((filter.Type == null) || x.TypeNavigation.Id == filter.Type)
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
                var statReports = _statisticalReportRepo.GetAll(x => x.Include(x => x.TypeNavigation!)
                                                                      .Include(x => x.ReportSource!));

                if (statReports.Count() < 0)
                {
                    return Exception<IEnumerable<StatisticalReportIndexDTO>>(new Exception("Няма намерен отчет."));
                }

                return Success(_mapper.Map<IEnumerable<StatisticalReportIndexDTO>>(statReports));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<IEnumerable<StatisticalReportIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveStatisticalReportDTO> GetStatisticalReportById(Guid id)
        {
            try
            {
                var statReport = _statisticalReportRepo.GetById(id, src => src.Include(x => x.TypeNavigation!));

                if (statReport == null)
                {
                    return Exception<SaveStatisticalReportDTO>(new Exception("Няма намерен отчет."));
                }

                return Success(_mapper.Map<SaveStatisticalReportDTO>(statReport));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveStatisticalReportDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateStatisticalReport(SaveStatisticalReportDTO statReportDTO)
        {
            try
            {
                var statReportEntity = _mapper.Map<StatisticalReport>(statReportDTO);
                _statisticalReportRepo.Add(statReportEntity);

                _statisticalReportRepo.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateStatisticalReport));

                return Success(statReportEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveStatisticalReportDTO> UpdateStatisticalReport(SaveStatisticalReportDTO statReportDTO)
        {
            try
            {
                var statReportEntity = _statisticalReportRepo.GetById(statReportDTO.Id.Value);

                if (statReportEntity == null)
                {
                    return Exception<SaveStatisticalReportDTO>(new Exception("Няма намерен отчет."));
                }

                var mappedStatisticalReport = _mapper.Map<StatisticalReport>(statReportDTO);

                _statisticalReportRepo.Update(mappedStatisticalReport);
                _statisticalReportRepo.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateStatisticalReport));

                return Success(_mapper.Map<SaveStatisticalReportDTO>(mappedStatisticalReport));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveStatisticalReportDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteStatisticalReport(Guid id)
        {
            try
            {
                var statReportEntity = _statisticalReportRepo.GetById(id);

                if (statReportEntity == null)
                {
                    return Exception<bool>(new Exception("Няма намерен отчет."));
                }

                _statisticalReportRepo.Remove(statReportEntity.Id);
                _statisticalReportRepo.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteStatisticalReport));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<Guid> PublishStatisticalReport(Guid id)
        {
            try
            {
                var statReport = _statisticalReportRepo.GetById(id);

                statReport.IsPublished = true;
                statReport.Published = DateTime.Now;

                _statisticalReportRepo.Update(statReport);
                _statisticalReportRepo.Save(CreateUserActivity(_currentUser!, eUserActionType.PublishStatisticalReport));

                return Success(statReport.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> UnpublishStatisticalReport(Guid id)
        {
            try
            {
                var statReport = _statisticalReportRepo.GetById(id);

                statReport.IsPublished = false;
                statReport.Published = null;

                _statisticalReportRepo.Update(statReport);
                _statisticalReportRepo.Save(CreateUserActivity(_currentUser!, eUserActionType.UnpublishStatisticalReport));

                return Success(statReport.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }
    }
}
