using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий пород
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class BreedRepository(VetClinicDbContext context) : EfRepository<Breed>(context), IBreedRepository;
