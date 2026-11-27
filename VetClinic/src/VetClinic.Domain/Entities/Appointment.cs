using VetClinic.Domain.Interfaces;

namespace VetClinic.Domain.Entities;

/// <summary>
/// Запись на прием к ветеринару
/// </summary>
public class Appointment : IEntity
{
    /// <summary>
    /// Уникальный идентификатор записи на прием
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время проведения приема
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Флаг, указывающий, является ли прием повторным
    /// </summary>
    public bool IsFollowUp { get; set; }

    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// Питомец
    /// </summary>
    public Pet? Pet { get; set; }

    /// <summary>
    /// Идентификатор ветеринара
    /// </summary>
    public int VeterinarianId { get; set; }

    /// <summary>
    /// Ветеринар
    /// </summary>
    public Veterinarian? Veterinarian { get; set; }
}