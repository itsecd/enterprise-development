using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Specializations;

/// <summary>
/// Операции сервиса специализаций
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Возвращает список специализаций ветеринаров.
    /// </summary>
    public Task<IReadOnlyList<SpecializationDto>> SpecializationsAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Находит специализацию по идентификатору.
    /// </summary>
    public Task<SpecializationDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Создаёт специализацию на основе переданных данных.
    /// </summary>
    public Task<SpecializationDto> CreateAsync(SaveSpecializationRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Обновляет специализацию по идентификатору.
    /// </summary>
    public Task<SpecializationDto> UpdateAsync(int id, SaveSpecializationRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Удаляет специализацию по идентификатору.
    /// </summary>
    public Task DeleteSpecializationAsync(int id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Возвращает ветеринаров заданной специализации.
    /// </summary>
    public Task<IReadOnlyList<VeterinarianDto>> VeterinariansBySpecializationAsync(int id, CancellationToken cancellationToken = default);
}
