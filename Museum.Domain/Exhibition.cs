using Museum.Shared;

namespace Museum.Domain;

/// <summary>
/// Выставка музея
/// </summary>
public class Exhibition
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тематика выставки
    /// </summary>
    public ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Номер зала, где проходит выставка
    /// </summary>
    public int HallNumber { get; set; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public DateTime EndDate { get; set; }
}
