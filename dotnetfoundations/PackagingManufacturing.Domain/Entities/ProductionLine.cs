namespace PackagingManufacturing.Domain.Entities;

public class ProductionLine
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal CapacityPerHour { get; set; }
    public bool IsOperational { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<WorkOrder> WorkOrders { get; set; } = [];
}
