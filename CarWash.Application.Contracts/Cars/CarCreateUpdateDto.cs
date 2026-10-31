using CarWash.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Cars;

/// <summary>
/// DTO для POST/PUT запросов к авто
/// </summary>
public record CarCreateUpdateDto(
    [property: EnumDataType(typeof(CarCategory))]
    CarCategory CarCategory,

    [property: Required(ErrorMessage = "Гос номер авто обязателен")]
    [property: RegularExpression(@"^[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}$", 
        ErrorMessage = "Укажите корректный госномер (например, А111АА)")]
    string LicensePlate,

    [property: Required(ErrorMessage = "Марка авто обязательна")]
    string Brand,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор клиента должен быть положительным")]
    int CustomerId);