namespace VeterinaryClinic.Application.Contracts.Pets;

/// <summary>
/// Операции сервиса питомцев
/// </summary>
public interface IPetService
{
    /// <summary>
    /// Возвращает всех питомцев
    /// </summary>
    public IReadOnlyList<PetDto> Pets();

    /// <summary>
    /// Возвращает питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public PetDto? Get(int id);

    /// <summary>
    /// Создаёт питомца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public PetDto Create(SavePetRequest request);

    /// <summary>
    /// Обновляет питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public PetDto Update(int id, SavePetRequest request);

    /// <summary>
    /// Удаляет питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeletePet(int id);

    /// <summary>
    /// Возвращает владельца питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public OwnerDto? OwnerByPet(int id);

    /// <summary>
    /// Возвращает приёмы питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<AppointmentDto> AppointmentsByPet(int id);
}
