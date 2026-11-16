namespace CarWash.Domain.Entities;

/// <summary>
/// Клиент автомойки
/// </summary>
public class CarWashCustomer
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Автомобили клиента
    /// </summary>
    public List<Car> Cars { get; set; } = [];

    /// <summary>
    /// Заказы клиента
    /// </summary>
    public List<Order> Orders { get; set; } = [];
}