namespace BikeRent.Domain;
/// <summary>
/// Тип велосипеда
/// </summary>
public class BikeType
{
    /// <summary>
    /// Идентификатор типа велосипеда
    /// </summary>
    required public int TypeId { get; set; }
    /// <summary>
    /// Стоимость аренды
    /// </summary>
    required public double Price { get; set; }
    /// <summary>
    /// Наименование типа
    /// </summary>
    required public string Name { get; set; }
}