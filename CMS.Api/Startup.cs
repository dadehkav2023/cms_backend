using System;
using System.Collections.Generic;
using Application.AutoMapper;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Interfaces.Public.Upload;
using Application.Services.Public.Upload;
using Application.ViewModels.ApiImageUploader;
using Domain.Entities.Identity.Role;
using Domain.Entities.Identity.User;
using FluentValidation.AspNetCore;
using Infrastructure.ExternalApi.FileServer;
using Infrastructure.ExternalApi.ImageServer;
using Infrastructure.IOC;
using Infrastructure.IOC.ApplicationContextConfigs;
using Infrastructure.IOC.IdentityContextConfigs;
using Infrastructure.IOC.IdentityServer4Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.Contexts;
using Persistence.EntityTypeConfigs.RelatedLink;

namespace CMS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();

            //Cors Config
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithHeaders().WithExposedHeaders("AccessToken", "RefreshToken")
                );
            });
            //Application SqlServer Config
            services.AddApplicationContext<IUnitOfWorkApplication, ApplicationDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Menu SqlServer Config
            services.AddApplicationContext<IUnitOfWorkMenu, MenuDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Slider SqlServer Config
            services.AddApplicationContext<IUnitOfWorkSlider, SliderDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //ServiceDesk SqlServer Config
            services.AddApplicationContext<IUnitOfWorkServiceDesk, ServiceDeskDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Gallery SqlServer Config
            services.AddApplicationContext<IUnitOfWorkGallery, GalleryDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Notification SqlServer Config
            services.AddApplicationContext<IUnitOfWorkNotification, NotificationDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Statement SqlServer Config
            services.AddApplicationContext<IUnitOfWorkStatement, StatementDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //News SqlServer Config
            services.AddApplicationContext<IUnitOfWorkNews, NewsDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //QuickAccess SqlServer Config
            services.AddApplicationContext<IUnitOfWorkQuickAccess, QuickAccessDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //RelatedLink SqlServer Config
            services.AddApplicationContext<IUnitOfWorkRelatedLink, RelatedLinkDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //AboutUs SqlServer Config
            services.AddApplicationContext<IUnitOfWorkAboutUs, AboutUsDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //ContactUs SqlServer Config
            services.AddApplicationContext<IUnitOfWorkContactUs, ContactUsDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Article SqlServer Config
            services.AddApplicationContext<IUnitOfWorkArticle, ArticleDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Map SqlServer Config
            services.AddApplicationContext<IUnitOfWorkMap, MapDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Rules SqlServer Config
            services.AddApplicationContext<IUnitOfWorkRules, RulesDataBaseContext>(Configuration, Configuration.GetConnectionString("Application"));
            //Identity Config
            services.AddIdentityContext<IdentityDataBaseContext, User, Role, int>(Configuration, Configuration.GetConnectionString("Application"));
            //SSO Config
            services.Add_SSOAPI_Config(Client_Id: "ApiClientIdKey", Authority: "https://localhost:44305", Audience: "https://localhost:44305/resources");

            //AutoMapper
            services.AddAutoMapper(AutoMapperConfig.RegisterMappings());

            //Services
            services.AddAppConfigures(Configuration);
            services.AddUsersServices();
            services.AddCMSServices();
            services.AddSliderService();
            services.AddServiceDeskService();
            services.AddImageGalleryService();
            services.AddNotificationServices();
            services.AddStatementServices();
            services.AddQuickAccessServices();
            services.AddRelatedLinkServices();
            services.AddAboutUsService();
            services.AddMenuServices();
            services.AddContactUsService();
            services.AddIdentityService();
            services.AddNoticesService();
            services.AddNewsServices();
            services.AddArticleServices();
            services.AddMapServices();
            services.AddRulesServices();
            services.AddLocationServices();

            //public services
            services.AddScoped<IUploader, Uploader>();
            //ImageApiUploader - Factory Method DesignPattern
            services.Configure<ApiImageUploaderViewModel>(option => Configuration.GetSection("File:ApiImageUploader").Bind(option));
            services.AddTransient<IFileUploaderService, FileUploaderService>();

            //Api Versioning Config
            services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new ApiVersion(1, 0);
                Options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1" });
                c.AddSecurityDefinition(
                    "oauth2",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(Configuration.GetSection("SSO").GetSection("AuthorizationUrl").Value),
                                TokenUrl = new Uri(Configuration.GetSection("SSO").GetSection("TokenUrl").Value),
                                Scopes = new Dictionary<string, string> { { Configuration.GetSection("SSO").GetSection("ScopeName").Value, "Sabak Api" } },
                            },
                        },
                    }
                );
                c.OperationFilter<Swagger.AuthorizationHeaderParameterOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS.Api v1");
                c.RoutePrefix = string.Empty;
                c.OAuthUsePkce();
            });
            // }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/echo", async context => context.Response.WriteAsync("echo")).RequireCors("CorsPolicy");
            });
        }
    }
}
