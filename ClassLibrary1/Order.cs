namespace RealEstateAgency.Domain;

/// <summary>
/// заявка клиента
/// </summary>
public record class Order
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Время заявки
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Тип заявки (покупка или продажа)
    /// </summary>
    public PurchaseOrSale Type { get; set; }

    /// <summary>
    /// Цена недвижимости
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Объект недвижимости
    /// </summary>
    public RealEstate Item { get; set; }

    /// <summary>
    /// Перечисление для типа заявки (покупка или продажа)
    /// </summary>
    public enum PurchaseOrSale { Purchase, Sale }

    /// <summary>
    /// Конструктор для создания заявки
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <param name="time">Время заявки</param>
    /// <param name="type">Тип заявки (покупка или продажа)</param>
    /// <param name="price">Цена недвижимости</param>
    /// <param name="item">Объект недвижимости</param>
    public Order(int id, DateTime time, PurchaseOrSale type, decimal price, RealEstate item)
    {
        Id = id;
        Time = time;
        Type = type;
        Price = price;
        Item = item;
    }
}
