using BeautySalon.Contracts.Dto;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Mapping;

/// <summary>
/// Преобразования между услугой и её DTO
/// </summary>
public static class ServiceMapper
{
    /// <summary>
    /// Преобразует услугу в DTO для чтения
    /// </summary>
    public static ServiceDto ToDto(this Service service) =>
        new(service.Id, service.Name, service.Category, service.Price, service.Duration);

    /// <summary>
    /// Создаёт новую услугу из DTO (идентификатор назначит репозиторий)
    /// </summary>
    public static Service ToEntity(this ServiceEditDto dto) =>
        new()
        {
            Name = dto.Name,
            Category = dto.Category!.Value,
            Price = dto.Price!.Value,
            Duration = dto.Duration!.Value
        };

    /// <summary>
    /// Переносит данные из DTO в существующую услугу
    /// </summary>
    public static void UpdateEntity(this Service service, ServiceEditDto dto)
    {
        service.Name = dto.Name;
        service.Category = dto.Category!.Value;
        service.Price = dto.Price!.Value;
        service.Duration = dto.Duration!.Value;
    }
}