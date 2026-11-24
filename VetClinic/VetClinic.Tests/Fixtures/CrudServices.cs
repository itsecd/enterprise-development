using VetClinic.Application.Services;
using VetClinic.Domain.Seeds;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Набор CRUD-сервисов поверх репозиториев в памяти
/// </summary>
/// <param name="Seed">Начальные данные, с которыми работают сервисы</param>
/// <param name="Owners">Сервис владельцев</param>
/// <param name="Pets">Сервис питомцев</param>
/// <param name="Appointments">Сервис записей на прием</param>
/// <param name="Breeds">Сервис пород</param>
/// <param name="Vets">Сервис ветеринаров</param>
public sealed record CrudServices(
    VetClinicSeed Seed,
    OwnerService Owners,
    PetService Pets,
    AppointmentService Appointments,
    BreedService Breeds,
    VetService Vets);
