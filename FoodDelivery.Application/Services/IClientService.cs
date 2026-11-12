using FoodDelivery.Contracts.Clients;

namespace FoodDelivery.Application.Services;

public interface IClientService
{
    public IReadOnlyList<ClientDto> GetAll();
    public ClientDto? GetById(int id);
    public ClientDto Create(ClientCreateUpdateDto dto);
    public bool Update(int id, ClientCreateUpdateDto dto);
    public void Delete(int id);
}