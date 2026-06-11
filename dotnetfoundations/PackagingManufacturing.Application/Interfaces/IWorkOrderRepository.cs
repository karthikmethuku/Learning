using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Interfaces;

public interface IWorkOrderRepository
{
    Task<IEnumerable<WorkOrder>> GetAllAsync();
    Task<WorkOrder?> GetByIdAsync(Guid id);
    Task<IEnumerable<WorkOrder>> GetByProductionOrderAsync(Guid productionOrderId);
    Task<IEnumerable<WorkOrder>> GetByProductionLineAsync(Guid productionLineId);
    Task<WorkOrder> CreateAsync(WorkOrder workOrder);
    Task<WorkOrder> UpdateAsync(WorkOrder workOrder);
    Task DeleteAsync(Guid id);
}
