using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Infrastructure.Data.Configurations;

public class BillOfMaterialConfiguration : IEntityTypeConfiguration<BillOfMaterial>
{
    public void Configure(EntityTypeBuilder<BillOfMaterial> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.QuantityRequired).HasColumnType("decimal(18,4)").IsRequired();

        builder.HasOne(b => b.Product)
            .WithMany(p => p.BillOfMaterials)
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Material)
            .WithMany(m => m.BillOfMaterials)
            .HasForeignKey(b => b.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
