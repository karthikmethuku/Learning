using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Interfaces;

public interface IMaterialRepository
{
    Task<IEnumerable<Material>> GetAllAsync();
    Task<Material?> GetByIdAsync(Guid id);
    Task<IEnumerable<Material>> GetLowStockAsync();
    Task<Material> CreateAsync(Material material);
    Task<Material> UpdateAsync(Material material);
    Task DeleteAsync(Guid id);
}
