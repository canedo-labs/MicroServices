using CanedoLab.MS.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanedoLab.MS.Catalog.API.Data.EntityTypeConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");
            
            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");
            
            builder
                .Property(c => c.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("Catalog");
        }
    }
}
