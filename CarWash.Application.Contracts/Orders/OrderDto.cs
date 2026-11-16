namespace CarWash.Application.Contracts.Orders;

/// <summary>
/// DTO для GET запросов к заказам
/// </summary>
public record OrderDto(
    int Id,
    int CarId,
    int ProcedureId,
    DateTime StartTime,
    int WashBoxNumber) : IHasId<int>;