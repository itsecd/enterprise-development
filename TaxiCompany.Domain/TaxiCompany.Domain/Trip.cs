namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Поездка</c> хранит информацию о поездке
/// </summary>
public class Trip
{
    /// <value>Идентификатор поездки</value>
    public required int Id { get; set; }
    /// <value>Пункт отправления</value>
    public required string Departure { get; set; }
    /// <value>Пункт назначения</value>
    public required string Destination { get; set; }
    /// <value>Дата поездки</value>
    public required DateTime Date { get; set; }
    /// <value>Время в движении</value>
    public TimeOnly DrivingTime { get; set; }
    /// <value>Стоимость</value>
    public required decimal Cost { get; set; }
    /// <value>Идентификатор клиента</value>
    public required int AssignedClientId { get; set; }
    /// <value>Идентификатор автомобиля</value>
    public required int AssignedCarId { get; set; }
}
