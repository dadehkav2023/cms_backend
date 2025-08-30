using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Common.Enum;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Application.Services.NoticesService
{
    public class SmsSenderService : ISmsSenderService
    {
        private readonly IConfiguration _configuration;

        public SmsSenderService( IConfiguration configuration)
        {
            _configuration = configuration;
        }    
        
        public bool SendSms(string receiver, SmsMessageEnum message, params string[] tokens)
        {
            var apiKey = _configuration.GetSection("Sms:ApiKey").Value;
            var url = _configuration.GetSection("Sms:Url").Value;
            var urlWithKey = string.Format(url, apiKey);
            var messageText = CreateViewMessage(message, tokens);
            var client = new RestClient(urlWithKey);
            var request = new RestRequest("send.json")
                .AddParameter("receptor", receiver)
                .AddParameter("sender", _configuration.GetSection("Sms:SenderNum").Value)
                .AddParameter("message", messageText);
            var response = client.Get(request);
            
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
        
        private static string CreateViewMessage(SmsMessageEnum message, params string[] viewMessagePlaceHolders)
        {
            var viewMessage = message.GetType().GetMember(message.ToString()).First()
                .GetCustomAttribute<DisplayAttribute>()?.GetName();
            if (string.IsNullOrWhiteSpace(viewMessage)) viewMessage = message.ToString();
            if (viewMessagePlaceHolders != null && viewMessagePlaceHolders.Length > 0) viewMessage = string.Format(viewMessage, viewMessagePlaceHolders);
            return viewMessage;
        }
        
    }
}