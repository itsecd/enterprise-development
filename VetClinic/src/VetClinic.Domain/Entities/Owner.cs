using VetClinic.Domain.Interfaces;

namespace VetClinic.Domain.Entities;

/// <summary>
/// Владелец животного
/// </summary>
public class Owner : IEntity
{
    /// <summary>
    /// Уникальный идентификатор владельца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество владельца
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Контактный номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес проживания владельца
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Список питомцев, принадлежащих владельцу
    /// </summary>
    public List<Pet> Pets { get; set; } = [];
}