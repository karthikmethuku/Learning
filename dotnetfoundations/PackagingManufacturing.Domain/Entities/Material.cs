namespace PackagingManufacturing.Domain.Entities;

public class Material
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty; // e.g. "kg", "m2", "litre"
    public decimal UnitCost { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal ReorderPoint { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<BillOfMaterial> BillOfMaterials { get; set; } = [];
}
