namespace Vet.Domain.Entities;

/// <summary>
/// Владелец
/// </summary>
public sealed class Owner
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Список питомцев
    /// </summary>
    public List<Pet> Pets { get; set; } = [];
}