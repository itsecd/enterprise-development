using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.EfCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>
/// Регистрация хранилища на основе Entity Framework Core
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет EF Core-репозитории с временем жизни в пределах одного scope
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения</param>
    /// <returns>Коллекция сервисов для продолжения настройки</returns>
    public static IServiceCollection AddEfCoreStorage(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
        return services;
    }
}