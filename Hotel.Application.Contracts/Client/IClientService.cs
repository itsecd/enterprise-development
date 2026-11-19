namespace Hotel.Application.Contracts.Client;
/// <summary>
/// Наследний интерфейса служды для CRUD операций над клиентами
/// </summary>
public interface IClientService :
    IService<ClientResponseDto, ClientCreateUpdateDto>
{ }