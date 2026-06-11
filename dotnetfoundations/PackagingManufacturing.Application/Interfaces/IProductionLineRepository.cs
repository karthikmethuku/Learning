using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Interfaces;

public interface IProductionLineRepository
{
    Task<IEnumerable<ProductionLine>> GetAllAsync();
    Task<ProductionLine?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductionLine>> GetOperationalAsync();
    Task<ProductionLine> CreateAsync(ProductionLine line);
    Task<ProductionLine> UpdateAsync(ProductionLine line);
    Task DeleteAsync(Guid id);
}
