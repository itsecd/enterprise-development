using StoreCashFlow.Domain;
using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;

namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с магазинами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StoreController(StoreService storeService) : ControllerBase
{
    /// <summary>
    /// Получить все магазины
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Store>> GetStores()
    {
        return Ok(storeService.GetStores());
    }

    /// <summary>
    /// Получить магазин по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Store> GetStore(int id)
    {
        return Ok(storeService.GetStoreById(id));
    }

    /// <summary>
    /// Добавить магазин
    /// </summary>
    [HttpPost]
    public ActionResult<Store> AddStore(Store newStore)
    {
        return Ok(storeService.AddStore(newStore));
    }

    /// <summary>
    /// Изменить магазин
    /// </summary>
    [HttpPut]
    public IActionResult UpdateStore(Store store)
    {
        var result = storeService.UpdateStore(store);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Удалить магазин
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteStore(int id)
    {
        var result = storeService.DeleteStore(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
