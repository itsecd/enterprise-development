using TransportCompany.Contracts.Dto;
using TransportCompany.Domain.Entities;

namespace TransportCompany.Application.Mappers;

/// <summary>
/// Методы расширения для преобразования сущностей в DTO и обратно
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразовать сущность клиента в DTO
    /// </summary>
    public static ClientDto ToDto(this Client entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Phone = entity.Phone
    };

    /// <summary>
    /// Преобразовать DTO редактирования клиента в сущность
    /// </summary>
    public static Client ToEntity(this ClientEditDto dto) => new()
    {
        Id = 0,
        Name = dto.Name,
        Phone = dto.Phone
    };

    /// <summary>
    /// Преобразовать сущность водителя в DTO
    /// </summary>
    public static DriverDto ToDto(this Driver entity) => new()
    {
        Id = entity.Id,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        Experience = entity.Experience,
        License = entity.License
    };

    /// <summary>
    /// Преобразовать DTO редактирования водителя в сущность
    /// </summary>
    public static Driver ToEntity(this DriverEditDto dto) => new()
    {
        Id = 0,
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        Experience = dto.Experience,
        License = dto.License
    };

    /// <summary>
    /// Преобразовать сущность модели ТС в DTO
    /// </summary>
    public static VehicleModelDto ToDto(this VehicleModel entity) => new()
    {
        Id = entity.Id,
        BodyType = entity.BodyType,
        BodyVolume = entity.BodyVolume
    };

    /// <summary>
    /// Преобразовать DTO редактирования модели ТС в сущность
    /// </summary>
    public static VehicleModel ToEntity(this VehicleModelEditDto dto) => new()
    {
        Id = 0,
        BodyType = dto.BodyType,
        BodyVolume = dto.BodyVolume
    };

    /// <summary>
    /// Преобразовать сущность транспортного средства в DTO
    /// </summary>
    public static VehicleDto ToDto(this Vehicle entity) => new()
    {
        Id = entity.Id,
        LicensePlate = entity.LicensePlate,
        LoadCapacity = entity.LoadCapacity,
        ModelId = entity.ModelId
    };

    /// <summary>
    /// Преобразовать DTO редактирования транспортного средства в сущность
    /// </summary>
    public static Vehicle ToEntity(this VehicleEditDto dto) => new()
    {
        Id = 0,
        LicensePlate = dto.LicensePlate,
        LoadCapacity = dto.LoadCapacity,
        ModelId = dto.ModelId
    };

    /// <summary>
    /// Преобразовать сущность рейса в DTO
    /// </summary>
    public static TripDto ToDto(this Trip entity) => new()
    {
        Id = entity.Id,
        DriverId = entity.DriverId,
        VehicleId = entity.VehicleId,
        ClientId = entity.ClientId,
        Date = entity.Date,
        Origin = entity.Origin,
        Destination = entity.Destination,
        CargoWeight = entity.CargoWeight,
        TransportationCost = entity.TransportationCost,
        Status = entity.Status
    };

    /// <summary>
    /// Преобразовать DTO редактирования рейса в сущность
    /// </summary>
    public static Trip ToEntity(this TripEditDto dto) => new()
    {
        Id = 0,
        DriverId = dto.DriverId,
        VehicleId = dto.VehicleId,
        ClientId = dto.ClientId,
        Date = dto.Date,
        Origin = dto.Origin,
        Destination = dto.Destination,
        CargoWeight = dto.CargoWeight,
        TransportationCost = dto.TransportationCost,
        Status = dto.Status
    };
}