
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий залов фитнес-клуба.
/// </summary>
public class GymRepository(FitnessClubDataSeeder data)
    : InMemoryRepository<Gym>(
        data.Gyms,
        gym => gym.Id,
        (gym, id) => gym.Id = id)
{
    /// <summary>
    /// Обновить информацию о зале.
    /// </summary>
    public override bool Update(int id, Gym gym)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            existing.Name = gym.Name;

            return true;
        }
    }

    /// <summary>
    /// Удалить зал, если в нём нет записей на занятия.
    /// </summary>
    public override bool Delete(int id)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            if (data.TrainingSessions.Any(
                session => session.Gym.Id == id))
            {
                throw new InvalidOperationException(
                    "Нельзя удалить зал, в котором есть записи на занятия.");
            }

            return Items.Remove(existing);
        }
    }
}
