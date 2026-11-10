using System.ComponentModel.DataAnnotations;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO создания и обновления записи на прием для POST/PUT запросов
/// </summary>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="VeterinarianId">Идентификатор ветеринара</param>
/// <param name="AppointmentDateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Флаг повторного приема</param>
public sealed record AppointmentCreateUpdateDto(
    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор питомца должен быть больше 0")]
    int PetId,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор ветеринара должен быть больше 0")]
    int VeterinarianId,

    [property: Required(ErrorMessage = "Дата и время приёма обязательны для заполнения")]
    DateTime AppointmentDateTime,

    [property: Range(1, 9999, ErrorMessage = "Номер кабинета должен быть положительным числом")]
    int RoomNumber,

    bool IsRepeat);