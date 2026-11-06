
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий тренеров фитнес-клуба.
/// </summary>
public class TrainerRepository(FitnessClubDataSeeder data)
    : InMemoryRepository<Trainer>(
        data.Trainers,
        trainer => trainer.Id,
        (trainer, id) => trainer.Id = id)
{
    /// <summary>
    /// Обновить информацию о тренере.
    /// </summary>
    public override bool Update(int id, Trainer trainer)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(
                item => item.Id == id);

            if (existing is null)
            {
                return false;
            }

            var specialization = data.Specializations
                .FirstOrDefault(s => s.Id == trainer.Specialization.Id);

            if (specialization is null)
            {
                throw new ArgumentException(
                    "Указанная специализация не найдена.");
            }

            existing.PassportNumber = trainer.PassportNumber;
            existing.FullName = trainer.FullName;
            existing.Gender = trainer.Gender;
            existing.BirthDate = trainer.BirthDate;
            existing.Specialization = specialization;
            existing.WorkExperience = trainer.WorkExperience;

            return true;
        }
    }

    /// <summary>
    /// Удалить тренера, если у него нет записей на занятия.
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
                session => session.Trainer.Id == id))
            {
                throw new InvalidOperationException(
                    "Нельзя удалить тренера, у которого есть записи на занятия.");
            }

            return Items.Remove(existing);
        }
    }
}
