using Hotel.Application.Contracts.Client;
using Hotel.Application.Mappers;

namespace Hotel.Application.Services;

/// <summary>
/// Реализация службы CRUD операций для клиентов
/// </summary>
/// <param name="_clients">Репозиторий клиентов</param>
public class ClientService(IClientRepository _clients) : IClientService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var clients = await _clients.ReadAll(ct);
        return clients.Select(c => ClientMapper.ToDto(c));
    }
    
    /// <inheritdoc/>
    public async Task<ClientResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var client = await _clients.Read(id, ct);
        return client is null ? null : ClientMapper.ToDto(client);
    }
    
    /// <inheritdoc/>
    public async Task<ClientResponseDto> CreateAsync(ClientCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Name: string,
                Surname: string,
                BirthDate: DateOnly,
                PassportNumber: string,
                Citizenship: string
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var client = ClientMapper.ToEntity(dto);

        var respose = await _clients.Create(client, ct);
        return ClientMapper.ToDto(respose);
    }
    
    /// <inheritdoc/>
    public async Task<ClientResponseDto?> UpdateAsync(int id, ClientCreateUpdateDto dto, CancellationToken ct = default)
    {
        var client = await _clients.Read(id, ct);
        if (client is null) return null;

        if (dto.Name is not null) client.Name = dto.Name;
        if (dto.Surname is not null) client.Surname = dto.Surname;
        if (dto.Patronymic is not null) client.Patronymic = dto.Patronymic;
        if (dto.PassportNumber is not null) client.PassportNumber = dto.PassportNumber;
        if (dto.BirthDate is not null) client.BirthDate = dto.BirthDate.Value;
        if (dto.Citizenship is not null) client.Citizenship = dto.Citizenship;

        await _clients.Update(client, ct);
        return ClientMapper.ToDto(client);
    }
    
    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _clients.Delete(id, ct);
}