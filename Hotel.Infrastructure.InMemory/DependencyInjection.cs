using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Infrastructure.InMemory;

/// <summary>
/// Регистрация зависимостей infrastructure
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Добавляет сессию хранилища и репозитории для всех сущностей в коллекцию
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с зарегестрированными репозиториями</returns>
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<HotelSession>();

        services.AddSingleton<IRepository<Client>>(sp => sp.GetRequiredService<HotelSession>().Clients);
        services.AddSingleton<IRepository<Room>>(sp => sp.GetRequiredService<HotelSession>().Rooms);
        services.AddSingleton<IRepository<RoomType>>(sp => sp.GetRequiredService<HotelSession>().RoomTypes);
        services.AddSingleton<IRepository<Booking>>(sp => sp.GetRequiredService<HotelSession>().Bookings);

        return services;
    }
}