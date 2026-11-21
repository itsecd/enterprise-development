namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO клиента
/// </summary>
/// <param name="Id">Уникальный идентификатор клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента, если оно указано</param>
/// <param name="Phone">Номер телефона клиента</param>
/// <param name="DeliveryAddress">Адрес доставки клиента</param>
public record CustomerDto(
    int Id,
    string LastName,
    string FirstName,
    string? Patronymic,
    string Phone,
    string DeliveryAddress
);
