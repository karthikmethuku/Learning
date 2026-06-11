using Microsoft.AspNetCore.Mvc;
using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Services;

namespace PackagingManufacturing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterialsController(MaterialService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var material = await service.GetByIdAsync(id);
        return material is null ? NotFound() : Ok(material);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock() => Ok(await service.GetLowStockAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaterialDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMaterialDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
