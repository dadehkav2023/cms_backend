using Application.ViewModels.ApiImageUploader;
using Common.Enum.Message;
using Common.EnumList;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Threading.Tasks;

namespace Application.Services.Messages
{
    public interface IMessage
    {
        Task<bool> Send(string reciver, string message, string subject = "", params string[] tokns);
        Task<bool> SendByPattern(SmsPatternEnum smsPattern, string receiver, string token);
    }
    public class SMS : IMessage
    {
        private readonly ILogger<SMS> _logger;
        private readonly IConfiguration _configuration;


        public SMS(ILogger<SMS> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<bool> Send(string reciver, string message, string subject, params string[] tokns)
        {
            var apiKey = _configuration.GetSection("Sms:ApiKey").Value;
            var url = _configuration.GetSection("Sms:Url").Value;
            var urlWithKey = string.Format(url, apiKey);
            var messageText = message;
            var client = new RestClient(urlWithKey);
            var request = new RestRequest("send.json")
                .AddParameter("receptor", reciver)
                .AddParameter("sender", _configuration.GetSection("Sms:SenderNum").Value)
                .AddParameter("message", messageText);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SendByPattern(SmsPatternEnum smsPattern, string receiver, string token)
        {
            var apiKey = _configuration.GetSection("Sms:ApiKey").Value;
            var url = _configuration.GetSection("Sms:ByPatternUrl").Value;
            var urlWithKey = string.Format(url, apiKey);

            var client = new RestClient(urlWithKey) { Timeout = -1 };
            var request = new RestRequest("lookup.json")
                .AddParameter("receptor", receiver)
                .AddParameter("token", token)
                .AddParameter("template", smsPattern.GetPatternName());

            var response = client.Get(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
    public class Email : IMessage
    {
        private readonly ILogger<Email> _logger;
        private readonly IConfiguration _configuration;
        private readonly EmailConfigurationViewModel _optionsEmailConfiguration;
        public Email(ILogger<Email> logger, IConfiguration configuration
            ,IOptions<EmailConfigurationViewModel> optionsEmailConfiguration)
        {
            _logger = logger;
            _configuration = configuration;
            _optionsEmailConfiguration = optionsEmailConfiguration?.Value;
        }
        public async Task<bool> Send(string emailreciver, string message,string subject, params string[] tokns)
        {
            // _logger.LogInformation($"Email: {emailreciver}, subject: {message}, message: {htmlMessage}");
            var Server = _optionsEmailConfiguration.SmtpServer;
            var Port = _optionsEmailConfiguration.Port;
            var UserName = _optionsEmailConfiguration.Username;
            var Password = _optionsEmailConfiguration.Password;
            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.To.Add(emailreciver);
            mailMessage.From = new System.Net.Mail.MailAddress(_optionsEmailConfiguration.From);
            mailMessage.Subject = subject;
            mailMessage.Body = message + " " + tokns[0];
            mailMessage.IsBodyHtml = true;
            using (var smtp = new System.Net.Mail.SmtpClient())
            {
                var credential = new System.Net.NetworkCredential
                {
                    UserName = UserName,
                    Password = Password
                };
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Host = Server;
                smtp.Port = Port;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMessage);
            }
            return true;
        }

        public async Task<bool> SendByPattern(SmsPatternEnum smsPattern, string receiver, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
