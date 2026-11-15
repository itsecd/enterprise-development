using BeautySalon.Shared.Enums;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Базовый класс для человека (клиент, мастер)
/// </summary>
public abstract class Person : IEntity
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }
}