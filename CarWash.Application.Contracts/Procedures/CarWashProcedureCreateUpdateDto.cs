using CarWash.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Procedures;

/// <summary>
/// DTO для POST/PUT запросов к услугам автомойки
/// </summary>
public record CarWashProcedureCreateUpdateDto(
    [property: Required(ErrorMessage = "Название процедуры обязательно")]
    [property: StringLength(250, ErrorMessage = "Название процедуры не должно превышать 250 символов")]
    string Name,

    [property: EnumDataType(typeof(CarCategory))]
    CarCategory CarCategory,

    [property: Range(typeof(decimal), "0.01", "100000", ErrorMessage = "Стоимость процедуры должна быть больше нуля")]
    decimal Price,

    TimeSpan Duration);