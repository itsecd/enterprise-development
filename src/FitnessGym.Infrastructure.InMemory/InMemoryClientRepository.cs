using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory;

/// <summary>
/// Репозиторий сущностей Client с хранением в памяти.
/// </summary>
/// <param name="seed">Начальные данные.</param>
public class InMemoryClientRepository(IReadOnlyList<Client> seed)
    : InMemoryRepository<Client>(seed), IClientRepository
{
    /// <inheritdoc/>
    public Task<IReadOnlyList<Client>> GetExpiredAsync(DateOnly today) =>
        ReadAsync<IReadOnlyList<Client>>(items =>
            [.. items.Where(client => client.SubscriptionEnd < today).OrderBy(client => client.FullName)]);

    /// <inheritdoc/>
    protected override void CopyProperties(Client source, Client target)
    {
        target.PassportNumber = source.PassportNumber;
        target.FullName = source.FullName;
        target.Gender = source.Gender;
        target.BirthDate = source.BirthDate;
        target.Phone = source.Phone;
        target.SubscriptionStart = source.SubscriptionStart;
        target.SubscriptionEnd = source.SubscriptionEnd;
    }
}
