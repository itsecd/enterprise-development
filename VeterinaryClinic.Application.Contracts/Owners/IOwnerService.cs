namespace VeterinaryClinic.Application.Contracts.Owners;

/// <summary>
/// Операции сервиса владельцев
/// </summary>
public interface IOwnerService
{
    /// <summary>
    /// Возвращает всех владельцев
    /// </summary>
    public IReadOnlyList<OwnerDto> Owners();

    /// <summary>
    /// Возвращает владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public OwnerDto? Get(int id);

    /// <summary>
    /// Создаёт владельца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public OwnerDto Create(SaveOwnerRequest request);

    /// <summary>
    /// Обновляет владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public OwnerDto Update(int id, SaveOwnerRequest request);

    /// <summary>
    /// Удаляет владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteOwner(int id);

    /// <summary>
    /// Возвращает владельцев нескольких питомцев
    /// </summary>
    public IReadOnlyList<OwnerDto> OwnersWithMultiplePets();

    /// <summary>
    /// Возвращает питомцев владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByOwner(int id);
}
