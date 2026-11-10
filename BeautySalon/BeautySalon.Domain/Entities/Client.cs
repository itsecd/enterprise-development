namespace BeautySalon.Domain.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Client : Person
{

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly Birthday {  get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}
