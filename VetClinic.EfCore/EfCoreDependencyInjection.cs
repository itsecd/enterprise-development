using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.EfCore.Repositories;

namespace VetClinic.EfCore;

/// <summary>
/// Модуль конфигурации зависимостей (DI) для слоя работы с базой данных MySQL через Entity Framework Core.
/// </summary>
public static class EfCoreDependencyInjection
{
    /// <summary>
    /// Подключает контекст базы данных через клиентскую интеграцию Aspire и регистрирует EF Core репозитории.
    /// </summary>
    /// <param name="builder">Построитель веб-приложения ASP.NET Core.</param>
    /// <param name="connectionName">Имя ресурса базы данных, объявленного в AppHost.</param>
    /// <returns>Обновленный построитель приложения.</returns>
    public static IHostApplicationBuilder AddEfCoreDataLayer(this IHostApplicationBuilder builder, string connectionName = "vetclinicdb")
    {
        // Автоматически подключает VetClinicDbContext к контейнеру MySQL из Aspire
        builder.AddMySqlDbContext<VetClinicDbContext>(connectionName);

        // Регистрация базовых справочников и сущностей
        builder.Services.AddScoped<IRepository<Owner>, EfOwnerRepository>();
        builder.Services.AddScoped<IRepository<Breed>, EfBreedRepository>();
        builder.Services.AddScoped<IRepository<Specialization>, EfSpecializationRepository>();
        builder.Services.AddScoped<IRepository<Service>, EfServiceRepository>();
        builder.Services.AddScoped<IRepository<Pet>, EfPetRepository>();

        // Регистрация специализированных репозиториев
        builder.Services.AddScoped<IRepository<Vet>, EfVetRepository>();
        builder.Services.AddScoped<IVetRepository, EfVetRepository>();

        builder.Services.AddScoped<IRepository<Appointment>, EfAppointmentRepository>();
        builder.Services.AddScoped<IAppointmentRepository, EfAppointmentRepository>();

        return builder;
    }
}
