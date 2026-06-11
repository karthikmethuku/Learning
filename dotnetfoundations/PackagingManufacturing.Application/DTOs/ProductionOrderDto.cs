using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.DTOs;

public record ProductionOrderDto(
    Guid Id,
    string OrderNumber,
    Guid ProductId,
    string ProductName,
    decimal QuantityOrdered,
    decimal QuantityProduced,
    ProductionStatus Status,
    DateTime PlannedStart,
    DateTime PlannedEnd,
    DateTime? ActualStart,
    DateTime? ActualEnd,
    string Notes);

public record CreateProductionOrderDto(
    Guid ProductId,
    decimal QuantityOrdered,
    DateTime PlannedStart,
    DateTime PlannedEnd,
    string Notes);

public record UpdateProductionOrderStatusDto(ProductionStatus Status);
