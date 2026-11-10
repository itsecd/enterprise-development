using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return [.. entities.Select(e => e.ToDto())];
    }

    /// <inheritdoc />
    public async Task<ClientDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Клиент", id);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task<ClientDto> CreateAsync(ClientEditDto dto, CancellationToken cancellationToken = default)
    {
        var created = await repository.AddAsync(dto.ToEntity(), cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<ClientDto> UpdateAsync(int id, ClientEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Клиент", id);

        entity.UpdateEntity(dto);
        await repository.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await repository.DeleteAsync(id, cancellationToken))
        {
            throw new NotFoundException("Клиент", id);
        }
    }
}