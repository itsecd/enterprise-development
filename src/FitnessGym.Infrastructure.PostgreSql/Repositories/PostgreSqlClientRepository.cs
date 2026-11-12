using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.PostgreSql.Repositories;

public class PostgreSqlClientRepository(FitnessGymDbContext context)
    : PostgreSqlRepository<Client>(context), IClientRepository
{
    public async Task<IReadOnlyList<Client>> GetExpiredAsync(DateOnly today) =>
        await Query.Where(client => client.SubscriptionEnd < today)
            .OrderBy(client => client.FullName).ThenBy(client => client.Id).ToListAsync();
}
