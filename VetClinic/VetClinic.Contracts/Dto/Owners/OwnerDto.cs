namespace VetClinic.Contracts.Dto.Owners;

/// <summary>
/// Владелец питомца
/// </summary>
public class OwnerDto
{
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// ФИО владельца
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// Адрес проживания
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public required string Phone { get; init; }
}
