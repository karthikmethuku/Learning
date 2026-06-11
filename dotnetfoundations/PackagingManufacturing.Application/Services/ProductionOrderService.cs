using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.Services;

public class ProductionOrderService(IProductionOrderRepository repository, IProductRepository productRepository)
{
    public async Task<IEnumerable<ProductionOrderDto>> GetAllAsync()
    {
        var orders = await repository.GetAllAsync();
        return orders.Select(ToDto);
    }

    public async Task<ProductionOrderDto?> GetByIdAsync(Guid id)
    {
        var order = await repository.GetByIdAsync(id);
        return order is null ? null : ToDto(order);
    }

    public async Task<IEnumerable<ProductionOrderDto>> GetByStatusAsync(ProductionStatus status)
    {
        var orders = await repository.GetByStatusAsync(status);
        return orders.Select(ToDto);
    }

    public async Task<ProductionOrderDto> CreateAsync(CreateProductionOrderDto dto)
    {
        var product = await productRepository.GetByIdAsync(dto.ProductId)
            ?? throw new InvalidOperationException($"Product {dto.ProductId} not found.");

        var order = new ProductionOrder
        {
            Id = Guid.NewGuid(),
            OrderNumber = GenerateOrderNumber(),
            ProductId = dto.ProductId,
            QuantityOrdered = dto.QuantityOrdered,
            PlannedStart = dto.PlannedStart,
            PlannedEnd = dto.PlannedEnd,
            Notes = dto.Notes,
            Product = product
        };

        var created = await repository.CreateAsync(order);
        return ToDto(created);
    }

    public async Task<ProductionOrderDto?> UpdateStatusAsync(Guid id, UpdateProductionOrderStatusDto dto)
    {
        var order = await repository.GetByIdAsync(id);
        if (order is null) return null;

        order.Status = dto.Status;

        if (dto.Status == ProductionStatus.InProgress && order.ActualStart is null)
            order.ActualStart = DateTime.UtcNow;

        if (dto.Status is ProductionStatus.Completed or ProductionStatus.Cancelled)
            order.ActualEnd = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(order);
        return ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var order = await repository.GetByIdAsync(id);
        if (order is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }

    private static string GenerateOrderNumber() =>
        $"PO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

    private static ProductionOrderDto ToDto(ProductionOrder o) =>
        new(o.Id, o.OrderNumber, o.ProductId, o.Product?.Name ?? string.Empty,
            o.QuantityOrdered, o.QuantityProduced, o.Status,
            o.PlannedStart, o.PlannedEnd, o.ActualStart, o.ActualEnd, o.Notes);
}
