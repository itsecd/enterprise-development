using CarWash.Contracts.Dto;
using CarWash.Domain.Entities;

namespace CarWash.Application.Mapping;

/// <summary>
/// Методы преобразования доменных сущностей в DTO
/// </summary>
public static class EntityMappingExtensions
{
    /// <summary>
    /// Преобразует автомобиль в DTO
    /// </summary>
    /// <param name="car">Автомобиль</param>
    /// <returns>DTO автомобиля</returns>
    public static CarDto ToDto(this Car car)
        => new()
        {
            Id = car.Id,
            LicensePlate = car.LicensePlate,
            Brand = car.Brand
        };

    /// <summary>
    /// Преобразует клиента в DTO
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <returns>DTO клиента</returns>
    public static ClientDto ToDto(this Client client)
        => new()
        {
            Id = client.Id,
            FullName = client.FullName,
            Phone = client.Phone
        };

    /// <summary>
    /// Преобразует услугу в DTO
    /// </summary>
    /// <param name="service">Услуга</param>
    /// <returns>DTO услуги</returns>
    public static ServiceDto ToDto(this Service service)
        => new()
        {
            Id = service.Id,
            Name = service.Name,
            Category = service.Category,
            Price = service.Price,
            DurationMinutes = service.DurationMinutes
        };
}
