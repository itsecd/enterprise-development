using BeautySalon.Application.Services;
using BeautySalon.Infrastructure.InMemory;

namespace BeautySalon.Tests;

/// <summary>
/// Фикстура для тестов CRUD-сервисов поверх in-memory репозиториев.
/// Тесты изменяют данные, поэтому каждый сервис собирается на отдельном хранилище.
/// </summary>
public class CrudServiceFixture
{
    /// <summary>
    /// Создаёт сервис клиентов на новом хранилище
    /// </summary>
    /// <returns>Сервис клиентов с изолированными данными</returns>
    public ClientService CreateClientService() => new(new ClientRepository(new BeautySalonDataStore()));

    /// <summary>
    /// Создаёт сервис записей на новом хранилище
    /// </summary>
    /// <returns>Сервис записей, репозитории которого смотрят в одно изолированное хранилище</returns>
    public AppointmentService CreateAppointmentService()
    {
        var store = new BeautySalonDataStore();
        return new AppointmentService(
            new AppointmentRepository(store),
            new ClientRepository(store),
            new MasterRepository(store),
            new ServiceRepository(store));
    }
}
