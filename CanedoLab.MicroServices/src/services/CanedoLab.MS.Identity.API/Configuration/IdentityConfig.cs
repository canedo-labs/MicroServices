using CanedoLab.MS.Identity.API.Data;
using CanedoLab.MS.Identity.API.Extensions;
using CanedoLab.MS.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CanedoLab.MS.Identity.API.Configuration
{
    public static class IdentityConfig 
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityErrorDescriberPortuguese>();

            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddAuthConfig(options => 
            {
                options.Audience = appSettings.Audience;
                options.Issuer = appSettings.Issuer;
                options.Secret = appSettings.Secret;
            });

            return services;
        }
    }
}
