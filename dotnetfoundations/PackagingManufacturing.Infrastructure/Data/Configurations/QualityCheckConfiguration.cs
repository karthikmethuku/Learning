using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Infrastructure.Data.Configurations;

public class QualityCheckConfiguration : IEntityTypeConfiguration<QualityCheck>
{
    public void Configure(EntityTypeBuilder<QualityCheck> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.SampleSize).HasColumnType("decimal(18,4)");
        builder.Property(q => q.PassedCount).HasColumnType("decimal(18,4)");
        builder.Property(q => q.FailedCount).HasColumnType("decimal(18,4)");
        builder.Property(q => q.Status).HasConversion<string>();
        builder.Property(q => q.InspectorName).IsRequired().HasMaxLength(100);

        builder.HasOne(q => q.WorkOrder)
            .WithMany(w => w.QualityChecks)
            .HasForeignKey(q => q.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
