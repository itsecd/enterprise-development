using System.ComponentModel.DataAnnotations;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO ветеринара для POST/PUT запросов
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="BirthYear">Год рождения</param>
/// <param name="WorkExperience">Опыт работы (в годах)</param>
public sealed record VeterinarianCreateUpdateDto(
    [property: Required(ErrorMessage = "Номер паспорта обязателен для заполнения")]
    [property: StringLength(20, MinimumLength = 4, ErrorMessage = "Номер паспорта должен содержать от 4 до 20 символов")]
    string PassportNumber,

    [property: Required(ErrorMessage = "ФИО ветеринара обязательно для заполнения")]
    [property: StringLength(150, MinimumLength = 2, ErrorMessage = "ФИО должно содержать от 2 до 150 символов")]
    string FullName,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор специализации должен быть больше 0")]
    int SpecializationId,

    [property: Range(1900, 2015, ErrorMessage = "Год рождения должен быть в диапазоне от 1900 до 2015")]
    int BirthYear,

    [property: Range(0, 80, ErrorMessage = "Опыт работы должен быть в диапазоне от 0 до 80 лет")]
    int WorkExperience);