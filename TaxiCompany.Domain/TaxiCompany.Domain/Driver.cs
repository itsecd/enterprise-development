namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Водитель</c> хранит информацию о водителе
/// </summary>
public class Driver
{
    /// <value>Идентификатор водителя</value>
    public required int Id { get; set; }
    /// <value>Имя и фамилия</value>
    public required string FullName { get; set; }
    /// <value>Номер телефона</value>
    public required string PhoneNumber { get; set; }
    /// <value>Паспортные данные</value>
    public required string Passport { get; set; }
    /// <value>Адрес</value>
    public required string Address { get; set; }
    /// <value>Идентификатор закрепленного автомобиля</value>
    public required int AssignedCarId { get; set; }
}
