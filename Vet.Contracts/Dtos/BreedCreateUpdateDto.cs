using System.ComponentModel.DataAnnotations;
using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO породы для POST/PUT запросов
/// </summary>
/// <param name="Species">Вид животного</param>
/// <param name="Name">Название</param>
public sealed record BreedCreateUpdateDto(
    [Required(ErrorMessage = "Вид животного обязателен")]
    [EnumDataType(typeof(Species), ErrorMessage = "Указан недопустимый вид животного")]
    Species Species,

    [Required(ErrorMessage = "Название породы обязательно для заполнения")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина названия породы должна быть от 1 до 100 символов")]
    string Name);