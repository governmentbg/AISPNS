
using AISTN.CommercialRegIntegrator.Helpers;
using AISTN.CommercialRegIntegrator.Services;
using Microsoft.Extensions.Options;

namespace AISTN.CommercialRegIntegrator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly FolderSettings _folderSettings;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, IOptions<FolderSettings> FolderSettings)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _folderSettings = FolderSettings.Value;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            int cycleCount = 0; 

            var directoryPath = _folderSettings.Pending;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                   
                    await ProcessDirectoryFilesAsync(directoryPath, stoppingToken);

                    if (cycleCount % 2 == 0)
                    {
                        await ConnectEntitiesAsync(stoppingToken);
                        cycleCount = 0; // Reset the counter after calling ConnectEntitiesAsync
                    }
                                     
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred: {ex.Message}");

                }
            }


        }

        private async Task ProcessDirectoryFilesAsync(string directoryPath, CancellationToken stoppingToken)
        {
            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                _logger.LogError($"Directory not found: {directoryPath}");
                return;
            }

            // Get all files in the directory
            var fileEntries = Directory.GetFiles(directoryPath);
            foreach (var filePath in fileEntries)
            {
                // Check for cancellation
                stoppingToken.ThrowIfCancellationRequested();

                await ProcessFileAsync(filePath, stoppingToken);

            }
        }

        private async Task ConnectEntitiesAsync( CancellationToken stoppingToken)
        {
               
            stoppingToken.ThrowIfCancellationRequested();

            using var scope = _scopeFactory.CreateScope();
            var importerService = scope.ServiceProvider.GetService<ICommercialRegisterImporterService>();
            _logger.LogInformation($"Connect entities");
            await importerService.ConnectEntitiesAsync();
        }


        private async Task ProcessFileAsync(string filePath, CancellationToken stoppingToken)
        {
            try
            {

                _logger.LogInformation($"File created: {filePath}");
                string newFilePath;

                if (IsFileClosed(filePath, true))
                {

                    newFilePath = FileHelper.MoveFile(filePath, _folderSettings.InProgress);
                    _logger.LogInformation($"File moved: {newFilePath}");

                    using var scope = _scopeFactory.CreateScope();
                    var importerService = scope.ServiceProvider.GetService<ICommercialRegisterImporterService>();

                    if (importerService == null) throw new NullReferenceException(nameof(importerService));

                    await importerService.ImportDeedAsync(newFilePath);
                   


                    _logger.LogInformation("File with name " + newFilePath + "  imported");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("File with name " + filePath + "  raised error:" + ex.GetBaseException().Message);
            }
        }
        

        public override void Dispose()
        {
            //_watcher.Dispose();
            base.Dispose();
        }

        internal static bool IsFileClosed(string filepath, bool wait)
        {
            bool fileClosed = false;
            int retries = 20;
            const int delay = 500; // Max time spent here = retries*delay milliseconds

            if (!File.Exists(filepath))
                return false;
            do
            {
                try
                {
                    // Attempts to open then close the file in RW mode, denying other users to place any locks.
                    FileStream fs = File.Open(filepath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    fs.Close();
                    fileClosed = true; // success
                }
                catch (IOException) { }

                if (!wait) break;

                retries--;

                if (!fileClosed)
                    Thread.Sleep(delay);
            }
            while (!fileClosed && retries > 0);

            return fileClosed;
        }

    }
}
