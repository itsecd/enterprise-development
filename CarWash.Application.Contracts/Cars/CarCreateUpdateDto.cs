using CarWash.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Cars;

/// <summary>
/// DTO для POST/PUT запросов к авто
/// </summary>
public record CarCreateUpdateDto(
    [param: EnumDataType(typeof(CarCategory))]
    CarCategory CarCategory,

    [param: Required(ErrorMessage = "Гос номер авто обязателен")]
    [param: RegularExpression(@"^[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}$", 
        ErrorMessage = "Укажите корректный госномер (например, А111АА)")]
    string LicensePlate,

    [param: Required(ErrorMessage = "Марка авто обязательна")]
    string Brand,

    [param: Range(1, int.MaxValue, ErrorMessage = "Идентификатор клиента должен быть положительным")]
    int CustomerId);