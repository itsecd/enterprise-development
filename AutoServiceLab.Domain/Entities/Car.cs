namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Автомобиль клиента автосервиса
/// </summary>
public class Car
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Гос. номер автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// Год выпуска автомобиля
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Идентификатор клиента автосервиса
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент автосервиса
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Заказы на работы с автомобилем
    /// </summary>
    public List<ServiceOrder> Orders { get; set; } = [];

    /// <summary>
    /// Возвращает строковое представление автомобиля
    /// </summary>
    public override string ToString() => $"{Brand} {Model} ({Year})";
}
