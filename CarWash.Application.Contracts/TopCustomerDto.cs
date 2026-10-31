namespace CarWash.Application.Contracts;

/// <summary>
/// DTO для представления клиента в топе посещений
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="VisitCount">Количество посещений автомойки</param>
public record TopCustomerDto(
    int CustomerId,
    int VisitCount);