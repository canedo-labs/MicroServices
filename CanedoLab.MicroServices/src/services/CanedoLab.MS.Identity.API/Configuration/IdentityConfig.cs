using CanedoLab.MS.Identity.API.Data;
using CanedoLab.MS.Identity.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = true;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience
                };
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfig(this IApplicationBuilder app) 
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
