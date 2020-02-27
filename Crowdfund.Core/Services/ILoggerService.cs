using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services {
    public interface ILoggerService 
    {
        void LogError(StatusCode errorcode, string text);
        void LogInformation(string text);
    }
}
