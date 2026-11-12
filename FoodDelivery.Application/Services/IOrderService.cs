using FoodDelivery.Contracts.Orders;

namespace FoodDelivery.Application.Services;

public interface IOrderService
{
    public IReadOnlyList<OrderDto> GetAll();
    public OrderDto? GetById(int id);
    public OrderDto Create(OrderCreateUpdateDto dto);
    public bool Update(int id, OrderCreateUpdateDto dto);
    public void Delete(int id);
}