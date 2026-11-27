using Microsoft.EntityFrameworkCore;
using VetClinic.Application.Services;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Infrastructure.PostgreSql;
using VetClinic.Infrastructure.PostgreSql.Repositories;

namespace VetClinic.WebApi.Extensions;

/// <summary>
/// Методы расширения для регистрации сервисов в контейнере зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация контекста и репозиториев PostgreSQL
    /// </summary>
    public static IServiceCollection AddPostgreSqlRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("vetclinic")
            ?? throw new InvalidOperationException(
                "Строка подключения 'vetclinic' не задана. Запустите VetClinic.AppHost.");

        services.AddDbContext<VetClinicDbContext>(options => options.UseNpgsql(connectionString));

        services.AddEfRepository<Breed>(db => db.Breeds);
        services.AddEfRepository<Specialization>(db => db.Specializations);
        services.AddEfRepository<Owner>(db => db.Owners.Include(owner => owner.Pets));
        services.AddEfRepository<Pet>(db => db.Pets.Include(pet => pet.Breed).Include(pet => pet.Owner));
        services.AddEfRepository<Veterinarian>(db => db.Veterinarians.Include(vet => vet.Specialization));
        services.AddEfRepository<Appointment>(db => db.Appointments
            .Include(appointment => appointment.Pet).ThenInclude(pet => pet!.Breed)
            .Include(appointment => appointment.Pet).ThenInclude(pet => pet!.Owner)
            .Include(appointment => appointment.Veterinarian));

        return services;
    }

    private static void AddEfRepository<T>(
        this IServiceCollection services,
        Func<VetClinicDbContext, IQueryable<T>> query)
        where T : class, IEntity
    {
        services.AddScoped<IRepository<T>>(provider =>
        {
            var db = provider.GetRequiredService<VetClinicDbContext>();
            return new EfRepository<T>(db, query(db));
        });
    }

    /// <summary>
    /// Регистрация сервисов бизнес-логики приложения
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVetClinicService, VetClinicService>();
        return services;
    }
}
