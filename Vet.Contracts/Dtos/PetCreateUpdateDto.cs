using System.ComponentModel.DataAnnotations;
using Vet.Domain.Enums;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO создания и обновления питомца для POST/PUT запросов
/// </summary>
/// <param name="Name">Кличка</param>
/// <param name="Species">Вид животного</param>
/// <param name="BreedId">Порода животного</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="BodyWeight">Масса тела</param>
/// <param name="OwnerId">Идентификатор владельца</param>
public sealed record PetCreateUpdateDto(
    [Required(ErrorMessage = "Кличка питомца обязательна для заполнения")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Кличка должна быть от 1 до 100 символов")]
    string Name,

    [Required(ErrorMessage = "Вид животного обязателен")]
    [EnumDataType(typeof(Species), ErrorMessage = "Указан недопустимый вид животного")]
    Species Species,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор породы должен быть больше 0")]
    int BreedId,

    DateOnly? BirthDate,

    [Range(0.01, 500.0, ErrorMessage = "Масса тела должна быть в диапазоне от 0.01 до 500.0 кг")]
    double? BodyWeight,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор владельца должен быть больше 0")]
    int OwnerId);