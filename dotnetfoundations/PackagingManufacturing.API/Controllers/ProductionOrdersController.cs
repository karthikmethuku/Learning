using Microsoft.AspNetCore.Mvc;
using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Services;
using PackagingManufacturing.Domain.Enums;

namespace PackagingManufacturing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductionOrdersController(ProductionOrderService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductionStatus? status)
    {
        if (status.HasValue)
            return Ok(await service.GetByStatusAsync(status.Value));
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await service.GetByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductionOrderDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateProductionOrderStatusDto dto)
    {
        var updated = await service.UpdateStatusAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
