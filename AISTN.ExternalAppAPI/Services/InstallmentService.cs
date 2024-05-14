using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class InstallmentService : ServiceBase
    {

        private readonly IGenericRepository<Installment> _installmentRepository;
        public InstallmentService(IMapper mapper,
                                  ExceptionLogger logger,
                                  ExcelGenerator excelGenerator,
                                  IGenericRepository<Installment> installmentRepository,
                                  UserService userService,
                                  IHttpContextAccessor contextAccessor) : base(mapper, logger, excelGenerator)
        {
            _installmentRepository = installmentRepository;
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
        }

        public OperationResult<PagedList<InstallmentIndexDTO>> GetSyndicInstallments(int pageNumber, int pageSize)
        {
            try
            {
                var installments = GetSyndicInstallmentsQuery();
                return Success(PagedList<InstallmentIndexDTO>.ToPagedList(installments.ProjectTo<InstallmentIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<InstallmentIndexDTO>>(ex);
            }
        }

        internal OperationResult<SaveInstallmentDTO> GetInstallmentById(Guid id)
        {
            try
            {
                var installment = _installmentRepository.GetById(id, src => src.Include(x => x.DocumentCollection)
                                                                               .Include(x => x.Syndic)
                                                                               .Include(x => x.InstallmentKind)
                                                                               .Include(x => x.VerifiedByNavigation)
                                                                               .Include(x => x.InstallmentYear));

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

        private IQueryable<Installment> GetSyndicInstallmentsQuery()
        {
            return _installmentRepository.Get(x => x.Syndic.UserId == _currentUser.UserId, source => source.Include(x => x.Syndic)
                                                                                           .Include(x => x.InstallmentKind)
                                                                                           .Include(x => x.VerifiedByNavigation)
                                                                                           .Include(x => x.InstallmentYear)).AsQueryable();
        }

        public OperationResult<Guid> CreateInstallment(SaveInstallmentDTO installmentDTO)
        {
            try
            {
                var syndic = _installmentRepository.Get(x => x.Syndic.UserId == _currentUser.UserId, source => source.Include(x => x.Syndic)).FirstOrDefault();
                if(syndic == null) {
                    return Exception<Guid>(new Exception("Няма намерен синдик."));
                }

                installmentDTO.SyndicId = syndic.SyndicId;

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

        public object? UpdateInstallment(SaveInstallmentDTO installmentDTO)
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
    }
}
