using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ApiImageUploader
{
    public class ApiImageUploaderViewModel
    {
        public string URLLink { get; set; }
        public string ApiController { get; set; }
        public string Apikey { get; set; }
    }
    public class EmailConfigurationViewModel
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
