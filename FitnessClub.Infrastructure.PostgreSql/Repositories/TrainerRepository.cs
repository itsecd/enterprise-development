using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// EF-репозиторий тренеров
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class TrainerRepository(FitnessClubDbContext context)
    : EfRepository<Trainer>(context), ITrainerRepository
{
    /// <summary>
    /// Загружает тренеров вместе со специализацией
    /// </summary>
    protected override IQueryable<Trainer> Query =>
        Context.Set<Trainer>().Include(t => t.Specialization);
}
