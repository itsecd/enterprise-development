using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Infrastructure.InMemory.Data;

/// <summary>
/// Заполняет хранилище начальными данными для проверки приложения.
/// </summary>
public static class FitnessClubDataSeeder
{
    /// <summary>
    /// Добавляет тестовые данные, если хранилище пустое.
    /// </summary>
    /// <param name="store">Хранилище фитнес-клуба.</param>
    public static void Seed(FitnessClubDataStore store)
    {
        ArgumentNullException.ThrowIfNull(store);

        if (store.Specializations.Count > 0
            || store.Halls.Count > 0
            || store.Clients.Count > 0
            || store.Trainers.Count > 0
            || store.Trainings.Count > 0)
        {
            return;
        }

        string[] specializationNames =
        [
            "Силовой тренинг",
            "Общий фитнес",
            "Йога",
            "Пилатес",
            "Кардио",
            "Бокс",
            "Плавание",
            "Стретчинг",
            "Кроссфит",
            "Функциональный тренинг"
        ];

        Specialization[] specializations = [.. specializationNames
            .Select((name, index) => new Specialization
            {
                Id = index + 1,
                Name = name,
                Description = $"Направление тренировок: {name.ToLowerInvariant()}."
            })];

        store.Specializations.AddRange(specializations);

        Hall[] halls = [.. Enumerable.Range(1, 10)
            .Select(id => new Hall
            {
                Id = id,
                Name = $"Зал №{id}",
                Capacity = 10 + id * 2
            })];

        store.Halls.AddRange(halls);

        string[] firstNames =
        [
            "Иван", "Анна", "Алексей", "Мария", "Дмитрий",
            "Елена", "Сергей", "Ольга", "Андрей", "Наталья"
        ];

        string[] lastNames =
        [
            "Иванов", "Петрова", "Смирнов", "Кузнецова", "Соколов",
            "Попова", "Лебедев", "Волкова", "Козлов", "Новикова"
        ];

        Client[] clients = [.. Enumerable.Range(1, 10)
            .Select(id => new Client
            {
                Id = id,
                PassportNumber = $"1000 {100000 + id}",
                FullName = $"{lastNames[id - 1]} {firstNames[id - 1]} "
                    + "Александрович",
                Gender = id % 2 == 0 ? Gender.Female : Gender.Male,
                BirthDate = new DateTime(1990 + id, id, Math.Min(id + 5, 28)),
                Phone = $"+799900000{id:D2}",
                SubscriptionStartDate = DateTime.Today.AddMonths(-3),
                SubscriptionEndDate = id % 3 == 0
                    ? DateTime.Today.AddDays(-10)
                    : DateTime.Today.AddMonths(2)
            })];

        store.Clients.AddRange(clients);

        Trainer[] trainers = [.. Enumerable.Range(1, 10)
            .Select(id => new Trainer
            {
                Id = id,
                PassportNumber = $"2000 {200000 + id}",
                FullName = $"Тренер {id}",
                Gender = id % 2 == 0 ? Gender.Female : Gender.Male,
                BirthDate = new DateTime(1985 + id, id, Math.Min(id + 5, 28)),
                Specialization = specializations[id - 1],
                WorkExperience = id
            })];

        store.Trainers.AddRange(trainers);

        var today = DateTime.Today;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

        Training[] trainings = [.. Enumerable.Range(1, 10)
            .Select(id => new Training
            {
                Id = id,
                Client = clients[id - 1],
                Trainer = trainers[id - 1],
                Hall = halls[id - 1],
                StartTime = id == 1
                    ? DateTime.Now.AddMinutes(-30)
                    : firstDayOfMonth.AddDays(id - 1).AddHours(10),
                DurationMinutes = 60,
                IsTrial = id % 2 == 0
            })];

        store.Trainings.AddRange(trainings);
    }
}