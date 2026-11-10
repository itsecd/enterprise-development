using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления записями на приём
/// </summary>
public class AppointmentService(
    IAppointmentRepository repository,
    IClientRepository clientRepository,
    IMasterRepository masterRepository,
    IServiceRepository serviceRepository) : IAppointmentService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return [.. entities.Select(e => e.ToDto())];
    }

    /// <inheritdoc />
    public async Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Запись на приём", id);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task<AppointmentDto> CreateAsync(AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        var (client, master, service) = await ResolveReferencesAsync(dto, cancellationToken);

        var created = await repository.AddAsync(dto.ToEntity(client, master, service), cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<AppointmentDto> UpdateAsync(
        int id, AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Запись на приём", id);

        // Сначала находим все связи: при ошибке существующая запись не должна измениться частично
        var (client, master, service) = await ResolveReferencesAsync(dto, cancellationToken);

        entity.UpdateEntity(dto, client, master, service);
        await repository.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await repository.DeleteAsync(id, cancellationToken))
        {
            throw new NotFoundException("Запись на приём", id);
        }
    }

    /// <summary>
    /// Находит клиента, мастера и услугу, на которые ссылается запись
    /// </summary>
    /// <exception cref="ReferenceNotFoundException">Одной из связанных сущностей не существует</exception>
    private async Task<(Client Client, Master Master, Service Service)> ResolveReferencesAsync(
        AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        var clientId = dto.ClientId!.Value;
        var masterId = dto.MasterId!.Value;
        var serviceId = dto.ServiceId!.Value;

        var client = await clientRepository.GetByIdAsync(clientId, cancellationToken)
            ?? throw new ReferenceNotFoundException("Клиент", clientId);
        var master = await masterRepository.GetByIdAsync(masterId, cancellationToken)
            ?? throw new ReferenceNotFoundException("Мастер", masterId);
        var service = await serviceRepository.GetByIdAsync(serviceId, cancellationToken)
            ?? throw new ReferenceNotFoundException("Услуга", serviceId);

        return (client, master, service);
    }
}