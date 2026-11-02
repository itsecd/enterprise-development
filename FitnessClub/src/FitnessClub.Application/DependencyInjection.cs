using FitnessClub.Application.Services;
using FitnessClub.Application.Validation;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Application;

/// <summary>
/// Методы регистрации зависимостей слоя приложения
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует сервисы приложения и валидаторы
    /// </summary>
    /// <param name="services">
    /// Коллекция сервисов для регистрации
    /// </param>
    /// <returns>
    /// Та же коллекция сервисов для дальнейшей настройки
    /// </returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<PassportNumberUniquenessValidator>();
        services.AddScoped<BookingScheduleValidator>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddScoped<ICrudService<TrainerDto, TrainerEditDto>, TrainerService>();
        services.AddScoped<ICrudService<BookingDto, BookingEditDto>, BookingService>();
        services.AddScoped<ICrudService<SpecializationDto, SpecializationEditDto>, SpecializationService>();
        services.AddScoped<ICrudService<HallDto, HallEditDto>, HallService>();
        services.AddScoped<ICrudService<ClientDto, ClientEditDto>, ClientService>();
        return services;
    }
}