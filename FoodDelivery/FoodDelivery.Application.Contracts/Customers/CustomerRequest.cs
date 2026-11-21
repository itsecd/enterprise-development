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
    [StringLength(100)]
    public required string LastName { get; init; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите имя клиента")]
    [StringLength(100)]
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество клиента
    /// </summary>
    [StringLength(100)]
    public string? Patronymic { get; init; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите номер телефона")]
    [Phone(ErrorMessage = "Укажите корректный номер телефона")]
    [StringLength(30)]
    public required string Phone { get; init; }

    /// <summary>
    /// Адрес доставки клиента
    /// </summary>
    [Required(ErrorMessage = "Укажите адрес доставки")]
    [StringLength(255)]
    public required string DeliveryAddress { get; init; }
}
