using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Infrastructure.InMemory.Repositories;

namespace BeautySalon.Tests;

/// <summary>
/// Проверки уникальности паспортов специалистов
/// </summary>
public class SpecialistPassportTests
{
    /// <summary>
    /// Нельзя создать специалиста с занятым паспортом
    /// </summary>
    [Fact]
    public void DuplicatePassportCannotBeAdded()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(1)!;
        var originalCount = repository.GetAll().Count;

        Assert.Throws<InvalidOperationException>(() => repository.Add(specialist));
        Assert.Equal(originalCount, repository.GetAll().Count);
    }

    /// <summary>
    /// При обновлении специалист сохраняет собственный паспорт
    /// </summary>
    [Fact]
    public void UpdatingWithOwnPassportIsAllowed()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(1)!;
        specialist.LastName = "Новая фамилия";

        Assert.True(repository.Update(specialist));
        Assert.Equal("Новая фамилия", repository.GetById(1)!.LastName);
    }

    /// <summary>
    /// Неудачная смена паспорта сохраняет прежние данные специалиста
    /// </summary>
    [Fact]
    public void DuplicatePassportUpdateDoesNotChangeStoredData()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(2)!;
        var originalName = specialist.LastName;
        var originalPassport = specialist.PassportNumber;

        specialist.LastName = "Изменённая фамилия";
        specialist.PassportNumber = repository.GetById(1)!.PassportNumber;

        Assert.Throws<InvalidOperationException>(() => repository.Update(specialist));

        var stored = repository.GetById(2)!;
        Assert.Equal(originalName, stored.LastName);
        Assert.Equal(originalPassport, stored.PassportNumber);
    }

    /// <summary>
    /// Несколько специалистов могут не указывать паспорт
    /// </summary>
    [Fact]
    public void MissingPassportsAreAllowedAndNormalized()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(1)!;
        specialist.PassportNumber = null;
        var first = repository.Add(specialist);

        specialist.PassportNumber = "   ";
        var second = repository.Add(specialist);

        Assert.Null(first.PassportNumber);
        Assert.Null(second.PassportNumber);
        Assert.NotEqual(first.Id, second.Id);
    }

    /// <summary>
    /// Пробелы по краям паспорта не позволяют создать дубликат
    /// </summary>
    [Fact]
    public void SurroundingSpacesDoNotBypassUniqueness()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(1)!;
        specialist.PassportNumber = $" {specialist.PassportNumber} ";

        Assert.Throws<InvalidOperationException>(() => repository.Add(specialist));
    }

    /// <summary>
    /// Одновременные запросы не сохраняют одинаковые паспорта
    /// </summary>
    [Fact]
    public void ConcurrentAddsKeepPassportUnique()
    {
        var repository = new SpecialistRepository(new SalonStorage());
        var specialist = repository.GetById(1)!;
        specialist.PassportNumber = "9900123456";
        var successes = 0;
        var conflicts = 0;

        Parallel.For(0, 8, _ =>
        {
            try
            {
                repository.Add(specialist);
                Interlocked.Increment(ref successes);
            }
            catch (InvalidOperationException)
            {
                Interlocked.Increment(ref conflicts);
            }
        });

        Assert.Equal(1, successes);
        Assert.Equal(7, conflicts);
    }
}