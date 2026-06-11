using Microsoft.EntityFrameworkCore;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Infrastructure.Data;

namespace PackagingManufacturing.Infrastructure.Repositories;

public class WorkOrderRepository(ManufacturingDbContext db) : IWorkOrderRepository
{
    public async Task<IEnumerable<WorkOrder>> GetAllAsync() =>
        await db.WorkOrders.Include(w => w.ProductionLine).Include(w => w.ProductionOrder).ToListAsync();

    public async Task<WorkOrder?> GetByIdAsync(Guid id) =>
        await db.WorkOrders
            .Include(w => w.ProductionLine)
            .Include(w => w.ProductionOrder)
            .Include(w => w.QualityChecks)
            .FirstOrDefaultAsync(w => w.Id == id);

    public async Task<IEnumerable<WorkOrder>> GetByProductionOrderAsync(Guid productionOrderId) =>
        await db.WorkOrders.Include(w => w.ProductionLine)
            .Where(w => w.ProductionOrderId == productionOrderId).ToListAsync();

    public async Task<IEnumerable<WorkOrder>> GetByProductionLineAsync(Guid productionLineId) =>
        await db.WorkOrders.Include(w => w.ProductionOrder)
            .Where(w => w.ProductionLineId == productionLineId).ToListAsync();

    public async Task<WorkOrder> CreateAsync(WorkOrder workOrder)
    {
        db.WorkOrders.Add(workOrder);
        await db.SaveChangesAsync();
        return workOrder;
    }

    public async Task<WorkOrder> UpdateAsync(WorkOrder workOrder)
    {
        db.WorkOrders.Update(workOrder);
        await db.SaveChangesAsync();
        return workOrder;
    }

    public async Task DeleteAsync(Guid id)
    {
        var workOrder = await db.WorkOrders.FindAsync(id);
        if (workOrder is not null)
        {
            db.WorkOrders.Remove(workOrder);
            await db.SaveChangesAsync();
        }
    }
}
