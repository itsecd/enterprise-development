namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO ветеринара для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО</param>
/// <param name="Specialization">DTO специализации</param>
/// <param name="BirthYear">Год рождения</param>
/// <param name="WorkExperience">Опыт работы</param>
public sealed record VeterinarianDto(int Id, string PassportNumber, string FullName, SpecializationDto Specialization, int BirthYear, int WorkExperience);