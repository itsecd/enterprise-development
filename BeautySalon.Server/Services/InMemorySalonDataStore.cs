using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Server.Services;

/// <summary>
/// Реализация хранилища в памяти на основе данных из SeederData.
/// </summary>
public class InMemorySalonDataStore : ISalonDataStore
{
    /// <summary>
    /// Коллекция клиентов салона.
    /// </summary>
    public List<Client> Clients { get; }
    /// <summary>
    /// Коллекция мастеров салона.
    /// </summary>
    public List<Master> Masters { get; }
    /// <summary>
    /// Справочник услуг.
    /// </summary>
    public List<Service> Services { get; }
    /// <summary>
    /// Записи клиентов на услуги.
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Инициализирует хранилище, связывая его с коллекциями из SeederData
    /// </summary>
    public InMemorySalonDataStore()
    {
        // Инициализируем SeederData
        SeederData.Initialize();
        
        // Связываем хранилище с коллекциями из SeederData
        Clients = SeederData.Clients;
        Masters = SeederData.Masters;
        Services = SeederData.Services;
        Appointments = SeederData.Appointments;
    }

    /// <summary>
    /// Сохранение изменений в базе.
    /// </summary>
    public void SaveChanges()
    {
        // При хранении в памяти сохранение не требуется, 
        // изменения в списках применяются сразу.
    }
}