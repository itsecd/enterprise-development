namespace CarRental.Domain;

/// <summary>
/// Реализация транспортного средства
/// </summary>
/// <param name="carNumber">Номер автомобиля</param>
/// <param name="model">Модель автомобиля</param>
/// <param name="color">Цвет автомобиля</param>
public class Vehicle(string carNumber, string model, string color)
{
    /// <summary>
    /// Номер автомобиля
    /// </summary>
    public string? CarNumber { get; set; } = carNumber;
    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public string? Model { get; set; } = model;
    /// <summary>
    /// Цвет автомобиля
    /// </summary>
    public string? Color { get; set; } = color;
}
