using AISTN.Data.LogDataModel;
using Microsoft.AspNetCore.Http;

namespace AISTN.Repository
{
    public class ExceptionLogger
    {
        protected readonly LogAistnContext _logDb;
        protected readonly IHttpContextAccessor _contextAccessor;

        public ExceptionLogger(LogAistnContext logDb, IHttpContextAccessor contextAccessor)
        {
            _logDb = logDb;
            _contextAccessor = contextAccessor;
        }


        /// <summary>
        /// TODO connect to real table and log exceptions
        /// </summary>
        /// <param name="ex"></param>
        public long LogException(Exception ex)
        {
            string message = ex.GetFullExceptionMessage();
            string stackTrace = string.Empty;

            //Get all stackTraces
            Exception sEx = ex;
            while (sEx != null)
            {
                stackTrace += sEx.StackTrace + Environment.NewLine + "-----------------------------------------" + Environment.NewLine;
                sEx = ex.InnerException != null ? sEx.InnerException : null;
            }

            LogException log = new LogException()
            {
                Message = message,
                StackTrace = stackTrace,
                Type = ex.GetType().ToString(),
                IpAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserId = "", // will be set when we have authenticator
                Timestamp = DateTime.Now
            };

            _logDb.LogExceptions.Add(log);

            _logDb.SaveChanges();

            return log.Id;
        }
    }

    public static class ExceptionReader
    {
        public static string GetFullExceptionMessage(this Exception ex)
        {
            string msg = ex.Message;

            if (ex.InnerException != null)
                msg += ". Inner Msg: " + ex.InnerException.GetFullExceptionMessage();

            return msg;
        }
    }
}
