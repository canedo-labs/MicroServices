using CanedoLab.MS.Catalog.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CanedoLab.MS.Catalog.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContextConfig(Configuration)
                .AddApiConfig()
                .AddJwtConfig(Configuration)
                .AddDependencyInjection()
                .AddSwaggerConfig();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseSwaggerConfig()
                .UseApiConfig(env);
        }
    }
}
