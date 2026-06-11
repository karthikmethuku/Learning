using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.Services;

public class WorkOrderService(
    IWorkOrderRepository repository,
    IProductionOrderRepository productionOrderRepository,
    IProductionLineRepository productionLineRepository)
{
    public async Task<IEnumerable<WorkOrderDto>> GetAllAsync()
    {
        var workOrders = await repository.GetAllAsync();
        return workOrders.Select(ToDto);
    }

    public async Task<WorkOrderDto?> GetByIdAsync(Guid id)
    {
        var workOrder = await repository.GetByIdAsync(id);
        return workOrder is null ? null : ToDto(workOrder);
    }

    public async Task<IEnumerable<WorkOrderDto>> GetByProductionOrderAsync(Guid productionOrderId)
    {
        var workOrders = await repository.GetByProductionOrderAsync(productionOrderId);
        return workOrders.Select(ToDto);
    }

    public async Task<WorkOrderDto> CreateAsync(CreateWorkOrderDto dto)
    {
        var productionOrder = await productionOrderRepository.GetByIdAsync(dto.ProductionOrderId)
            ?? throw new InvalidOperationException($"Production order {dto.ProductionOrderId} not found.");

        var productionLine = await productionLineRepository.GetByIdAsync(dto.ProductionLineId)
            ?? throw new InvalidOperationException($"Production line {dto.ProductionLineId} not found.");

        if (!productionLine.IsOperational)
            throw new InvalidOperationException($"Production line '{productionLine.Name}' is not operational.");

        var workOrder = new WorkOrder
        {
            Id = Guid.NewGuid(),
            WorkOrderNumber = GenerateWorkOrderNumber(),
            ProductionOrderId = dto.ProductionOrderId,
            ProductionLineId = dto.ProductionLineId,
            QuantityAssigned = dto.QuantityAssigned,
            PlannedStart = dto.PlannedStart,
            PlannedEnd = dto.PlannedEnd,
            Notes = dto.Notes,
            ProductionOrder = productionOrder,
            ProductionLine = productionLine
        };

        var created = await repository.CreateAsync(workOrder);
        return ToDto(created);
    }

    public async Task<WorkOrderDto?> UpdateProgressAsync(Guid id, UpdateWorkOrderProgressDto dto)
    {
        var workOrder = await repository.GetByIdAsync(id);
        if (workOrder is null) return null;

        workOrder.QuantityCompleted = dto.QuantityCompleted;
        workOrder.QuantityRejected = dto.QuantityRejected;
        workOrder.Status = dto.Status;

        if (dto.Status == ProductionStatus.InProgress && workOrder.ActualStart is null)
            workOrder.ActualStart = DateTime.UtcNow;

        if (dto.Status is ProductionStatus.Completed or ProductionStatus.Cancelled)
            workOrder.ActualEnd = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(workOrder);
        return ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var workOrder = await repository.GetByIdAsync(id);
        if (workOrder is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }

    private static string GenerateWorkOrderNumber() =>
        $"WO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

    private static WorkOrderDto ToDto(WorkOrder w) =>
        new(w.Id, w.WorkOrderNumber, w.ProductionOrderId, w.ProductionLineId,
            w.ProductionLine?.Name ?? string.Empty, w.QuantityAssigned,
            w.QuantityCompleted, w.QuantityRejected, w.Status,
            w.PlannedStart, w.PlannedEnd, w.ActualStart, w.ActualEnd, w.Notes);
}
