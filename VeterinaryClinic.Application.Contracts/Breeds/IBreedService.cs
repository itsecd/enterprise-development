namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Операции сервиса пород
/// </summary>
public interface IBreedService
{
    /// <summary>
    /// Возвращает все породы
    /// </summary>
    public IReadOnlyList<BreedDto> Breeds();

    /// <summary>
    /// Возвращает породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public BreedDto? Get(int id);

    /// <summary>
    /// Создаёт породу
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public BreedDto Create(SaveBreedRequest request);

    /// <summary>
    /// Обновляет породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public BreedDto Update(int id, SaveBreedRequest request);

    /// <summary>
    /// Удаляет породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteBreed(int id);

    /// <summary>
    /// Считает повторные приёмы по породе
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    public int RepeatedAppointments(int breedId);

    /// <summary>
    /// Возвращает питомцев по породе
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByBreed(int id);
}
