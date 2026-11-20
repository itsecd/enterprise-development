using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Breeds;

/// <summary>
/// Операции сервиса пород
/// </summary>
public interface IBreedService
{
    /// <summary>
    /// Возвращает список пород животных.
    /// </summary>
    public Task<IReadOnlyList<BreedDto>> BreedsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит породу по идентификатору.
    /// </summary>
    public Task<BreedDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт породу на основе переданных данных.
    /// </summary>
    public Task<BreedDto> CreateAsync(SaveBreedRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет породу по идентификатору.
    /// </summary>
    public Task<BreedDto> UpdateAsync(int id, SaveBreedRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет породу по идентификатору.
    /// </summary>
    public Task DeleteBreedAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Подсчитывает повторные приёмы животных указанной породы.
    /// </summary>
    public Task<int> RepeatedAppointmentsAsync(int breedId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает животных указанной породы.
    /// </summary>
    public Task<IReadOnlyList<PetDto>> PetsByBreedAsync(int id, CancellationToken cancellationToken = default);
}
