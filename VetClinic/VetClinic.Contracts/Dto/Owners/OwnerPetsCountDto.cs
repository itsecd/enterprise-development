namespace VetClinic.Contracts.Dto.Owners;

/// <summary>
/// Владелец питомца вместе с количеством его питомцев
/// </summary>
public class OwnerPetsCountDto
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
    /// Количество питомцев у владельца
    /// </summary>
    public required int PetsCount { get; init; }
}
