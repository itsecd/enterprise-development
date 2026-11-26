namespace VetClinic.Application.Contracts.Services;

/// <summary>
/// DTO входящих данных для создания медицинской услуги или изменения информации о ней
/// </summary>
/// <param name="Title">Наименование ветеринарной услуги</param>
/// <param name="Price">Стоимость оказания услуги</param>
/// <param name="SpecializationId">Идентификатор профиля, к которому относится услуга</param>
public record ServiceCreateUpdateDto(string Title, decimal Price, int SpecializationId);
