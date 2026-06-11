using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Infrastructure.Data.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Code).IsRequired().HasMaxLength(50);
        builder.HasIndex(m => m.Code).IsUnique();
        builder.Property(m => m.Unit).IsRequired().HasMaxLength(20);
        builder.Property(m => m.UnitCost).HasColumnType("decimal(18,4)");
        builder.Property(m => m.StockQuantity).HasColumnType("decimal(18,4)");
        builder.Property(m => m.ReorderPoint).HasColumnType("decimal(18,4)");
    }
}
