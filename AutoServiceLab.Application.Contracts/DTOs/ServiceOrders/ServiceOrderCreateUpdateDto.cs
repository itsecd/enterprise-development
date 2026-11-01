namespace AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;

/// <summary>
/// DTO для создания или обновления заказа
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="CarId">Идентификатор автомобиля</param>
/// <param name="IssueDate">Дата выдачи автомобиля (опционально)</param>
public record ServiceOrderCreateUpdateDto(
    int ClientId,
    int CarId,
    DateTime? IssueDate
);
