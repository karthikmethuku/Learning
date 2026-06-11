using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Services;

public class ProductService(IProductRepository repository)
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await repository.GetAllAsync();
        return products.Select(ToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var existing = await repository.GetBySkuAsync(dto.SKU);
        if (existing is not null)
            throw new InvalidOperationException($"A product with SKU '{dto.SKU}' already exists.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            SKU = dto.SKU,
            Description = dto.Description,
            Unit = dto.Unit,
            StandardCost = dto.StandardCost
        };

        var created = await repository.CreateAsync(product);
        return ToDto(created);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Unit = dto.Unit;
        product.StandardCost = dto.StandardCost;
        product.IsActive = dto.IsActive;

        var updated = await repository.UpdateAsync(product);
        return ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }

    private static ProductDto ToDto(Product p) =>
        new(p.Id, p.Name, p.SKU, p.Description, p.Unit, p.StandardCost, p.IsActive);
}
