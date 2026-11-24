namespace Carwash.Application.Contracts.Orders;

/// <summary>
/// Dto для Create/Update запросов к заказам
/// </summary>
/// <param name="ClientId">Идентификатор клиента, сделавшего заказ</param>
/// <param name="CarId">Идентификатор машины, для которой оформляется заказ</param>
/// <param name="ServiceId">Идентификатор услуги, для которой оформляется заказ</param>
/// <param name="BoxNumber">Номер бокса, в котором будет выполняться заказ</param>
/// <param name="StartTime">Время начала заказа</param>
public record OrderCreateUpdateDto
(
    int ClientId,
    int CarId,
    int ServiceId,
    int BoxNumber,
    DateTime StartTime
);
