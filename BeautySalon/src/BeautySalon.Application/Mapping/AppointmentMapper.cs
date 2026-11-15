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
    public static Appointment ToEntity(this AppointmentEditDto dto, Client client, Master master, Service service) =>
        new()
        {
            Client = client,
            Master = master,
            Service = service,
            DateTime = dto.DateTime!.Value,
            IsRegularClient = dto.IsRegularClient
        };

    /// <summary>
    /// Переносит данные из DTO и найденных связанных сущностей в существующую запись
    /// </summary>
    public static void UpdateEntity(
        this Appointment appointment, AppointmentEditDto dto, Client client, Master master, Service service)
    {
        appointment.Client = client;
        appointment.Master = master;
        appointment.Service = service;
        appointment.DateTime = dto.DateTime!.Value;
        appointment.IsRegularClient = dto.IsRegularClient;
    }
}