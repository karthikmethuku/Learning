using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Domain.Entities;

public class ProductionOrder
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public decimal QuantityOrdered { get; set; }
    public decimal QuantityProduced { get; set; }
    public ProductionStatus Status { get; set; } = ProductionStatus.Draft;
    public DateTime PlannedStart { get; set; }
    public DateTime PlannedEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Product Product { get; set; } = null!;
    public ICollection<WorkOrder> WorkOrders { get; set; } = [];
}
