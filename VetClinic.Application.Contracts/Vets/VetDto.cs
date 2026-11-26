namespace VetClinic.Application.Contracts.Vets;

/// <summary>
/// DTO исходящих данных для отображения карточки ветеринарного врача
/// </summary>
/// <param name="Id">Id врача</param>
/// <param name="FullName">ФИО врача</param>
/// <param name="ExpirienceYears">Стаж работы</param>
/// <param name="SpecializationName">Название врачебной специализации</param>
public record VetDto(int Id, string FullName, int ExpirienceYears, string SpecializationName);

