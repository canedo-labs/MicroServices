using CanedoLab.MS.Catalog.API.Data;
using CanedoLab.MS.Catalog.API.Data.Repositories;
using CanedoLab.MS.Catalog.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CanedoLab.MS.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<CatalogContext>();

            return services;
        }
    }
}
