namespace VeterinaryClinic.Application.Contracts.Veterinarians;

/// <summary>
/// Данные для создания или обновления ветеринара
/// </summary>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество, если указано</param>
/// <param name="Phone">Контактный телефон</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="ExperienceYears">Стаж работы в годах</param>
public record SaveVeterinarianRequest(string LastName, string FirstName, string? Patronymic, string Phone,
    string PassportNumber,
    DateOnly BirthDate, int SpecializationId, int ExperienceYears);
