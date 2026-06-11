using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Services;

public class MaterialService(IMaterialRepository repository)
{
    public async Task<IEnumerable<MaterialDto>> GetAllAsync()
    {
        var materials = await repository.GetAllAsync();
        return materials.Select(ToDto);
    }

    public async Task<MaterialDto?> GetByIdAsync(Guid id)
    {
        var material = await repository.GetByIdAsync(id);
        return material is null ? null : ToDto(material);
    }

    public async Task<IEnumerable<MaterialDto>> GetLowStockAsync()
    {
        var materials = await repository.GetLowStockAsync();
        return materials.Select(ToDto);
    }

    public async Task<MaterialDto> CreateAsync(CreateMaterialDto dto)
    {
        var material = new Material
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description,
            Unit = dto.Unit,
            UnitCost = dto.UnitCost,
            StockQuantity = dto.StockQuantity,
            ReorderPoint = dto.ReorderPoint
        };

        var created = await repository.CreateAsync(material);
        return ToDto(created);
    }

    public async Task<MaterialDto?> UpdateAsync(Guid id, UpdateMaterialDto dto)
    {
        var material = await repository.GetByIdAsync(id);
        if (material is null) return null;

        material.Name = dto.Name;
        material.Description = dto.Description;
        material.Unit = dto.Unit;
        material.UnitCost = dto.UnitCost;
        material.StockQuantity = dto.StockQuantity;
        material.ReorderPoint = dto.ReorderPoint;
        material.IsActive = dto.IsActive;

        var updated = await repository.UpdateAsync(material);
        return ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var material = await repository.GetByIdAsync(id);
        if (material is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }

    private static MaterialDto ToDto(Material m) =>
        new(m.Id, m.Name, m.Code, m.Description, m.Unit, m.UnitCost, m.StockQuantity, m.ReorderPoint, m.IsActive);
}
