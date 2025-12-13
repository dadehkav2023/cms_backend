using Application.Services.AboutUs;
using Application.Services.Accounting;
using Application.Services.Article;
using Application.Services.Article.Article;
using Application.Services.CMS.Identity.Role;
using Application.Services.CMS.Identity.User;
using Application.Services.CMS.Setting;
using Application.Services.ContactUs;
using Application.Services.ContactUs.ContactUsMessage;
using Application.Services.ImageGallery;
using Application.Services.Location;
using Application.Services.Map;
using Application.Services.Menu;
using Application.Services.Messages;
using Application.Services.News;
using Application.Services.News.Attachment;
using Application.Services.News.Category;
using Application.Services.News.PhotoNews;
using Application.Services.News.PhotoNews.Attachment;
using Application.Services.News.TextNews;
using Application.Services.News.TextNews.Attachment;
using Application.Services.News.VideoNews;
using Application.Services.News.VideoNews.Attachment;
using Application.Services.NoticesService;
using Application.Services.Notification;
using Application.Services.Notification.Attachment;
using Application.Services.QuickAccess;
using Application.Services.RelatedLink;
using Application.Services.Rules;
using Application.Services.Rules.Attachment;
using Application.Services.ServiceDesk;
using Application.Services.Slider;
using Application.Services.Statement;
using Application.Services.Statement.Attachment;
using Application.Services.Statement.Category;
using Application.Services.Store;
using Application.Services.WALLET.Services.Concrete;
using Application.Services.WALLET.Services.Interface;
using Application.Validations.FluentValidations.AboutUs;
using Application.Validations.FluentValidations.Article;
using Application.Validations.FluentValidations.Article.Attachment;
using Application.Validations.FluentValidations.ContactUs;
using Application.Validations.FluentValidations.ImageGalleryValidation;
using Application.Validations.FluentValidations.News;
using Application.Validations.FluentValidations.News.PhotoNews.Attachment;
using Application.Validations.FluentValidations.News.TextNews.Attachment;
using Application.Validations.FluentValidations.News.VideoNews.Attachment;
using Application.Validations.FluentValidations.Notification;
using Application.Validations.FluentValidations.Notification.Attachment;
using Application.Validations.FluentValidations.Rules.Attachment;
using Application.Validations.FluentValidations.ServiceDeskValidation;
using Application.Validations.FluentValidations.SettingValidate;
using Application.Validations.FluentValidations.SliderValidation;
using Application.Validations.FluentValidations.Statement;
using Application.Validations.FluentValidations.Statement.Attachment;
using Application.Validations.FluentValidations.Store;
using Application.ViewModels.AboutUs.Request;
using Application.ViewModels.Article.Attachment.Request;
using Application.ViewModels.Article.Request;
using Application.ViewModels.CMS.Setting.Request;
using Application.ViewModels.ContactUs.ContactUs.Request;
using Application.ViewModels.ImageGallery.Request;
using Application.ViewModels.News.PhotoNews.Attachment.Request;
using Application.ViewModels.News.Request;
using Application.ViewModels.News.TextNews.Attachment.Request;
using Application.ViewModels.News.VideoNews.Attachment.Request;
using Application.ViewModels.Notification.Notification.Attachment.Request;
using Application.ViewModels.Notification.Notification.Request;
using Application.ViewModels.Rules.Attachment.Request;
using Application.ViewModels.ServiceDesk.Request;
using Application.ViewModels.Slider.Request;
using Application.ViewModels.Statement.Attachment.Request;
using Application.ViewModels.Statement.Request;
using Application.ViewModels.Store.Product;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IOC
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddAppConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddUsersServices(this IServiceCollection services)
        {
            //services.AddScoped<IUsersService, UsersService>();

            return services;
        }

        public static IServiceCollection AddGalleryServices(this IServiceCollection services)
        {
            //services.AddScoped<IUsersService, UsersService>();

            return services;
        }

        public static IServiceCollection AddCMSServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ISettingService, SettingService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestSetSettingViewModel>, SettingValidation>();

            return services;
        }

        public static IServiceCollection AddSliderService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ISliderService, SliderService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestCreateSliderViewModel>, CreateNewSliderValidation>();
            services.AddTransient<IValidator<RequestEditSliderViewModel>, EditSliderValidation>();

            return services;
        }

        public static IServiceCollection AddServiceDeskService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IServiceDeskService, ServiceDeskService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewServiceDeskViewModel>, NewServiceDeskValidation>();
            services.AddTransient<IValidator<RequestEditServiceDeskViewModel>, EditServiceDeskValidation>();

            return services;
        }

        public static IServiceCollection AddImageGalleryService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IImageGalleryService, ImageGalleryService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewGalleryViewModel>, NewImageGalleryServiceValidation>();
            services.AddTransient<IValidator<RequestEditGalleryViewModel>, EditImageGalleryServiceValidation>();

            return services;
        }

        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<INotificationService, NotificationService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewNotificationViewModel>, NewNotificationServiceValidation>();
            services.AddTransient<IValidator<RequestEditNotificationViewModel>, EditNotificationServiceValidation>();

            #region Attachment

            //Services
            services.AddScoped<INotificationAttachmentService, NotificationAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewNotificationAttachmentViewModel>, NewNotificationAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditNotificationAttachmentViewModel>, EditNotificationAttachmentServiceValidation>();

            #endregion

            return services;
        }

        public static IServiceCollection AddRulesServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IRulesService, RulesService>();

            #region Attachment

            //Services
            services.AddScoped<IRulesAttachmentService, RulesAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewRulesAttachmentViewModel>, NewRulesAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditRulesAttachmentViewModel>, EditRulesAttachmentServiceValidation>();

            #endregion

            return services;
        }

        public static IServiceCollection AddStatementServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IStatementService, StatementService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewStatementViewModel>, NewStatementServiceValidation>();
            services.AddTransient<IValidator<RequestEditStatementViewModel>, EditStatementServiceValidation>();

            #region Attachment

            //Services
            services.AddScoped<IStatementAttachmentService, StatementAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewStatementAttachmentViewModel>, NewStatementAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditStatementAttachmentViewModel>, EditStatementAttachmentServiceValidation>();

            #endregion

            #region Category

            //Services
            services.AddScoped<IStatementCategoryService, StatementCategoryService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddArticleServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IArticleService, ArticleService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewArticleViewModel>, NewArticleServiceValidation>();
            services.AddTransient<IValidator<RequestEditStatementViewModel>, EditStatementServiceValidation>();

            #region Attachment

            //Services
            services.AddScoped<IArticleAttachmentService, ArticleAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewArticleAttachmentViewModel>, NewArticleAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditArticleAttachmentViewModel>, EditArticleAttachmentServiceValidation>();

            #endregion

            return services;
        }

        public static IServiceCollection AddMapServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IMapService, MapService>();

            return services;
        }

        public static IServiceCollection AddLocationServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ILocationService, LocationService>();

            return services;
        }

        public static IServiceCollection AddNewsServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ITextNewsService, TextNewsService>();

            // FluentValidation
            services.AddTransient<IValidator<RequestNewNewsViewModel>, NewNewsServiceValidation>();
            services.AddTransient<IValidator<RequestEditNewsViewModel>, EditNewsServiceValidation>();

            #region PhotoNews

            //Services
            services.AddScoped<IPhotoNewsService, PhotoNewsService>();

            #region Attachment

            //Attachment
            services.AddScoped<IPhotoNewsAttachmentService, PhotoNewsAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewPhotoNewsAttachmentViewModel>, NewPhotoNewsAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditPhotoNewsAttachmentViewModel>, EditPhotoNewsAttachmentServiceValidation>();

            #endregion

            #endregion

            #region VideoNews

            //Services
            services.AddScoped<IVideoNewsService, VideoNewsService>();

            #region Attachment

            //Attachment
            services.AddScoped<IVideoNewsAttachmentService, VideoNewsAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestNewVideoNewsAttachmentViewModel>, NewVideoNewsAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditVideoNewsAttachmentViewModel>, EditVideoNewsAttachmentServiceValidation>();

            #endregion

            #endregion


            #region TextNews

            services.AddScoped<INewsService, NewsService>();

            #region Attachment

            //Services
            services.AddScoped<ITextNewsAttachmentService, TextNewsAttachmentService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestTextNewNewsAttachmentViewModel>, NewTextNewsAttachmentServiceValidation>();
            services.AddTransient<IValidator<RequestEditTextNewsAttachmentViewModel>, EditTextNewsAttachmentServiceValidation>();

            #endregion

            #endregion

            #region Category

            //Services
            services.AddScoped<INewsCategoryService, NewsCategoryService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddQuickAccessServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IQuickAccessService, QuickAccessService>();

            return services;
        }

        public static IServiceCollection AddRelatedLinkServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IRelatedLinkService, RelatedLinkService>();

            return services;
        }

        public static IServiceCollection AddMenuServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IMenuService, MenuService>();

            return services;
        }

        public static IServiceCollection AddAboutUsService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IAboutUsService, AboutUsService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestSetAboutUsViewModel>, AboutUsValidation>();

            return services;
        }

        public static IServiceCollection AddContactUsService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IContactUsService, ContactUsService>();

            //FluentValidation
            services.AddTransient<IValidator<RequestSetContactUsViewModel>, ContactUsValidation>();

            #region ContactUs Message

            //Services
            services.AddScoped<IContactUsMessageService, ContactUsMessageService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IWalletService, WalletService>();

            services.AddTransient<IValidator<RequestSetProductViewModel>, RequestSetProductValidator>();

            return services;
        }

        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IMessage, SMS>();

            return services;
        }

        public static IServiceCollection AddNoticesService(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ISmsSenderService, SmsSenderService>();

            return services;
        }
    }
}
