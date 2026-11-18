using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;

namespace FitnessClub.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// EF-репозиторий специализаций
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class SpecializationRepository(FitnessClubDbContext context)
    : EfRepository<Specialization>(context), ISpecializationRepository;
