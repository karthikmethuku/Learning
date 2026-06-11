using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.Application.Interfaces;

public interface IProductionOrderRepository
{
    Task<IEnumerable<ProductionOrder>> GetAllAsync();
    Task<ProductionOrder?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductionOrder>> GetByStatusAsync(ProductionStatus status);
    Task<ProductionOrder> CreateAsync(ProductionOrder order);
    Task<ProductionOrder> UpdateAsync(ProductionOrder order);
    Task DeleteAsync(Guid id);
}
