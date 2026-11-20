using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Pets;

/// <summary>
/// Операции сервиса питомцев
/// </summary>
public interface IPetService
{
    /// <summary>
    /// Возвращает список животных клиники.
    /// </summary>
    public Task<IReadOnlyList<PetDto>> PetsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит животное по идентификатору.
    /// </summary>
    public Task<PetDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт животное на основе переданных данных.
    /// </summary>
    public Task<PetDto> CreateAsync(SavePetRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет животное по идентификатору.
    /// </summary>
    public Task<PetDto> UpdateAsync(int id, SavePetRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет животное по идентификатору.
    /// </summary>
    public Task DeletePetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает владельца указанного животного.
    /// </summary>
    public Task<OwnerDto?> OwnerByPetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает записи на приём указанного животного.
    /// </summary>
    public Task<IReadOnlyList<AppointmentDto>> AppointmentsByPetAsync(int id, CancellationToken cancellationToken = default);
}
