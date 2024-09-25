namespace BikeRent.Domain;
/// <summary>
/// Клиент сервиса
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required  int Id { get; set; }
    /// <summary>
    /// Имя клиента
    /// </summary>
    public required  string FirstName { get; set; }
    /// <summary>
    /// Фамилия Клиента
    /// </summary>
    public required string SecondName { get; set; }
    /// <summary>
    /// Отчество клиента
    /// </summary>
    public required string Patronymic { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateTime BirthDate { get; set; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}