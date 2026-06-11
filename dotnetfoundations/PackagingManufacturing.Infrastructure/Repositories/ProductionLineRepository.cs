using Microsoft.EntityFrameworkCore;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Infrastructure.Data;

namespace PackagingManufacturing.Infrastructure.Repositories;

public class ProductionLineRepository(ManufacturingDbContext db) : IProductionLineRepository
{
    public async Task<IEnumerable<ProductionLine>> GetAllAsync() =>
        await db.ProductionLines.ToListAsync();

    public async Task<ProductionLine?> GetByIdAsync(Guid id) =>
        await db.ProductionLines.FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IEnumerable<ProductionLine>> GetOperationalAsync() =>
        await db.ProductionLines.Where(l => l.IsOperational).ToListAsync();

    public async Task<ProductionLine> CreateAsync(ProductionLine line)
    {
        db.ProductionLines.Add(line);
        await db.SaveChangesAsync();
        return line;
    }

    public async Task<ProductionLine> UpdateAsync(ProductionLine line)
    {
        db.ProductionLines.Update(line);
        await db.SaveChangesAsync();
        return line;
    }

    public async Task DeleteAsync(Guid id)
    {
        var line = await db.ProductionLines.FindAsync(id);
        if (line is not null)
        {
            db.ProductionLines.Remove(line);
            await db.SaveChangesAsync();
        }
    }
}
