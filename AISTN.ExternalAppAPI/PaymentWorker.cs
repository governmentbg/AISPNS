
using AISTN.ExternalAppAPI.Services;

namespace AISTN.ExternalAppAPI
{
    public class PaymentWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private double _taskDelayInSeconds;

        public PaymentWorker(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _taskDelayInSeconds = double.Parse(_configuration["ePayment:PaymentCheckerIntervalSeconds"]);
        }



        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    using var scope = _scopeFactory.CreateScope();
                    var checkerService = scope.ServiceProvider.GetService<PaymentCheckerService>();

                    if (checkerService == null) throw new NullReferenceException(nameof(checkerService));

                    await checkerService.checkPaymentsAsync();
                    scope.Dispose();
                    
                    await Task.Delay(TimeSpan.FromSeconds(_taskDelayInSeconds) , stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    //todo_logger.LogError($"Error occurred: {ex.Message}");
                    throw;

                }
            }
        }



    }
}
