namespace StoreCashFlow.Domain;

/// <summary>
/// Покупатель
/// </summary>
public class Customer
{
    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    public required int CustomerId { get; set; }
    /// <summary>
    /// Номер карты
    /// </summary>
    public required string CardNumber { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName{ get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public required string Potronimic { get; set; }
}
