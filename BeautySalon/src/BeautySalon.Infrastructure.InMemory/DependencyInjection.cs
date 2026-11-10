using BeautySalon.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Регистрация in-memory хранилища в контейнере зависимостей.
/// Для перехода на БД эту регистрацию заменяют на аналогичную из Infrastructure.MsSqlServer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует хранилище данных и репозитории (singleton: данные живут, пока работает приложение)
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Та же коллекция для цепочки вызовов</returns>
    public static IServiceCollection AddInMemoryStorage(this IServiceCollection services)
    {
        services.AddSingleton<BeautySalonDataStore>();
        services.AddSingleton<IClientRepository, ClientRepository>();
        services.AddSingleton<IMasterRepository, MasterRepository>();
        services.AddSingleton<IServiceRepository, ServiceRepository>();
        services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
        return services;
    }
}