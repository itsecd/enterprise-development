using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;
using Vet.Infrastructure.EfCore.Repositories;

namespace Vet.Infrastructure.EfCore;

/// <summary>
/// Методы расширения для внедрения зависимостей слоя EF Core инфраструктуры
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует контекст базы данных MySQL и EF Core репозитории в контейнере зависимостей
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения</param>
    /// <param name="connectionString">Строка подключения к MySQL</param>
    /// <returns>Обновленная коллекция сервисов</returns>
    public static IServiceCollection AddEfCoreInfrastructure(this IServiceCollection services, string connectionString)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
        services.AddDbContext<VetDbContext>(options => options.UseMySql(connectionString, serverVersion));

        services.AddScoped<IRepository<Breed, int>, BreedRepository>();
        services.AddScoped<IRepository<Specialization, int>, SpecializationRepository>();
        services.AddScoped<IRepository<Veterinarian, int>, VeterinarianRepository>();
        services.AddScoped<IRepository<Owner, int>, OwnerRepository>();
        services.AddScoped<IRepository<Pet, int>, PetRepository>();
        services.AddScoped<IRepository<Appointment, int>, AppointmentRepository>();

        return services;
    }
}