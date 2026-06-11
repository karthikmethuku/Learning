namespace PackagingManufacturing.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty; // e.g. "box", "roll", "sheet"
    public decimal StandardCost { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<BillOfMaterial> BillOfMaterials { get; set; } = [];
    public ICollection<ProductionOrder> ProductionOrders { get; set; } = [];
}
