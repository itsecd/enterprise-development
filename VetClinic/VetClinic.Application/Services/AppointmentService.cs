using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис записей на прием, проверяет, что питомец и врач существуют
/// </summary>
/// <param name="appointments">Репозиторий записей на прием</param>
/// <param name="pets">Репозиторий питомцев</param>
/// <param name="vets">Репозиторий ветеринаров</param>
public class AppointmentService(
    IAppointmentRepository appointments,
    IPetRepository pets,
    IVetRepository vets) : IAppointmentService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await appointments.GetAllAsync(cancellationToken);
        return [.. all.Select(a => a.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await appointments.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto> CreateAsync(AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Pet = await GetPetAsync(dto.PetId, cancellationToken);
        entity.Vet = await GetVetAsync(dto.VetId, cancellationToken);
        var created = await appointments.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto?> UpdateAsync(int id, AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await appointments.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Pet = await GetPetAsync(dto.PetId, cancellationToken);
        entity.Vet = await GetVetAsync(dto.VetId, cancellationToken);
        entity.Apply(dto);
        await appointments.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await appointments.DeleteAsync(id, cancellationToken);

    /// <summary>
    /// Найти питомца или сообщить, что его нет
    /// </summary>
    /// <param name="petId">Идентификатор питомца</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="KeyNotFoundException">Питомец не найден</exception>
    private async Task<Pet> GetPetAsync(int petId, CancellationToken cancellationToken) =>
        await pets.GetByIdAsync(petId, cancellationToken)
        ?? throw new KeyNotFoundException($"Питомец с идентификатором {petId} не найден");

    /// <summary>
    /// Найти ветеринара или сообщить, что его нет
    /// </summary>
    /// <param name="vetId">Идентификатор ветеринара</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="KeyNotFoundException">Ветеринар не найден</exception>
    private async Task<Vet> GetVetAsync(int vetId, CancellationToken cancellationToken) =>
        await vets.GetByIdAsync(vetId, cancellationToken)
        ?? throw new KeyNotFoundException($"Ветеринар с идентификатором {vetId} не найден");
}
