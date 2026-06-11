namespace PackagingManufacturing.Application.DTOs;

public record ProductDto(Guid Id, string Name, string SKU, string Description, string Unit, decimal StandardCost, bool IsActive);

public record CreateProductDto(string Name, string SKU, string Description, string Unit, decimal StandardCost);

public record UpdateProductDto(string Name, string Description, string Unit, decimal StandardCost, bool IsActive);
