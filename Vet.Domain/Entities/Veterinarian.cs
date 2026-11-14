namespace Vet.Domain.Entities;

/// <summary>
/// Ветеринар
/// </summary>
public sealed class Veterinarian
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>    
    public required int BirthYear { get; set; }

    /// <summary>
    /// Опыт работы (в годах)
    /// </summary>
    public required int WorkExperience { get; set; }

    /// <summary>
    /// Список приёмов
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}