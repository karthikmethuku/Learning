using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Interfaces;
using PackagingManufacturing.Domain.Entities;

namespace PackagingManufacturing.Application.Services;

public class ProductionLineService(IProductionLineRepository repository)
{
    public async Task<IEnumerable<ProductionLineDto>> GetAllAsync()
    {
        var lines = await repository.GetAllAsync();
        return lines.Select(ToDto);
    }

    public async Task<ProductionLineDto?> GetByIdAsync(Guid id)
    {
        var line = await repository.GetByIdAsync(id);
        return line is null ? null : ToDto(line);
    }

    public async Task<IEnumerable<ProductionLineDto>> GetOperationalAsync()
    {
        var lines = await repository.GetOperationalAsync();
        return lines.Select(ToDto);
    }

    public async Task<ProductionLineDto> CreateAsync(CreateProductionLineDto dto)
    {
        var line = new ProductionLine
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description,
            CapacityPerHour = dto.CapacityPerHour
        };

        var created = await repository.CreateAsync(line);
        return ToDto(created);
    }

    public async Task<ProductionLineDto?> UpdateAsync(Guid id, UpdateProductionLineDto dto)
    {
        var line = await repository.GetByIdAsync(id);
        if (line is null) return null;

        line.Name = dto.Name;
        line.Description = dto.Description;
        line.CapacityPerHour = dto.CapacityPerHour;
        line.IsOperational = dto.IsOperational;

        var updated = await repository.UpdateAsync(line);
        return ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var line = await repository.GetByIdAsync(id);
        if (line is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }

    private static ProductionLineDto ToDto(ProductionLine l) =>
        new(l.Id, l.Name, l.Code, l.Description, l.CapacityPerHour, l.IsOperational);
}
