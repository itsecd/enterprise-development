namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Авто</c> хранит информацию о характеристиках автомобиля
/// </summary>
public class Car
{
    /// <value>Идентификатор авто</value>
    public required int Id { get; set; }
    /// <value>Цвет</value>
    public required string Colour { get; set; }
    /// <value>Модель</value>
    public required string Model { get; set; }
    /// <value>Серийный номер</value>
    public required string SerialNumber { get; set; }
    /// <value>Год выпуска</value>
    public required DateTime RealeseYear { get; set; }
    /// <value>Идентификатор водителя</value>
    public required int AssignedDriverId { get; set; }
}
