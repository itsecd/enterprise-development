using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;

namespace AutoServiceLab.Application.Contracts.Interfaces;

public interface IMechanicService : IApplicationService<MechanicDto, MechanicCreateUpdateDto, int>
{
    /// <summary>
    /// Получить заказы, которые выполнял механик
    /// </summary>
    public Task<IList<ServiceOrderDto>> GetOrdersByMechanic(int mechanicId);
}
