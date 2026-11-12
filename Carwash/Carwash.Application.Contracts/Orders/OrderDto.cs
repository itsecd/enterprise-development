using Carwash.Application.Contracts.Clients;
using Carwash.Application.Contracts.Cars;
using Carwash.Application.Contracts.Services;

namespace Carwash.Application.Contracts.Orders;

/// <summary>
/// Dto для Get запросов к заказам
/// </summary>
/// <param name="Id">Уникальный идентификатор заказа</param>
/// <param name="Client">Клиент, сделавший заказ</param>
/// <param name="Car">Машина, для которой оформляется заказ</param>
/// <param name="Service">Услуга, для которой оформляется заказ</param>
/// <param name="BoxNumber">Номер бокса, в котором будет выполняться заказ</param>
/// <param name="StartTime">Время начала заказа</param>
/// <param name="EndTime">Время окончания заказа</param>
public record OrderDto
(
    int Id,
    ClientDto Client,
    CarDto Car,
    ServiceDto Service,
    int BoxNumber,
    DateTime StartTime,
    DateTime EndTime
);
