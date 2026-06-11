using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Domain.Entities;

public class WorkOrder
{
    public Guid Id { get; set; }
    public string WorkOrderNumber { get; set; } = string.Empty;
    public Guid ProductionOrderId { get; set; }
    public Guid ProductionLineId { get; set; }
    public decimal QuantityAssigned { get; set; }
    public decimal QuantityCompleted { get; set; }
    public decimal QuantityRejected { get; set; }
    public ProductionStatus Status { get; set; } = ProductionStatus.Draft;
    public DateTime PlannedStart { get; set; }
    public DateTime PlannedEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ProductionOrder ProductionOrder { get; set; } = null!;
    public ProductionLine ProductionLine { get; set; } = null!;
    public ICollection<QualityCheck> QualityChecks { get; set; } = [];
}
