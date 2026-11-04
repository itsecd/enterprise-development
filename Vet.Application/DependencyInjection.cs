using Microsoft.Extensions.DependencyInjection;
using Vet.Application.Interfaces;
using Vet.Application.Services;
using Vet.Contracts.Dtos;

namespace Vet.Application;

/// <summary>
/// Класс для регистрации зависимостей слоя приложения
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление сервисов приложения в коллекцию зависимостей
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб</param>
    /// <returns>Коллекция служб с добавленными сервисами</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IApplicationService<BreedDto, BreedCreateUpdateDto, int>, BreedService>();
        services.AddScoped<IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>, SpecializationService>();
        services.AddScoped<IApplicationService<OwnerDto, OwnerCreateUpdateDto, int>, OwnerService>();
        services.AddScoped<IApplicationService<PetDto, PetCreateUpdateDto, int>, PetService>();
        services.AddScoped<IApplicationService<VeterinarianDto, VeterinarianCreateUpdateDto, int>, VeterinarianService>();
        services.AddScoped<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>, AppointmentService>();

        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}