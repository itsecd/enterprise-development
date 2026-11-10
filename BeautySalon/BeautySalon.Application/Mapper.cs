using BeautySalon.Domain.Entities;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Application.Contracts.Services;
using BeautySalon.Application.Contracts.Appointments;
using BeautySalon.Application.Contracts.Analytics;

namespace BeautySalon.Application;

/// <summary>
/// Маппер сущностей в Dto и обратно
/// </summary>
public static class Mapper
{
    public static ClientDto Map(Client entity) => new(entity.Id, entity.FullName, entity.Gender, entity.Birthday, entity.PhoneNumber);

    public static Client Map(ClientCreateUpdateDto dto) =>
        new() { Id = 0, FullName = dto.FullName, Gender = dto.Gender, Birthday = dto.Birthday, PhoneNumber = dto.PhoneNumber };

    public static IList<ClientDto> Map(IList<Client> entities) => [.. entities.Select(Map)];

    public static MasterDto Map(Master entity) =>
        new(entity.Id, entity.FullName, entity.Gender, entity.PassportNumber, entity.Specialization, entity.WorkExperience);

    public static Master Map(MasterCreateUpdateDto dto) =>
        new() { Id = 0, FullName = dto.FullName, Gender = dto.Gender, PassportNumber = dto.PassportNumber, Specialization = dto.Specialization, WorkExperience = dto.WorkExperience};

    public static IList<MasterDto> Map(IList<Master> entities) => [.. entities.Select(Map)];

    public static ServiceDto Map(Service entity) =>
        new(entity.Id, entity.Name, entity.Category, entity.Price, entity.Duration);

    public static Service Map(ServiceCreateUpdateDto dto) =>
        new() { Id = 0, Name = dto.Name, Category = dto.Category, Price = dto.Price, Duration = dto.Duration };

    public static IList<ServiceDto> Map(IList<Service> entities) => [.. entities.Select(entity => Map(entity))];

    public static AppointmentDto Map(Appointment entity) =>
        new(entity.Id, Map(entity.Client), Map(entity.Service), Map(entity.Master), entity.StartTime, entity.IsRegular);

    public static Appointment Map(AppointmentCreateUpdateDto dto, Client client, Service service, Master master) =>
        new() { Id = 0, Client = client, Service = service, Master = master, StartTime = dto.StartTime, IsRegular = dto.IsRegular };

    public static IList<AppointmentDto> Map(IList<Appointment> entities) => [.. entities.Select(Map)];

    public static PopularServiceDto Map(Service service, int appointmentsCount) => new(Mapper.Map(service), appointmentsCount);
}
