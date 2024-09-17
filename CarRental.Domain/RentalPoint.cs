namespace CarRental.Domain;

/// <summary>
/// Реализация пункта проката
/// </summary>
/// <param name="name">Название пункта</param>
/// <param name="address">Адрес пункта</param>
public class RentalPoint(string name, string address)
{
    public string? Name { get; set; } = name;
    public string? Address { get; set; } = address;
}