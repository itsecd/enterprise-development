namespace DispatchTransportControl.Domain;

/// <summary>
/// Водитель
/// </summary>
public class Driver
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public required string Patronymic { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// Водительское удостоверение
    /// </summary>
    public required string DriverLicense { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public required string Phone { get; set; }
}