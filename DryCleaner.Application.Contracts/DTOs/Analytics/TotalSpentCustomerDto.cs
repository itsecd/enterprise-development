namespace DryCleaner.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Информация об общей сумме заказов клиента
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента</param>
/// <param name="TotalSpentAmount">Сумма заказов</param>
public record TotalSpentCustomerDto(
    int CustomerId,
    string LastName,
    string FirstName,
    string? Patronymic,
    decimal TotalSpentAmount);