using AISTN.Common.Services;

namespace AISTN.ExternalAppAPI
{
    public class EmailWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EmailWorker> _logger;

        public EmailWorker(IServiceScopeFactory scopeFactory, ILogger<EmailWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var emailService = scope.ServiceProvider.GetService<EmailService>();

                    if (emailService == null) throw new NullReferenceException(nameof(emailService));

                    emailService.SendEmail();

                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogInformation(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    throw;
                }
            }
        }
    }
}
