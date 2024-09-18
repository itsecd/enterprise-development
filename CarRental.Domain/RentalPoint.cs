namespace CarRental.Domain;

/// <summary>
/// Реализация пункта проката
/// </summary>
/// <param name="name">Название пункта</param>
/// <param name="address">Адрес пункта</param>
public class RentalPoint(string name, string address)
{
    /// <summary>
    /// Название пункта проката
    /// </summary>
    public string? Name { get; set; } = name;
    /// <summary>
    /// Адрес пункта проката
    /// </summary>
    public string? Address { get; set; } = address;
}