using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
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
    public class OrderService : ServiceBase
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IMapper mapper, ExceptionLogger logger,
                            IGenericRepository<Order> orderRepository,
                            UserService userService,
                            IHttpContextAccessor contextAccessor,
                            ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _orderRepository = orderRepository;
        }

        public OperationResult<PagedList<OrderIndexDTO>> SearchOrder(int pageNumber, int pageSize, OrderSearchFilter filter)
        {
            try
            {
                var query = GetOrderQuery(filter);
                return Success(PagedList<OrderIndexDTO>.ToPagedList(query.ProjectTo<OrderIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<OrderIndexDTO>>(ex);
            }
        }

        private IQueryable<Order> GetOrderQuery(OrderSearchFilter filter)
        {
            return _orderRepository.Get(filter: x => ((filter.Number == null) || x.Number.Contains(filter.Number))
                                                      && ((filter.Date == default) || x.Date == filter.Date)
                                                      && ((filter.StateGazetteYear == null) || x.StateGazetteYear.Contains(filter.StateGazetteYear)),
                                        include: source => source.Include(x => x.Syndic!)
                                                                 .Include(x => x.OrderKind))
                                    .AsQueryable().OrderBy(x => x.Number);
        }

        public OperationResult<SaveOrderDTO> GetOrderById(Guid id)
        {
            try
            {
                var order = _orderRepository.GetById(id, src => src.Include(x => x.Syndic));
                return Success(_mapper.Map<SaveOrderDTO>(order));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveOrderDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateOrder(SaveOrderDTO orderDto)
        {
            try
            {
                var orderEntity = _mapper.Map<Order>(orderDto);

                _orderRepository.Add(orderEntity);
                _orderRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateOrder));

                return Success(orderEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveOrderDTO> UpdateOrder(SaveOrderDTO orderDto)
        {
            try
            {
                var mappedOrder = _mapper.Map<Order>(orderDto);

                var orderEntity = _orderRepository.GetById(mappedOrder.Id);

                if (orderEntity == null)
                {
                    return Exception<SaveOrderDTO>(new Exception("Няма намерена заповед."));
                }

                _mapper.Map(orderDto, orderEntity);

                _orderRepository.Update(orderEntity);
                _orderRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateOrder));

                return Success(_mapper.Map<SaveOrderDTO>(orderEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveOrderDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteOrder(Guid id)
        {
            try
            {
                var orderEntity = _orderRepository.GetById(id);

                if (orderEntity == null)
                {
                    return Exception<bool>(new Exception("Няма намерена заповед."));
                }

                _orderRepository.Remove(orderEntity.Id);
                _orderRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteOrder));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
    }
}
