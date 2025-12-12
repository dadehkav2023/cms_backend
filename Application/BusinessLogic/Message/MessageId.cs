using System.ComponentModel.DataAnnotations;

namespace Application.BusinessLogic.Message
{
    public enum MessageId
    {
        [Display(Name = "عملیات با موفقیت انجام شد.")]
        Success = 1,
        
        [Display(Name = "مشکلی در سیستم بوجود آمده است.")]
        Exception = -1,

        [Display(Name = "مشکل {0}اتفاق افتاده است ")]
        InternalError = -2,

        [Display(Name = "تنظیمات تعریف نشده است")]
        SettingIsEmpty = -3,
        
        [Display(Name = "اسلایدر یافت نشد")]
        SliderNotFound = -4,
        
        [Display(Name = "میز خدمت یافت نشد")]
        ServiceDeskNotFound = -5,
        
        [Display(Name = "آپلود فایل به درستی انجام نشد")]
        CannotUploadFile = -6,
        
        [Display(Name = "آیتم گالری تصویر یافت نشد")]
        GalleryItemNotFound = -7,
        
        [Display(Name = "اطلاعیه مورد نظر یافت نشد")]
        NotificationNotFound = -8,
        
        [Display(Name = "پیوست یافت نشد")]
        AttachmentNotFound = -9,
        
        [Display(Name = "بیانیه یافت نشد")]
        StatementNotFound = -10,
        
        [Display(Name = "دسترسی سریع یافت نشد")]
        QuickAccessNotFound = -11,
        
        [Display(Name = "لینک مرتبط یافت نشد")]
        RelatedLinkNotFound = -12,
        
        [Display(Name = "درباره ما تعریف نشده است")]
        AboutUsIsEmpty = -13,
        
        [Display(Name = "نام و نام خانوادگی الزامی است")]
        FirstNameAndLastNameIsRequired = -14,
        
        [Display(Name = "کاربر وارد شده اشتباه می باشد")]
        UserIsWrong = -15,
        
        [Display(Name = "خبر وارد شده اشتباه می باشد")]
        NewsNotFound = -16,
        
        [Display(Name = "دسته بندی وارد شده اشتباه می باشد")]
        CategoryNotFound = -17,

        [Display(Name = "مقاله وارد شده اشتباه می باشد")]
        ArticleNotFound = -18,
        
        [Display(Name = "قوانین و مقرارت وارد شده اشتباه می باشد")]
        RulesNotFound = -19,
        
        [Display(Name = "کد وارد شده معتبر نمی باشد")]
        ValidationCodeIsWrong = -20,
        
        [Display(Name = "خطایی رخ داده است")]
        ErrorOccured = -21,
        
        [Display(Name = "شماره موبایل وارد شده معتبر نمی باشد")]
        MobileNumberWasNotConfirmed = -22,
        [Display(Name = "اطلاعات کاربری یافت نشد")]
        NotExistAccountUser = -23,
        [Display(Name = "اطلاعات کاربری تکراری میباشد")]
        UserInfoDoublicat = -24,
        [Display(Name = "کاربر با موفقیت اضافه شد")]
        UserSuccessfullyAdded = -25,
        [Display(Name = "تاریخ انقضا زمان تمام نشده است")]
        ExpirationDateTimeNotOver = -26,
        [Display(Name = "شماره موبایل یافت نشد")]
        PhoneNotFind = -27,
        [Display(Name = "کد تأیید درست است")]
        VerificationCodeTrue = -28,
        [Display(Name = "کد تأیید نادرست است")]
        VerificationCodeNotTrue = -29,
        [Display(Name = "شماره موبایل تایید نشده است")]
        PhoneNotVerifyed = -30,
        [Display(Name = "نام کاربری {0} قبلا در سیستم وجود داشته است..لطفا نام کاربری دیگری انتخاب کنید")]
        UserNameAlreadyExisted = -31,
        [Display(Name = "شماره موبایل {0} وجود ندارد")]
        PhoneNotExist = -32,
        [Display(Name = "کد فعالسازی به شماره موبایل {0} ارسال شد")]
        SendAcceptCodeToPhonenumber = -33,
        [Display(Name = "رمز ورود با تکرار آن یکسان نیست")]
        ComparePassNotEqual = -34,
        [Display(Name = "خطا در تغییر رمز")]
        ErrorInChangePass = -35,
        [Display(Name = "تغیرر رمز ورود با موفقیت انجام شد")]
        SuccessChangePass = -36,
        [Display(Name = "ایمیل {0} وجود ندارد")]
        EmailNotExist = -37,
        [Display(Name = "کد فعالسازی ایمیل {0} ارسال شد")]
        EmailConfirmCode = -38,
        [Display(Name = "امکان تغییر محصول/خدمت نمی باشد")]
        CannotEditProduct = -39,
        [Display(Name = "موجودی کالای {0} کمتر از مقدار درخواستی می باشد")]
        ProductInventoryNotEnough = -40,
        [Display(Name = "موجودی یف پول کافی نمی باشد")]
        InsufficientWalletBalance = -41,
    }
}