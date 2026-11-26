namespace VetClinic.Application.Contracts.Vets;

/// <summary>
/// DTO входящих данных для найма или редактирования анкеты ветеринарного врача
/// </summary>
/// <param name="FullName">ФИО врача</param>
/// <param name="PassportNumber">Серия и номер паспорта сотрудника</param>
/// <param name="BirthDate">Дата рождения врача</param>
/// <param name="ExpirienceYears">Стаж работы</param>
/// <param name="SpecializationId">Идентификатор профильной специализации</param>
public record VetCreateUpdateDto(string FullName, string PassportNumber, DateOnly BirthDate, int ExpirienceYears, int SpecializationId);