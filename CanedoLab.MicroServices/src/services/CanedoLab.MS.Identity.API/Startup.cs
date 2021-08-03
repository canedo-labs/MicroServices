using CanedoLab.MS.Identity.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CanedoLab.MS.Identity.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContextConfig(_configuration)
                .AddIdentityConfig(_configuration)
                .AddApiConfig()
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
