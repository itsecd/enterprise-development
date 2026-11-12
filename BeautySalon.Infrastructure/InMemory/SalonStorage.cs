using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Общее состояние салона на время работы приложения
/// </summary>
public sealed class SalonStorage
{
    internal object SyncRoot { get; } = new();

    internal List<Customer> Customers { get; }
    internal List<BeautySpecialist> Specialists { get; }
    internal List<Service> Services { get; }
    internal List<Appointment> Appointments { get; }

    private readonly Dictionary<Type, int> nextIds = new();

    /// <summary>
    /// Создать независимое хранилище с начальными данными
    /// </summary>
    public SalonStorage()
    {
        Customers = BeautySalonData.Customers.Select(EntityCopies.Copy).ToList();
        Specialists = BeautySalonData.Specialists.Select(EntityCopies.Copy).ToList();
        Services = BeautySalonData.Services.Select(EntityCopies.Copy).ToList();

        var customers = Customers.ToDictionary(customer => customer.Id);
        var specialists = Specialists.ToDictionary(specialist => specialist.Id);
        var services = Services.ToDictionary(service => service.Id);

        Appointments = BeautySalonData.Appointments.Select(source => new Appointment
        {
            Id = source.Id,
            Customer = customers[source.Customer.Id],
            Specialist = specialists[source.Specialist.Id],
            Service = services[source.Service.Id],
            AppointmentDateTime = source.AppointmentDateTime,
            IsRegularCustomer = source.IsRegularCustomer
        }).ToList();

        nextIds[typeof(Customer)] = Customers.Max(customer => customer.Id);
        nextIds[typeof(BeautySpecialist)] = Specialists.Max(specialist => specialist.Id);
        nextIds[typeof(Service)] = Services.Max(service => service.Id);
        nextIds[typeof(Appointment)] = Appointments.Max(appointment => appointment.Id);
    }

    internal int NextId<T>()
    {
        var id = checked(nextIds[typeof(T)] + 1);
        nextIds[typeof(T)] = id;
        return id;
    }
}