
namespace Crowdfund.Core.Services {
    public interface ILoggerService 
    {

        void LogError(StatusCode errorcode, string text);

        void LogInformation(string text);
    }
}
