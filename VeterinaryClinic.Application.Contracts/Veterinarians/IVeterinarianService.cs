namespace VeterinaryClinic.Application.Contracts.Veterinarians;

/// <summary>
/// Операции сервиса ветеринаров
/// </summary>
public interface IVeterinarianService
{
    /// <summary>
    /// Возвращает всех ветеринаров
    /// </summary>
    public IReadOnlyList<VeterinarianDto> Veterinarians();

    /// <summary>
    /// Возвращает ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public VeterinarianDto? Get(int id);

    /// <summary>
    /// Создаёт ветеринара
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public VeterinarianDto Create(SaveVeterinarianRequest request);

    /// <summary>
    /// Обновляет ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public VeterinarianDto Update(int id, SaveVeterinarianRequest request);

    /// <summary>
    /// Удаляет ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteVeterinarian(int id);

    /// <summary>
    /// Возвращает ветеринаров по виду животного
    /// </summary>
    /// <param name="species">Вид животного</param>
    public IReadOnlyList<VeterinarianDto> VeterinariansBySpecies(VeterinaryClinic.Domain.Shared.Enums.AnimalType species);

    /// <summary>
    /// Возвращает питомцев ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<PetDto> PetsByVeterinarian(int id);

    /// <summary>
    /// Возвращает приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<AppointmentDto> AppointmentsByVeterinarian(int id);
}
