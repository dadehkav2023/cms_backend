using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IOC.ApplicationContextConfigs
{
    public static class ApplicationContextConfig
    {
        public static IServiceCollection AddApplicationContext<TService, TContext>
            (this IServiceCollection services,
            IConfiguration Configuration,
            string ConnectionConfigurationKey)
            where TService : class
            where TContext : DbContext, TService
        {
            services.AddDbContext<TContext>(option =>
            {
                option.UseSqlServer(
                    ConnectionConfigurationKey,
                    serverDbContextOptionsBuilder =>
                    {
                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                        serverDbContextOptionsBuilder.CommandTimeout(minutes);
                        serverDbContextOptionsBuilder.EnableRetryOnFailure();
                        //serverDbContextOptionsBuilder.UseNetTopologySuite();
                    }
                );
            });

            services.AddScoped<TService, TContext>();

            return services;
        }
    }
}
