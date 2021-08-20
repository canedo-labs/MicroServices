using CanedoLab.MS.Catalog.API.Extensions;
using CanedoLab.MS.Services.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CanedoLab.MS.Catalog.API.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration) 
        {
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
