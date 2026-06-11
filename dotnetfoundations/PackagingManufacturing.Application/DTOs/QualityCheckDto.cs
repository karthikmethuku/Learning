using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.DTOs;

public record QualityCheckDto(
    Guid Id,
    Guid WorkOrderId,
    decimal SampleSize,
    decimal PassedCount,
    decimal FailedCount,
    QualityStatus Status,
    string InspectorName,
    string Remarks,
    DateTime InspectedAt);

public record CreateQualityCheckDto(
    Guid WorkOrderId,
    decimal SampleSize,
    decimal PassedCount,
    decimal FailedCount,
    string InspectorName,
    string Remarks);
