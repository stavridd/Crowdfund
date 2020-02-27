using System;
using Crowdfund.Core;
using Serilog;
using Serilog.Core;


namespace Crowdfund.Core.Services {
    public class LoggerService : ILoggerService
    {
        private Logger log_;

        public LoggerService()
        {
            log_ = new LoggerConfiguration()
                .WriteTo
                .File("log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
        }

        public void LogError(StatusCode errorcode, string text)
        {
            log_.Error($"Error: {errorcode} \n Text: {text} \n Date: {DateTime.UtcNow} \n \n ");
 
        }

        public void LogInformation(string text)
        {
            log_.Information(text);
        }
    }
}
