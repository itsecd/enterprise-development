namespace BikeRent.Domain;
/// <summary>
/// Клиент сервиса
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    required public  int Id { get; set; }
    /// <summary>
    /// Имя клиента
    /// </summary>
    required public  string FirstName { get; set; }
    /// <summary>
    /// Фамилия Клиента
    /// </summary>
    required public string SecondName { get; set; }
    /// <summary>
    /// Отчество клиента
    /// </summary>
    required public string ThirdName { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    required public DateTime BirthDate { get; set; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    required public string PhoneNumber { get; set; }
}