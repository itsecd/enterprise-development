namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// DTO для отображения общей стоимости проживания клиента
/// </summary>
public record HotelClientStayCostDto(
    HotelClientDto Client,
    decimal TotalStayCost
);
