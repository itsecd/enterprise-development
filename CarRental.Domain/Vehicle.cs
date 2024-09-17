namespace CarRental.Domain;

/// <summary>
/// Реализация транспортного средства
/// </summary>
/// <param name="carNumber">Номер автомобиля</param>
/// <param name="model">Модель автомобиля</param>
/// <param name="color">Цвет автомобиля</param>
public class Vehicle(string carNumber, string model, string color)
{
    public string? CarNumber { get; set; } = carNumber;
    public string? Model { get; set; } = model;
    public string? Color { get; set; } = color;
}
