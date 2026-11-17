using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.PostgreSql.Repositories;

public class PostgreSqlTrainerRepository(FitnessGymDbContext context)
    : PostgreSqlRepository<Trainer>(context), ITrainerRepository
{
    protected override IQueryable<Trainer> Query => base.Query.Include(trainer => trainer.Specialization);

    public async Task<IReadOnlyList<Trainer>> GetExperiencedAsync(int minYears) =>
        await Query.Where(trainer => trainer.ExperienceYears >= minYears)
            .OrderBy(trainer => trainer.Id).ToListAsync();

    public Task<bool> HasSpecializationAsync(int specializationId) =>
        Context.Trainers.AnyAsync(trainer => trainer.SpecializationId == specializationId);
}
