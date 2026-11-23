using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;

namespace AutoServiceLab.Application.Contracts.Interfaces;

public interface IServiceOrderService : IApplicationService<ServiceOrderDto, ServiceOrderCreateUpdateDto, int>
{
    /// <summary>
    /// Получить работы конкретного заказа
    /// </summary>
    public IList<string> GetWorksByOrder(int orderId);
}
