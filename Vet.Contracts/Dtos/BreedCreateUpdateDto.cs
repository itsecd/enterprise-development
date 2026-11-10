using System.ComponentModel.DataAnnotations;
using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO породы для POST/PUT запросов
/// </summary>
/// <param name="Species">Вид животного</param>
/// <param name="Name">Название</param>
public sealed record BreedCreateUpdateDto(
    [property: Required(ErrorMessage = "Вид животного обязателен")]
    [property: EnumDataType(typeof(Species), ErrorMessage = "Указан недопустимый вид животного")]
    Species Species,

    [property: Required(ErrorMessage = "Название породы обязательно для заполнения")]
    [property: StringLength(100, MinimumLength = 1, ErrorMessage = "Длина названия породы должна быть от 1 до 100 символов")]
    string Name);