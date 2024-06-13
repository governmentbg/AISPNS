using AISTN.Common.Helper;
using AISTN.Common.Models;
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
    public class InstallmentService : ServiceBase
    {
        private readonly IGenericRepository<Installment> _installmentRepository;

        public InstallmentService(IMapper mapper, ExceptionLogger logger,
                                  IGenericRepository<Installment> installmentRepository,
                                  UserService userService,
                                  IHttpContextAccessor contextAccessor,
                                  ExcelGenerator excelGenerator)
                    : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _installmentRepository = installmentRepository;
        }

        public OperationResult<PagedList<InstallmentIndexDTO>> SearchInstallment(InstallmentSearchFilter filter, int pageNumber, int pageSize)
        {
            try
            {
                var query = GetInstallmentQuery(filter);
                return Success(PagedList<InstallmentIndexDTO>.ToPagedList(query.ProjectTo<InstallmentIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<InstallmentIndexDTO>>(ex);
            }
        }

        public byte[] ExportSearchInstallmentToExcel(InstallmentSearchFilter filter)
        {
            try
            {
                var query = GetInstallmentQuery(filter);
                return _excelGenerator.ExportGridToExcelXlsxFile(_mapper.Map<List<InstallmentIndexDTO>>(query));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        private IQueryable<Installment> GetInstallmentQuery(InstallmentSearchFilter filter)
        {
            return _installmentRepository.Get(filter: x => ((filter.InstallementKind == null) || x.InstallmentKind.Description.Contains(filter.InstallementKind))
                                                      && ((filter.PaymentDate == default) || x.PaymentDate == filter.PaymentDate)
                                                      && ((filter.Bank == null) || x.Bank.Contains(filter.Bank))
                                                      && ((filter.Amount == null) || x.Amount == filter.Amount),
                                        include: source => source.Include(x => x.Syndic)
                                                                 .Include(x => x.InstallmentKind)
                                                                 .Include(x => x.InstallmentYear!))
                                    .AsQueryable().OrderBy(x => x.DateCreated);
        }

        public OperationResult<SaveInstallmentDTO> GetInstallmentById(Guid id)
        {
            try
            {
                var installment = _installmentRepository.GetById(id, src => src.Include(x => x.DocumentCollection)
                                                                               .Include(x => x.Syndic)
                                                                               .Include(x => x.InstallmentKind)
                                                                               .Include(x => x.VerifiedByNavigation)
                                                                               .Include(x => x.InstallmentYear!));

                if (installment == null)
                {
                    return Exception<SaveInstallmentDTO>(new Exception("Няма намерена вноска."));
                }

                return Success(_mapper.Map<SaveInstallmentDTO>(installment));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveInstallmentDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateInstallment(SaveInstallmentDTO installmentDTO)
        {
            try
            {
                if (installmentDTO.Verified == true)
                {
                    installmentDTO.VerifiedBy = _currentUser.UserId;
                }

                var installmentEntity = _mapper.Map<Installment>(installmentDTO);

                _installmentRepository.Add(installmentEntity);
                _installmentRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateInstallment));

                return Success(installmentEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveInstallmentDTO> UpdateInstallment(SaveInstallmentDTO installmentDTO)
        {
            try
            {
                var installmentEntity = _installmentRepository.GetById(installmentDTO.Id.Value, source => source.Include(x => x.VerifiedByNavigation));

                if (installmentEntity == null)
                {
                    return Exception<SaveInstallmentDTO>(new Exception("Няма намерена вноска."));
                }

                if (installmentDTO.Verified == true && !installmentEntity.VerifiedBy.HasValue)
                {
                    installmentDTO.VerifiedBy = _currentUser.UserId;
                    _mapper.Map(installmentDTO, installmentEntity);
                    _installmentRepository.Update(installmentEntity);
                    _installmentRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateInstallment));
                    installmentEntity = _installmentRepository.GetById(installmentDTO.Id.Value, source => source.Include(x => x.VerifiedByNavigation));
                }
                else
                {
                    _mapper.Map(installmentDTO, installmentEntity);
                    _installmentRepository.Update(installmentEntity);
                    _installmentRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateInstallment));
                }

                return Success(_mapper.Map<SaveInstallmentDTO>(installmentEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveInstallmentDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteInstallment(Guid id)
        {
            try
            {
                var installmentEntity = _installmentRepository.GetById(id);

                if (installmentEntity == null)
                {
                    return Exception<bool>(new Exception("Няма намерена вноска."));
                }

                _installmentRepository.Remove(id);
                _installmentRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteInstallment));

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
