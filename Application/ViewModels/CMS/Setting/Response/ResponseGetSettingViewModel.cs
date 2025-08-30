using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CMS.Setting.Response
{
    public class ResponseGetSettingViewModel
    {
        public string Name { get; set; }
        public string LogoImageAddress { get; set; }
        public string InstagramAddress { get; set; }
        public string FacebookAddress { get; set; }
        public string TelegramAddress { get; set; }
        public string WhatsappAddress { get; set; }
        public string TwitterAddress { get; set; }
        public string AboutUsSummary { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string GoogleMapLink { get; set; }
        public string LatitudeAndLongitude { get; set; }
        public int SliderImageCount { get; set; }
        public int HomePageNewsCount { get; set; }
    }
}
