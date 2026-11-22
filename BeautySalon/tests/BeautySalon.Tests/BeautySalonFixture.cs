using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.InMemory;

namespace BeautySalon.Tests;

/// <summary>
/// Фикстура с тестовыми данными из in-memory хранилища
/// </summary>
public class BeautySalonFixture
{
    private readonly BeautySalonDataStore _dataStore = new();

    /// <summary>
    /// Клиенты салона
    /// </summary>
    public List<Client> Clients => _dataStore.Clients;

    /// <summary>
    /// Мастера салона
    /// </summary>
    public List<Master> Masters => _dataStore.Masters;

    /// <summary>
    /// Услуги салона
    /// </summary>
    public List<Service> Services => _dataStore.Services;

    /// <summary>
    /// Записи на приём
    /// </summary>
    public List<Appointment> Appointments => _dataStore.Appointments;
}
