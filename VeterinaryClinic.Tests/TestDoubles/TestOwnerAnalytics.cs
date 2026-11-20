using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Tests.TestDoubles;

public sealed class TestOwnerAnalytics(IRepository<Owner> owners) : IOwnerAnalytics
{
    public async Task<IReadOnlyList<Owner>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default)
        => (await owners.GetAllAsync(cancellationToken))
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ThenBy(o => o.Patronymic)
            .ToArray();
}
