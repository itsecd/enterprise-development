using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба для CRUD операций c клиентами
/// </summary>
/// <param name="clientRepository">Репозиторий клиентов</param>
public class ClientService(IRepository<Client, int> clientRepository) : IApplicationService<ClientDto, ClientCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var client = Mapper.Map(dto);

        var result = await clientRepository.Create(client);

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await clientRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<ClientDto?> Get(int id)
    {
        var client = await clientRepository.Read(id);

        if (client == null)
            return null;

        return Mapper.Map(client);
    }

    /// <inheritdoc/>
    public async Task<IList<ClientDto>> GetAll() => Mapper.Map(await clientRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<ClientDto> Update(ClientCreateUpdateDto dto, int id)
    {
        var client = Mapper.Map(dto);
        client.Id = id;

        var result = await clientRepository.Update(client);
        return Mapper.Map(result);
    }
}
