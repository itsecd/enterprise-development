using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IItemCategoryService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemCategoryDto>>> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemCategoryDto>> GetById(int id)
    {
        var c = await service.GetByIdAsync(id);
        return c is null ? NotFound() : Ok(c);
    }

    [HttpGet("popularity")]
    public async Task<ActionResult<CategoryPopularityDto>> GetPopularity([FromQuery] DateTime end) =>
        Ok(await service.GetPopularityAsync(end));

    [HttpPost]
    public async Task<ActionResult<ItemCategoryDto>> Create(ItemCategoryEditDto request)
    {
        var c = await service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = c.CategoryId }, c);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ItemCategoryEditDto request) =>
        await service.UpdateAsync(id, request) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
