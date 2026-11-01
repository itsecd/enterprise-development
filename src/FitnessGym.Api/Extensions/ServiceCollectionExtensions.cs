using FitnessGym.Application.Services;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Domain.Seeds;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Api.Extensions;

/// <summary>
/// Регистрация сервисов приложения и репозиториев в контейнере зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует общее хранилище в памяти с тестовыми данными.
    /// </summary>
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        var data = ClubDataSeeder.Create();
        services.AddSingleton<IClientRepository>(_ => new InMemoryClientRepository(data.Clients));
        services.AddSingleton<ITrainerRepository>(_ => new InMemoryTrainerRepository(data.Trainers));
        services.AddSingleton<ISpecializationRepository>(
            _ => new InMemorySpecializationRepository(data.Specializations));
        services.AddSingleton<ITrainingSessionRepository>(
            _ => new InMemoryTrainingSessionRepository(data.Sessions));
        return services;
    }

    /// <summary>
    /// Регистрирует отдельные сервисы для клиентов, тренеров, специализаций, занятий и залов.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<ITrainingSessionService, TrainingSessionService>();
        services.AddScoped<IHallService, HallService>();
        return services;
    }
}
