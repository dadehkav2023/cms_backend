using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CMS.Setting.Request
{
    public class RequestSetSettingViewModel
    {
        
        [Required(ErrorMessage = "نام سامانه اجباری می باشد")]
        [MinLength(5, ErrorMessage = "حداقل 5 کارکتر")]
        [MaxLength(500,ErrorMessage ="حدااکثر 500 کارکتر")]
        public string Name { get; set; }
        public IFormFile LogoImageAddress { get; set; }
        public string InstagramAddress { get; set; }
        public string FacebookAddress { get; set; }
        public string TelegramAddress { get; set; }
        public string WhatsappAddress { get; set; }
        public string TwitterAddress { get; set; }
        public string AboutUsSummary { get; set; }
        //[RegularExpression(@"(\+98|0)?9\d{9}", ErrorMessage = "شماره موبایل را بدرستی وارد کنید")]
        public string Tell { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "آدرس اجباری می باشد")]
        [MinLength(5, ErrorMessage = "حداقل 5 کارکتر")]
        [MaxLength(4000, ErrorMessage = "حدااکثر 4000 کارکتر")]
        public string Address { get; set; }
        //[RegularExpression("([0-9]+)", ErrorMessage = "لطفاً فقط عدد وارد نمایید")]
        [Required(ErrorMessage = "کد پستی اجباری می باشد")]
        [MinLength(5, ErrorMessage = "حداقل 5 کارکتر")]
        [MaxLength(4000, ErrorMessage = "حدااکثر 30 کارکتر")]
        public string PostalCode { get; set; }
        public string GoogleMapLink { get; set; }
        public string LatitudeAndLongitude { get; set; }
        public int SliderImageCount { get; set; } = 0;
        public int HomePageNewsCount { get; set; } = 0;
    }
}
