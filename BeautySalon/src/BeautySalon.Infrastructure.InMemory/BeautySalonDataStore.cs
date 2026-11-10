using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Seeds;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Хранилище данных салона красоты в памяти. Первоначально заполняется тестовыми данными.
/// </summary>
public class BeautySalonDataStore
{
    /// <summary>
    /// Коллекция клиентов
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Коллекция мастеров
    /// </summary>
    public List<Master> Masters { get; }

    /// <summary>
    /// Коллекция услуг
    /// </summary>
    public List<Service> Services { get; }

    /// <summary>
    /// Коллекция записей на приём
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Создаёт хранилище и заполняет его данными из <see cref="BeautySalonSeed"/>
    /// </summary>
    public BeautySalonDataStore()
    {
        var seed = new BeautySalonSeed();
        Clients = seed.Clients;
        Masters = seed.Masters;
        Services = seed.Services;
        Appointments = seed.Appointments;
    }
}