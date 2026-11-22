using BeautySalon.Contracts.Dto;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Mapping;

/// <summary>
/// Преобразования между записью на приём и её DTO.
/// Связанные сущности маппер сам не ищет: их находит сервис и передаёт готовыми.
/// </summary>
public static class AppointmentMapper
{
    /// <summary>
    /// Преобразует запись в DTO для чтения
    /// </summary>
    /// <param name="appointment">Запись на приём</param>
    /// <returns>DTO записи с идентификаторами и именами связанных сущностей</returns>
    public static AppointmentDto ToDto(this Appointment appointment) =>
        new(appointment.Id,
            appointment.Client.Id,
            appointment.Client.FullName,
            appointment.Master.Id,
            appointment.Master.FullName,
            appointment.Service.Id,
            appointment.Service.Name,
            appointment.DateTime,
            appointment.IsRegularClient);

    /// <summary>
    /// Создаёт новую запись из DTO и найденных связанных сущностей
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <param name="client">Клиент, найденный по идентификатору из DTO</param>
    /// <param name="master">Мастер, найденный по идентификатору из DTO</param>
    /// <param name="service">Услуга, найденная по идентификатору из DTO</param>
    /// <returns>Новая запись на приём (идентификатор назначит репозиторий)</returns>
    public static Appointment ToEntity(this AppointmentEditDto dto, Client client, Master master, Service service) =>
        new()
        {
            Client = client,
            Master = master,
            Service = service,
            DateTime = dto.DateTime,
            IsRegularClient = dto.IsRegularClient
        };

    /// <summary>
    /// Переносит данные из DTO и найденных связанных сущностей в существующую запись
    /// </summary>
    /// <param name="appointment">Обновляемая запись на приём</param>
    /// <param name="dto">Новые данные</param>
    /// <param name="client">Клиент, найденный по идентификатору из DTO</param>
    /// <param name="master">Мастер, найденный по идентификатору из DTO</param>
    /// <param name="service">Услуга, найденная по идентификатору из DTO</param>
    public static void UpdateEntity(
        this Appointment appointment, AppointmentEditDto dto, Client client, Master master, Service service)
    {
        appointment.Client = client;
        appointment.Master = master;
        appointment.Service = service;
        appointment.DateTime = dto.DateTime;
        appointment.IsRegularClient = dto.IsRegularClient;
    }
}
