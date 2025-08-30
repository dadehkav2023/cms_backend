using Common.Enum;

namespace Application.Services.NoticesService
{
    public interface ISmsSenderService
    {
        bool SendSms(string receiver, SmsMessageEnum message, params string[] tokens);
        
    }
}