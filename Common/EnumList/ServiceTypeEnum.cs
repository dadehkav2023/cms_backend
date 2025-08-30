using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum ServiceTypeEnum
    {
        [Display(Name ="تنظیمات")]
        CMSSettingSerice=1,
        [Display(Name = "اخبار")]
        NewsService =2,
        [Display(Name = "اسلایدر")]
        SliderService =3,
        [Display(Name = "اطلاعیه")]
        NotificationService =4,
        [Display(Name = "گالری تصاویر")]
        ImageGalleryService = 5,
        [Display(Name = "دسترسی سریع")]
        QuickAccessService = 6,
        [Display(Name = "لینک های مرتبط")]
        RelatedLinkService = 7,
        [Display(Name = "میز خدمت")]
        ServiceDeskService = 8,
        [Display(Name = "بیانیه")]
        StatementService = 9,
    }
}
