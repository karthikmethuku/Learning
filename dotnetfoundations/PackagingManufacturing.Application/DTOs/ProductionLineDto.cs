namespace PackagingManufacturing.Application.DTOs;

public record ProductionLineDto(Guid Id, string Name, string Code, string Description, decimal CapacityPerHour, bool IsOperational);

public record CreateProductionLineDto(string Name, string Code, string Description, decimal CapacityPerHour);

public record UpdateProductionLineDto(string Name, string Description, decimal CapacityPerHour, bool IsOperational);
