using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.PostgreSql.Repositories;

public class PostgreSqlTrainingSessionRepository(FitnessGymDbContext context)
    : PostgreSqlRepository<TrainingSession>(context), ITrainingSessionRepository
{
    protected override IQueryable<TrainingSession> Query => base.Query
        .Include(session => session.Client)
        .Include(session => session.Trainer).ThenInclude(trainer => trainer.Specialization);

    public async Task<IReadOnlyList<TrainingSession>> GetForMonthAsync(string hallName, DateTime month)
    {
        var start = new DateTime(month.Year, month.Month, 1);
        var end = start.AddMonths(1);
        return await Query.Where(session => session.HallName == hallName
                                            && session.StartsAt >= start && session.StartsAt < end)
            .OrderBy(session => session.StartsAt).ThenBy(session => session.Id).ToListAsync();
    }

    public Task<bool> HasClientAsync(int clientId) =>
        Context.Sessions.AnyAsync(session => session.ClientId == clientId);

    public Task<bool> HasTrainerAsync(int trainerId) =>
        Context.Sessions.AnyAsync(session => session.TrainerId == trainerId);

    public async Task<IReadOnlyList<Trainer>> GetPopularTrainersAsync(int count) =>
        await Context.Trainers.AsNoTracking().Include(trainer => trainer.Specialization)
            .Where(trainer => Context.Sessions.Any(session => session.TrainerId == trainer.Id))
            .OrderByDescending(trainer => Context.Sessions.Count(session => session.TrainerId == trainer.Id))
            .ThenBy(trainer => trainer.Id).Take(count).ToListAsync();

    public async Task<IReadOnlyList<string>> GetHallsAsync() =>
        await Context.Sessions.Select(session => session.HallName).Distinct().Order().ToListAsync();

    public async Task<bool> IsHallAvailableAsync(string hallName, DateTime at) =>
        !await Context.Sessions.AnyAsync(session => session.HallName == hallName
                                                   && session.EndsAt != null
                                                   && session.StartsAt <= at && at < session.EndsAt);
}
