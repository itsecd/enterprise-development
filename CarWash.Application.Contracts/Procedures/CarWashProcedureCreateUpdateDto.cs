using CarWash.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Procedures;

/// <summary>
/// DTO для POST/PUT запросов к услугам автомойки
/// </summary>
public record CarWashProcedureCreateUpdateDto(
    [param: Required(ErrorMessage = "Название процедуры обязательно")]
    [param: StringLength(250, ErrorMessage = "Название процедуры не должно превышать 250 символов")]
    string Name,

    [param: EnumDataType(typeof(CarCategory))]
    CarCategory CarCategory,

    [param: Range(
        typeof(decimal), "0.01", "100000",
        ParseLimitsInInvariantCulture = true,
        ErrorMessage = "Стоимость процедуры должна быть больше нуля")]
    decimal Price,

    TimeSpan Duration);