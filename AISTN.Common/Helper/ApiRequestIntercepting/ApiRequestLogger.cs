using AISTN.Data.LogDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Linq;

namespace AISTN.Common.Helper.ApiRequestIntercepting
{
    public class ApiRequestLogger
    {
        protected readonly IDbContextFactory<LogAistnContext> _logDbFactory;
        private ProcessingSettings _processingSettings;

        public ApiRequestLogger(IDbContextFactory<LogAistnContext> logDb, IOptions<ProcessingSettings> processingSettings)
        {
            _logDbFactory = logDb;
            _processingSettings = processingSettings.Value;
        }

        public async Task<long> LogRequest(string requestContent, string endpoint, string ipAddress)
        {
            var _logDb = await _logDbFactory.CreateDbContextAsync();

            var log = new LogApiRequest()
            {
                Endpoint = endpoint,
                RequestContent = requestContent,
                IpAddress = ipAddress,
                RequestTimestamp = DateTime.Now,
                ProcessingStatus = (int)eProcessingStatus.Pending
            };

            _logDb.LogApiRequests.Add(log);

            await _logDb.SaveChangesAsync();

            return log.Id;
        }

        public async Task<bool> OkToStartProcessing(long requestId)
        {
            var _logDb = await _logDbFactory.CreateDbContextAsync();

            ///1. Get the current request
            var request = _logDb.LogApiRequests.Find(requestId) ?? throw new BusinessException($"Request not found ({requestId})");

            ///2.Ensure we haven't reached the processing quota limit
            while (_logDb.LogApiRequests.Count(x => x.ProcessingStatus == (int)eProcessingStatus.InProcess) >= _processingSettings.MaxRequestsInProcess)
            {
                await KillHangingRequests(_logDb);

                await Task.Delay(1000);
            }

            ///3. Set InProcess status
            request.ProcessingStatus = (int)eProcessingStatus.InProcess;
            await _logDb.SaveChangesAsync();

            ///4. Make sure that no other request is in process (due to db concurrency)
            do
            {
                var allInLine = await _logDb.LogApiRequests.Where(x => x.ProcessingStatus == (int)eProcessingStatus.InProcess)
                                                    .OrderBy(x => x.RequestTimestamp)
                                                    .Select(x => new { x.Id, x.RequestTimestamp })
                                                    .ToListAsync();

                if (allInLine.Count() > _processingSettings.MaxRequestsInProcess)
                {
                    //If this request is among the first N in line, run it
                    if (allInLine.Take(_processingSettings.MaxRequestsInProcess)
                                 .Select(x => x.Id)
                                 .Contains(requestId))
                    {
                        break;
                    }
                    else
                    {
                        await KillHangingRequests(_logDb);

                        await Task.Delay(1000);
                    }
                }
                else
                {
                    break;
                }
            }
            while (true);

            ///5. Continue to execute request
            return true;


            async Task KillHangingRequests(LogAistnContext _logDb)
            {
                //Set status to Killed to requests that are InProcess status for more than the permitted time
                DateTime threshold = DateTime.Now.AddSeconds(-1 * _processingSettings.ProcessingTimeoutSeconds);

                var oldOnes = _logDb.LogApiRequests.Where(x => x.ProcessingStatus == (int)eProcessingStatus.InProcess)
                                                   .Where(x => x.RequestTimestamp < threshold).ToList()
                                                   .ToList();
                oldOnes.ForEach(x => x.ProcessingStatus = (int)eProcessingStatus.Killed);

                await _logDb.SaveChangesAsync();
            }
        }

        public async Task LogResponse(string responseContent, int responseHttpCode, long requestLogId, long? exceptionLogId)
        {
            var _logDb = await _logDbFactory.CreateDbContextAsync();

            var log = _logDb.LogApiRequests.SingleOrDefault(x => x.Id == requestLogId)
                                        ?? throw new EntityNotFoundException<long>(typeof(LogApiRequest), requestLogId);

            log.ResponseContent = responseContent;
            log.ResponseTimestamp = DateTime.Now;
            log.ResponseHttpCode = responseHttpCode;
            log.ProcessingStatus = (int)eProcessingStatus.Finished;
            log.ExceptionId = exceptionLogId;

            await _logDb.SaveChangesAsync();

            return;
        }
    }
}
