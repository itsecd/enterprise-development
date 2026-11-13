namespace BeautySalon.Domain.Models;

/// <summary>
/// Клиент салона красоты.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Количество баллов в программе лояльности.
    /// </summary>
    public required int LoyaltyPoints { get; set; }

    /// <summary>
    /// Строковое представление
    /// </summary>
    public override string ToString() =>
        $"Client: {base.ToString()}, {LoyaltyPoints} pts.";
}