using Microsoft.EntityFrameworkCore;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>
/// Описывает фиксированные начальные данные для миграции.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Добавляет не менее десяти записей каждой сущности в модель миграции.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели EF Core.</param>
    public static void Configure(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Specialization>().HasData(specializations);

        Hall[] halls = [.. Enumerable.Range(1, 10)
            .Select(id => new Hall
            {
                Id = id,
                Name = $"Зал №{id}",
                Capacity = 10 + id * 2
            })];

        modelBuilder.Entity<Hall>().HasData(halls);

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
                SubscriptionStartDate = new DateTime(2026, 11, 13).AddMonths(-3),
                SubscriptionEndDate = id % 3 == 0
                    ? new DateTime(2026, 11, 13).AddDays(-10)
                    : new DateTime(2026, 11, 13).AddMonths(2)
            })];

        modelBuilder.Entity<Client>().HasData(clients);

        Trainer[] trainers = [.. Enumerable.Range(1, 10)
            .Select(id => new Trainer
            {
                Id = id,
                PassportNumber = $"2000 {200000 + id}",
                FullName = $"Тренер {id}",
                Gender = id % 2 == 0 ? Gender.Female : Gender.Male,
                BirthDate = new DateTime(1985 + id, id, Math.Min(id + 5, 28)),
                SpecializationId = id,
                WorkExperience = id
            })];

        modelBuilder.Entity<Trainer>().HasData(trainers);

        var today = new DateTime(2026, 11, 13);
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        Training[] trainings = [.. Enumerable.Range(1, 10)
            .Select(id => new Training
            {
                Id = id,
                ClientId = id,
                TrainerId = id,
                HallId = id,
                StartTime = id == 1
                    ? new DateTime(2026, 11, 13, 12, 0, 0, DateTimeKind.Utc).AddMinutes(-30)
                    : firstDayOfMonth.AddDays(id - 1).AddHours(10),
                DurationMinutes = 60,
                IsTrial = id % 2 == 0
            })];

        modelBuilder.Entity<Training>().HasData(trainings);
    }
}
