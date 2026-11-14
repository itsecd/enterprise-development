using System.ComponentModel.DataAnnotations;
using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO специализации для POST/PUT запросов
/// </summary>
/// <param name="Name">Название</param>
/// <param name="Species">Виды животных</param>
public sealed record SpecializationCreateUpdateDto(
    [Required(ErrorMessage = "Название специальности обязательно для заполнения")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название специальности должно содержать от 2 до 100 символов")]
    string Name,

    [Required(ErrorMessage = "Список целевых видов животных обязателен")]
    [MinLength(1, ErrorMessage = "Должен быть указан хотя бы один вид животного")]
    IReadOnlyList<Species> Species);