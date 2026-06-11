namespace PackagingManufacturing.Domain.Entities;

public class BillOfMaterial
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid MaterialId { get; set; }
    public decimal QuantityRequired { get; set; } // per unit of product
    public string Notes { get; set; } = string.Empty;

    public Product Product { get; set; } = null!;
    public Material Material { get; set; } = null!;
}
