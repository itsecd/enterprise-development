namespace VetClinic.Domain.Models;

/// <summary>
/// Запись питомца на прием к врачу, используется как контракт
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время приема
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Повторный прием (true) или первичный (false)
    /// </summary>
    public bool IsRepeat { get; set; }

    /// <summary>
    /// Питомец, записанный на прием
    /// </summary>
    public int PetId { get; set; }

    public Pet? Pet { get; set; }

    /// <summary>
    /// Врач, который ведет прием
    /// </summary>
    public int VetId { get; set; }

    public Vet? Vet { get; set; }
}
