using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.DTOs;

public record WorkOrderDto(
    Guid Id,
    string WorkOrderNumber,
    Guid ProductionOrderId,
    Guid ProductionLineId,
    string ProductionLineName,
    decimal QuantityAssigned,
    decimal QuantityCompleted,
    decimal QuantityRejected,
    ProductionStatus Status,
    DateTime PlannedStart,
    DateTime PlannedEnd,
    DateTime? ActualStart,
    DateTime? ActualEnd,
    string Notes);

public record CreateWorkOrderDto(
    Guid ProductionOrderId,
    Guid ProductionLineId,
    decimal QuantityAssigned,
    DateTime PlannedStart,
    DateTime PlannedEnd,
    string Notes);

public record UpdateWorkOrderProgressDto(decimal QuantityCompleted, decimal QuantityRejected, ProductionStatus Status);
