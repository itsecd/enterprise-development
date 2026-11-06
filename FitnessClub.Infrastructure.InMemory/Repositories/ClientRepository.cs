
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий клиентов с хранением данных в памяти.
/// </summary>
public class ClientRepository(FitnessClubDataSeeder data)
    : InMemoryRepository<Client>(
        data.Clients,
        client => client.Id,
        (client, id) => client.Id = id)
{
    /// <summary>
    /// Обновить информацию о клиенте.
    /// </summary>
    public override bool Update(int id, Client client)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            existing.PassportNumber = client.PassportNumber;
            existing.FullName = client.FullName;
            existing.Gender = client.Gender;
            existing.BirthDate = client.BirthDate;
            existing.PhoneNumber = client.PhoneNumber;
            existing.SubscriptionStartDate = client.SubscriptionStartDate;
            existing.SubscriptionEndDate = client.SubscriptionEndDate;

            return true;
        }
    }

    /// <summary>
    /// Удалить клиента, если у него нет записей на занятия.
    /// </summary>
    public override bool Delete(int id)
    {
        lock (SyncRoot)
        {
            var client = Items.FirstOrDefault(
                item => item.Id == id);

            if (client is null)
            {
                return false;
            }

            if (data.TrainingSessions.Any(
                session => session.Client.Id == id))
            {
                throw new InvalidOperationException(
                    "Нельзя удалить клиента, у которого есть записи на занятия.");
            }

            return Items.Remove(client);
        }
    }
}
