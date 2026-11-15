namespace Museum.Domain;

/// <summary>
/// Посетитель музея
/// </summary>
public class Visitor
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО посетителя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// номер
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Др посетителя
    /// </summary>
    public DateTime BirthDate { get; set; }
}