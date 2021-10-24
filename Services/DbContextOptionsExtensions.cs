using Audit.Data;
using Audit.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Audit.Services
{
    public static class DbContextOptionsExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection services)
        {

            var siteSettings = GetSiteSettings(services);

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<MyDbContext>());

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(siteSettings.ConnectionString.AuditableConnectionStrings));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IPrincipal>(provider =>
            //    provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

            return services;

        }

        private static SiteSettings GetSiteSettings(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var siteSettingsOptions = provider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
            var siteSettings = siteSettingsOptions.Value;
            if (siteSettings == null) throw new ArgumentNullException(nameof(siteSettings));
            return siteSettings;
        }
    }
}
