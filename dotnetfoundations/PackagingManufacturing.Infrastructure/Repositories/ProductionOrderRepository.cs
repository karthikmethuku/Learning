using Microsoft.EntityFrameworkCore;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Domain.Enums;
using PackagingManufacturing.Infrastructure.Data;

namespace PackagingManufacturing.Infrastructure.Repositories;

public class ProductionOrderRepository(ManufacturingDbContext db) : IProductionOrderRepository
{
    public async Task<IEnumerable<ProductionOrder>> GetAllAsync() =>
        await db.ProductionOrders.Include(o => o.Product).ToListAsync();

    public async Task<ProductionOrder?> GetByIdAsync(Guid id) =>
        await db.ProductionOrders.Include(o => o.Product).Include(o => o.WorkOrders)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<ProductionOrder>> GetByStatusAsync(ProductionStatus status) =>
        await db.ProductionOrders.Include(o => o.Product)
            .Where(o => o.Status == status).ToListAsync();

    public async Task<ProductionOrder> CreateAsync(ProductionOrder order)
    {
        db.ProductionOrders.Add(order);
        await db.SaveChangesAsync();
        return order;
    }

    public async Task<ProductionOrder> UpdateAsync(ProductionOrder order)
    {
        db.ProductionOrders.Update(order);
        await db.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await db.ProductionOrders.FindAsync(id);
        if (order is not null)
        {
            db.ProductionOrders.Remove(order);
            await db.SaveChangesAsync();
        }
    }
}
