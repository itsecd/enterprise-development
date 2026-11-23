using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;

namespace AutoServiceLab.Application.Contracts.Interfaces;

public interface IClientService : IApplicationService<ClientDto, ClientCreateUpdateDto, int>
{
    /// <summary>
    /// Получить заказы конкретного клиента
    /// </summary>
    public IList<ServiceOrderDto> GetOrdersByClient(int clientId);
}
