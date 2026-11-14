using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController(IItemService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("/api/categories/{categoryId:int}/items")]
    public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetByCategoryId(int categoryId)
    {
        var items = await service.GetByCategoryIdAsync(categoryId);
        return items is null ? NotFound() : Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create(ItemEditDto request)
    {
        var item = await service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.ItemId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ItemEditDto request) =>
        await service.UpdateAsync(id, request) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
