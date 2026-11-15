namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о посетителе для ответа API
/// </summary>
public sealed class VisitorDto
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// ФИО посетителя
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    public required string Phone { get; init; }

    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public DateTime BirthDate { get; init; }
}
