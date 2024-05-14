using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class PropertyService : ServiceBase
    {
        private readonly IGenericRepository<Property> _propertyRepository;

        public PropertyService(UserService userService,
                               IHttpContextAccessor contextAccessor,
                               IMapper mapper,
                               ExceptionLogger logger,
                               ExcelGenerator excelGenerator,
                               IGenericRepository<Property> propertyRepository)
                : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _propertyRepository = propertyRepository;
        }

        public OperationResult<PagedList<PropertyIndexDTO>> GetPropertiesByClassKind(int pageNumber, int pageSize, PropertyCaseFilter filter)
        {
            try
            {
                var query = default(IQueryable);
                switch (filter.kind)
                {
                    case PropertyClassKind.Things:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Things, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Patents:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Patents, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Shares:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Shares, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Receivables:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Receivables, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    default:
                        _logger.LogException(new ArgumentOutOfRangeException(nameof(filter.kind), filter.kind, null));
                        return Exception<PagedList<PropertyIndexDTO>>(new Exception("Обекта не можа да бъде взет."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<PropertyIndexDTO>>(ex);
            }
        }

        public OperationResult<DetailsPropertyDTO> GetPropertyById(Guid id)
        {
            try
            {
                var property = _propertyRepository.GetById(id, src => src.Include(x => x.Case)
                                                                     .Include(x => x.Entity)
                                                                     .Include(x => x.Person)
                                                                     .Include(x => x.PropertyClass)
                                                                     .Include(x => x.PropertyKind)
                                                                     .Include(x => x.PropertyType)
                                                                     .Include(x => x.Address));

                return Success(_mapper.Map<DetailsPropertyDTO>(property));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsPropertyDTO>(ex);
            }
        }

        private IQueryable<Property> GetPropertyQueryByKind(Guid kindId, Guid caseId)
        {
            return _propertyRepository.Get(x => (x.PropertyClassId == kindId) && (x.CaseId == caseId),
                                                     source => source.Include(x => x.Case)
                                                                     .Include(x => x.Entity)
                                                                     .Include(x => x.Person)
                                                                     .Include(x => x.PropertyClass)
                                                                     .Include(x => x.PropertyKind)
                                                                     .Include(x => x.PropertyType)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Municipality)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Region)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Settlement))
                                                                  .AsQueryable()
                                                                  .OrderBy(x => x.Id);
        }
    }
}
