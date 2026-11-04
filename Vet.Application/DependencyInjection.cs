using Microsoft.Extensions.DependencyInjection;
using Vet.Application.Interfaces;
using Vet.Application.Services;
using Vet.Contracts.Dtos;

namespace Vet.Application;

public static class DependencyInjection
{
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