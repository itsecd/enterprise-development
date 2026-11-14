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
    public override ActionResult<IList<CarDto>> GetAll() => base.GetAll();

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override ActionResult<CarDto> Get(int id) => base.Get(id);

    /// <summary>
    /// Создать новый автомобиль
    /// </summary>
    [HttpPost]
    public override ActionResult<CarDto> Post(CarCreateUpdateDto dto) => base.Post(dto);

    /// <summary>
    /// Обновить данные автомобиля
    /// </summary>
    [HttpPut("{id}")]
    public override ActionResult<CarDto> Update(CarCreateUpdateDto dto, int id) => base.Update(dto, id);

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => base.Delete(id);
}
