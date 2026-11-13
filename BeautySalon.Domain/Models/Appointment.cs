namespace BeautySalon.Domain.Models;

/// <summary>
/// Запись клиента на услугу к мастеру.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    public required int MasterId { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int ServiceId { get; set; }

    /// <summary>
    /// Дата и время начала записи
    /// </summary>
    public required DateTime StartDateTime { get; set; }

    /// <summary>
    /// Постоянный клиент?
    /// </summary>
    public required bool IsRegularClient { get; set; }

    /// <summary>
    /// Строковое представление записи
    /// </summary>
    public override string ToString() =>
        $"ClientId: {ClientId}, MasterId: {MasterId}, ServiceId: {ServiceId}, " +
        $"Start: {StartDateTime:dd.MM.yyyy HH:mm}, Regular: {IsRegularClient}";
}