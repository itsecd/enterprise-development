namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// DTO для отображения общей стоимости проживания клиента
/// </summary>
/// <param name="Client">Данные клиента</param>
/// <param name="TotalStayCost">Общая стоимость проживания</param>
public record HotelClientStayCostDto(
    HotelClientDto Client,
    decimal TotalStayCost
);
