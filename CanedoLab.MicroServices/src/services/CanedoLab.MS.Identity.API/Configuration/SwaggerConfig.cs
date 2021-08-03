using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CanedoLab.MS.Identity.API.Configuration
{
    public static class SwaggerConfig 
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services) 
        {
            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Canedo Lab Microservices Auth",
                    Description = "",
                    Contact = new OpenApiContact() { Name = "Canedo", Email = "email@email.com" },
                    License = new OpenApiLicense() { Name = "MIT" }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
