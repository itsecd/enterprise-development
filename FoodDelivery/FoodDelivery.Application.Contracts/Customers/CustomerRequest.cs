using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Customers;

/// <summary>
/// Данные для создания или изменения клиента
/// </summary>
public sealed class CustomerRequest
{
    /// <summary>
    /// Фамилия клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите фамилию клиента")]
    public required string LastName { get; init; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите имя клиента")]
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите номер телефона")]
    [Phone(ErrorMessage = "Укажите корректный номер телефона")]
    public required string Phone { get; init; }

    /// <summary>
    /// Адрес доставки клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите адрес доставки")]
    public required string DeliveryAddress { get; init; }
}
