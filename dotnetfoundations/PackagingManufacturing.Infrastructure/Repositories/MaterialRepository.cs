using Microsoft.EntityFrameworkCore;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Infrastructure.Data;

namespace PackagingManufacturing.Infrastructure.Repositories;

public class MaterialRepository(ManufacturingDbContext db) : IMaterialRepository
{
    public async Task<IEnumerable<Material>> GetAllAsync() =>
        await db.Materials.Where(m => m.IsActive).ToListAsync();

    public async Task<Material?> GetByIdAsync(Guid id) =>
        await db.Materials.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Material>> GetLowStockAsync() =>
        await db.Materials.Where(m => m.IsActive && m.StockQuantity <= m.ReorderPoint).ToListAsync();

    public async Task<Material> CreateAsync(Material material)
    {
        db.Materials.Add(material);
        await db.SaveChangesAsync();
        return material;
    }

    public async Task<Material> UpdateAsync(Material material)
    {
        db.Materials.Update(material);
        await db.SaveChangesAsync();
        return material;
    }

    public async Task DeleteAsync(Guid id)
    {
        var material = await db.Materials.FindAsync(id);
        if (material is not null)
        {
            material.IsActive = false;
            await db.SaveChangesAsync();
        }
    }
}
