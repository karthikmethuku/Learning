using Microsoft.EntityFrameworkCore;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;
using PackagingManufacturing.Infrastructure.Data;

namespace PackagingManufacturing.Infrastructure.Repositories;

public class ProductRepository(ManufacturingDbContext db) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await db.Products.Where(p => p.IsActive).ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await db.Products.Include(p => p.BillOfMaterials).ThenInclude(b => b.Material)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Product?> GetBySkuAsync(string sku) =>
        await db.Products.FirstOrDefaultAsync(p => p.SKU == sku);

    public async Task<Product> CreateAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is not null)
        {
            product.IsActive = false;
            await db.SaveChangesAsync();
        }
    }
}
