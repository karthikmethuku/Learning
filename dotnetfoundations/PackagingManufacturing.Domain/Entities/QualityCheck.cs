using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Domain.Entities;

public class QualityCheck
{
    public Guid Id { get; set; }
    public Guid WorkOrderId { get; set; }
    public decimal SampleSize { get; set; }
    public decimal PassedCount { get; set; }
    public decimal FailedCount { get; set; }
    public QualityStatus Status { get; set; } = QualityStatus.Pending;
    public string InspectorName { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public DateTime InspectedAt { get; set; } = DateTime.UtcNow;

    public WorkOrder WorkOrder { get; set; } = null!;
}
