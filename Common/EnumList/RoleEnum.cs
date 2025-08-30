using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enum
{
    public enum RoleEnum
    {
        [Description("درباره ما")]
        AboutUs = 1,
        [Description("تنظیمات")]
        CmsSetting = 2,
        [Description("تماس با ما")]
        ContactUs = 3,
        [Description("گالری")]
        Gallery = 4,
        [Description("منو")]
        Menu = 5,
        [Description("اسلایدر")]
        Slider = 6,
        [Description("میز خدمت")]
        ServiceDesk = 7,
        [Description("اطلاعیه")]
        Notification = 8,
        [Description("بیانیه")]
        Statement = 9,
        [Description("دسترسی سریع")]
        QuickAccess = 10,
        [Description("لینک مرتبط")]
        RelatedLink = 11,
        [Description("خبر متنی")]
        TextNews = 12,
        [Description("خبر تصویری")]
        ImageNews = 13,
        [Description("خبر ویدئویی")]
        VideoNews = 14,
        [Description("مدیریت کاربران")]
        UserManagement = 15,
        [Description("مدیریت استان")]
        Province = 16,
        [Description("مدیریت کارزار")]
        Karzar = 17,
    }
}