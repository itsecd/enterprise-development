using FitnessClub.Domain.Data;

namespace FitnessClub.Tests;

/// <summary>
/// Фикстура, содержащая набор данных для тестов фитнес-клуба.
/// </summary>
public class FitnessClubFixture
{
    /// <summary>
    /// Фиксированный момент времени для тестов.
    /// </summary>
    public DateTime ReferenceTime { get; }

    /// <summary>
    /// Подготовленные тестовые данные фитнес-клуба.
    /// </summary>
    public FitnessClubDataSeeder Data { get; }

    /// <summary>
    /// Создаёт фикстуру с фиксированным временем и тестовыми данными.
    /// </summary>
    public FitnessClubFixture()
    {
        ReferenceTime = new DateTime(2026, 9, 25, 12, 0, 0);
        Data = new FitnessClubDataSeeder(ReferenceTime);
    }
}