using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий услуг в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ServiceRepository(CarWashDbContext context) : EfRepository<Service>(context), IServiceRepository;
