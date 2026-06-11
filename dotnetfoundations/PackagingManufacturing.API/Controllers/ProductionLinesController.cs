using Microsoft.AspNetCore.Mvc;
using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Services;

namespace PackagingManufacturing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductionLinesController(ProductionLineService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? operationalOnly)
    {
        if (operationalOnly == true)
            return Ok(await service.GetOperationalAsync());
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var line = await service.GetByIdAsync(id);
        return line is null ? NotFound() : Ok(line);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductionLineDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductionLineDto dto)
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
