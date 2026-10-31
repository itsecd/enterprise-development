namespace CarWash.Application.Contracts.Customers;

/// <summary>
/// DTO для GET запросов к клиентам
/// </summary>
public record CarWashCustomerDto(
    int Id,
    string LastName,
    string FirstName,
    string? Patronymic,
    string Phone) : IHasId<int>;
