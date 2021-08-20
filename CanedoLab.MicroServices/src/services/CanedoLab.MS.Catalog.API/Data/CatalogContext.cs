using CanedoLab.MS.Catalog.API.Models;
using CanedoLab.MS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CanedoLab.MS.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDefaultColumnType(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        private void SetDefaultColumnType(ModelBuilder modelBuilder) 
        {
            var mutableProperties = 
                modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));

            foreach (var mutableProperty in mutableProperties)
            {
                mutableProperty.SetColumnType("varchar(150)");
            }
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
