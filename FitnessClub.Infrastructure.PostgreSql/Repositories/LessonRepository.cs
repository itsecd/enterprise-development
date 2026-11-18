using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// EF-репозиторий занятий
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class LessonRepository(FitnessClubDbContext context)
    : EfRepository<Lesson>(context), ILessonRepository
{
    /// <summary>
    /// Загружает занятия вместе с клиентом и тренером
    /// </summary>
    protected override IQueryable<Lesson> Query =>
        Context.Set<Lesson>()
            .Include(l => l.Client)
            .Include(l => l.Trainer).ThenInclude(t => t.Specialization);
}
