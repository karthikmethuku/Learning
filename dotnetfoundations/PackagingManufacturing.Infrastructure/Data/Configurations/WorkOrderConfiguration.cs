using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Infrastructure.Data.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.WorkOrderNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(w => w.WorkOrderNumber).IsUnique();
        builder.Property(w => w.QuantityAssigned).HasColumnType("decimal(18,4)");
        builder.Property(w => w.QuantityCompleted).HasColumnType("decimal(18,4)");
        builder.Property(w => w.QuantityRejected).HasColumnType("decimal(18,4)");
        builder.Property(w => w.Status).HasConversion<string>();

        builder.HasOne(w => w.ProductionOrder)
            .WithMany(o => o.WorkOrders)
            .HasForeignKey(w => w.ProductionOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.ProductionLine)
            .WithMany(l => l.WorkOrders)
            .HasForeignKey(w => w.ProductionLineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
