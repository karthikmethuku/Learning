namespace PackagingManufacturing.Application.DTOs;

public record MaterialDto(Guid Id, string Name, string Code, string Description, string Unit, decimal UnitCost, decimal StockQuantity, decimal ReorderPoint, bool IsActive);

public record CreateMaterialDto(string Name, string Code, string Description, string Unit, decimal UnitCost, decimal StockQuantity, decimal ReorderPoint);

public record UpdateMaterialDto(string Name, string Description, string Unit, decimal UnitCost, decimal StockQuantity, decimal ReorderPoint, bool IsActive);
