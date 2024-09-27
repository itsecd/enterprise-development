namespace DispatchService.Model;

/// <summary>
/// Водитель
/// </summary>
public class Driver
{
    /// <summary>
    /// Идетификатор водителя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// Номер лицензии водителя
    /// </summary>
    public required string DriverLicense { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

    //public Driver(int _ID, string fullName, string passport, string driverLicense, string address, string phoneNumber)
    //{
    //    Id = _ID;
    //    FullName = fullName;
    //    Passport = passport;
    //    DriverLicense = driverLicense;
    //    Address = address;
    //    PhoneNumber = phoneNumber;
    //}
}