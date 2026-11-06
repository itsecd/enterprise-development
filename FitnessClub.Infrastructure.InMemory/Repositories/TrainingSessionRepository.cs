
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий записей клиентов на персональные занятия.
/// </summary>
public class TrainingSessionRepository(FitnessClubDataSeeder data)
    : InMemoryRepository<TrainingSession>(
        data.TrainingSessions,
        session => session.Id,
        (session, id) => session.Id = id)
{
    /// <summary>
    /// Обновить запись на занятие.
    /// </summary>
    public override bool Update(int id, TrainingSession session)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            var client = data.Clients.FirstOrDefault(
                c => c.Id == session.Client.Id)
                ?? throw new ArgumentException("Клиент не найден.");

            var trainer = data.Trainers.FirstOrDefault(
                t => t.Id == session.Trainer.Id)
                ?? throw new ArgumentException("Тренер не найден.");

            var gym = data.Gyms.FirstOrDefault(
                g => g.Id == session.Gym.Id)
                ?? throw new ArgumentException("Зал не найден.");

            if (session.EndsAt.HasValue &&
                session.EndsAt.Value <= session.StartsAt)
            {
                throw new ArgumentException(
                    "Время окончания занятия должно быть позже начала.");
            }

            existing.Client = client;
            existing.Trainer = trainer;
            existing.Gym = gym;
            existing.StartsAt = session.StartsAt;
            existing.EndsAt = session.EndsAt;
            existing.IsTrial = session.IsTrial;

            return true;
        }
    }
}
