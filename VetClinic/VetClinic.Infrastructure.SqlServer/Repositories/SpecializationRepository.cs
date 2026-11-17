using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий специализаций врачей
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class SpecializationRepository(VetClinicDbContext context) : EfRepository<Specialization>(context), ISpecializationRepository;
