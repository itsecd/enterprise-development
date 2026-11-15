namespace VetClinic.Domain.Entities;

using VetClinic.Domain.Shared.Enums;
/// <summary>
/// Записи на приём
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор приёма 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Время приёма
    /// </summary>
    public DateTime DateTime { get; set; }
    /// <summary>
    /// Диагноз 
    /// </summary>
    public string? Diagnosis { get; set; }
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int PetId { get; set; }
    /// <summary>
    /// Ссылка на питомца 
    /// </summary>
    public required Pet Pet { get; set; }
    /// <summary>
    /// Идентификатор ветеринара
    /// </summary>
    public int VetId { get; set; }
    /// <summary>
    /// Ссылка на ветеринара, проводящего приём
    /// </summary>
    public required Vet Vet { get; set; }
    /// <summary>
    /// Номер кабинета
    /// </summary>
    public required int RoomNumber { get; set; }
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public int ServiceId { get; set; }
    /// <summary>
    /// Ссылка на оказанную услугу 
    /// </summary>
    public required Service Service { get; set; }
    /// <summary>
    /// Индикатор повторного приёма
    /// </summary>
    public required RepeatVisitIndicator RepeatVisitIndicator { get; set; }
}