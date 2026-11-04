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
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    public string Phone { get; init; } = string.Empty;

    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public DateTime BirthDate { get; init; }
}
