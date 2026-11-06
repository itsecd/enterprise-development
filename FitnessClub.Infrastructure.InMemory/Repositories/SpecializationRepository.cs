
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий специализаций тренеров.
/// </summary>
public class SpecializationRepository(FitnessClubDataSeeder data)
    : InMemoryRepository<Specialization>(
        data.Specializations,
        specialization => specialization.Id,
        (specialization, id) => specialization.Id = id)
{
    /// <summary>
    /// Обновить специализацию.
    /// </summary>
    public override bool Update(int id, Specialization specialization)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            existing.Name = specialization.Name;

            return true;
        }
    }

    /// <summary>
    /// Удалить специализацию.
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

            if (data.Trainers.Any(
                trainer => trainer.Specialization.Id == id))
            {
                throw new InvalidOperationException(
                    "Нельзя удалить специализацию, которая назначена тренерам.");
            }

            return Items.Remove(existing);
        }
    }
}
