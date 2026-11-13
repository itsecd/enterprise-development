namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Операции сервиса специализаций
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Возвращает все специализации
    /// </summary>
    public IReadOnlyList<SpecializationDto> Specializations();

    /// <summary>
    /// Возвращает специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public SpecializationDto? Get(int id);

    /// <summary>
    /// Создаёт специализацию
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public SpecializationDto Create(SaveSpecializationRequest request);

    /// <summary>
    /// Обновляет специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public SpecializationDto Update(int id, SaveSpecializationRequest request);

    /// <summary>
    /// Удаляет специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteSpecialization(int id);

    /// <summary>
    /// Возвращает ветеринаров по специализации
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecialization(int id);
}
