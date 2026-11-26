namespace VetClinic.Application.Contracts.Services;

/// <summary>
/// DTO исходящих данных для отображения информации об услуге
/// </summary>
/// <param name="Id">Id услуги</param>
/// <param name="Title">Название услуги</param>
/// <param name="Price">Стоимость услуги</param>
/// <param name="SpecializationName">Название профиля услуги</param>
public record ServiceDto(int Id, string Title, decimal Price, string SpecializationName);

