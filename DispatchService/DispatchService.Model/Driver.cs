namespace DispatchService.Model;

/// <summary>
/// Водитель
/// </summary>
public class Driver
{
    /// <summary>
    /// Индетификатор водителя
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public string Passport { get; set; }

    /// <summary>
    /// Номер лицензии водителя
    /// </summary>
    public string DriverLicense { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

    public Driver(int _ID, string fullName, string passport, string driverLicense, string address, string phoneNumber)
    {
        ID = _ID;
        FullName = fullName;
        Passport = passport;
        DriverLicense = driverLicense;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}