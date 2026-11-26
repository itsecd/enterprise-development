namespace VetClinic.Domain.Models;

/// <summary>
/// Владелец питомца
/// </summary>
public class Owner
{
    /// <summary>
    /// Уникальный идентификатор владельца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО владельца
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Адрес проживания
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Список питомцев владельца
    /// </summary>
    public List<Pet> Pets { get; set; } = [];
}
