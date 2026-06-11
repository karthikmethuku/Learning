using Microsoft.AspNetCore.Mvc;
using PackagingManufacturing.Application.DTOs;
using PackagingManufacturing.Application.Services;

namespace PackagingManufacturing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkOrdersController(WorkOrderService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? productionOrderId)
    {
        if (productionOrderId.HasValue)
            return Ok(await service.GetByProductionOrderAsync(productionOrderId.Value));
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var workOrder = await service.GetByIdAsync(id);
        return workOrder is null ? NotFound() : Ok(workOrder);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkOrderDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:guid}/progress")]
    public async Task<IActionResult> UpdateProgress(Guid id, UpdateWorkOrderProgressDto dto)
    {
        var updated = await service.UpdateProgressAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
