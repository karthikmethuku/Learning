using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Infrastructure.Data.Configurations;

public class ProductionOrderConfiguration : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.Property(o => o.QuantityOrdered).HasColumnType("decimal(18,4)");
        builder.Property(o => o.QuantityProduced).HasColumnType("decimal(18,4)");
        builder.Property(o => o.Status).HasConversion<string>();

        builder.HasOne(o => o.Product)
            .WithMany(p => p.ProductionOrders)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
