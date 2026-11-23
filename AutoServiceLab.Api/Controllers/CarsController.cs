using AutoServiceLab.Application.Contracts.Dtos.Cars;
using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с автомобилями
/// </summary>
public class CarsController(ICarService service, ILogger<CarsController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Получить список всех автомобилей
    /// </summary>
    [HttpGet]
    public override async Task<ActionResult<IList<CarDto>>> GetAll() => await base.GetAll();

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override async Task<ActionResult<CarDto>> Get(int id) =>
await base.Get(id);

    /// <summary>
    /// Создать новый автомобиль
    /// </summary>
    [HttpPost]
    public override async Task<ActionResult<CarDto>> Post(CarCreateUpdateDto dto) => await base.Post(dto);

    /// <summary>
    /// Обновить данные автомобиля
    /// </summary>
    [HttpPut("{id}")]
    public override async Task<ActionResult<CarDto>> Update(CarCreateUpdateDto dto, int id) => await base.Update(dto, id);

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id) => await base.Delete(id);
}
