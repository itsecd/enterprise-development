namespace VetClinic.Application.Contracts.Services;
/// <summary>
/// DTO входящих данных для создания медицинской услуги или изменения информации о ней
/// </summary>
/// <param name="Title">Наименование ветеринарной услуги</param>
/// <param name="Price">Стоимость оказания услуги</param>
/// <param name="SpecializationId">Идентификатор профиля, к которому относится услуга</param>
public record ServiceCreateUpdateDto(string Title, decimal Price, int SpecializationId);
/// <summary>
/// DTO исходящих данных для отображения информации об услуге
/// </summary>
/// <param name="Id">Id услуги</param>
/// <param name="Title">Название услуги</param>
/// <param name="Price">Стоимость услуги</param>
/// <param name="SpecializationName">Название профиля услуги</param>
public record ServiceDto(int Id, string Title, decimal Price, string SpecializationName);

