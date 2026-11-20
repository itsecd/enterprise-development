using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Owners;

/// <summary>
/// Операции сервиса владельцев
/// </summary>
public interface IOwnerService
{
    /// <summary>
    /// Возвращает список владельцев животных.
    /// </summary>
    public Task<IReadOnlyList<OwnerDto>> OwnersAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит владельца по идентификатору.
    /// </summary>
    public Task<OwnerDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт владельца на основе переданных данных.
    /// </summary>
    public Task<OwnerDto> CreateAsync(SaveOwnerRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет владельца по идентификатору.
    /// </summary>
    public Task<OwnerDto> UpdateAsync(int id, SaveOwnerRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет владельца по идентификатору.
    /// </summary>
    public Task DeleteOwnerAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает владельцев, у которых зарегистрировано несколько животных.
    /// </summary>
    public Task<IReadOnlyList<OwnerDto>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает животных указанного владельца.
    /// </summary>
    public Task<IReadOnlyList<PetDto>> PetsByOwnerAsync(int id, CancellationToken cancellationToken = default);
}
