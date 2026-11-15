using Microsoft.Extensions.DependencyInjection;
using VetClinic.Application.Contracts;
using VetClinic.Application.Contracts.Appointments;
using VetClinic.Application.Contracts.Breeds;
using VetClinic.Application.Contracts.Owners;
using VetClinic.Application.Contracts.Pets;
using VetClinic.Application.Contracts.Services;
using VetClinic.Application.Contracts.Specializations;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Application.Services;

namespace VetClinic.Application;

/// <summary>
/// Статический класс конфигурации зависимостей (DI) для сервисного слоя
/// </summary>
public static class ApplicationDependencyInjection
{
    /// <summary>
    /// Регистрирует все прикладные сервисы и сервис сложной аналитики в контейнере зависимостей со временем жизни Scoped
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов</param>
    /// <returns>Обновленная коллекция сервисов</returns>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ISpecializationAppService, SpecializationAppService>();
        services.AddScoped<IBreedAppService, BreedAppService>();
        services.AddScoped<IOwnerAppService, OwnerAppService>();
        services.AddScoped<IPetAppService, PetAppService>();
        services.AddScoped<IVetAppService, VetAppService>();
        services.AddScoped<IServiceAppService, ServiceAppService>();
        services.AddScoped<IAppointmentAppService, AppointmentAppService>();
        services.AddScoped<IAnalyticService, AnalyticService>();

        return services;
    }
}

