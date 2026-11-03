using VetClinic.Domain.Seeds;
using VetClinic.Shared;

namespace VetClinic.Tests;

/// <summary>
/// Unit тесты аналитических запросов ветеринарной клиники
/// </summary>
public class VetClinicTests(VetClinicSeed data) : IClassFixture<VetClinicSeed>
{
    /// <summary>
    /// Все ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    [Theory]
    [InlineData(AnimalSpecies.Dog, new[] { 1, 6, 10 })]
    [InlineData(AnimalSpecies.Cat, new[] { 2, 7 })]
    [InlineData(AnimalSpecies.Bird, new[] { 3 })]
    [InlineData(AnimalSpecies.Rabbit, new[] { 4 })]
    [InlineData(AnimalSpecies.Hamster, new[] { 8 })]
    public void GetVetsBySpecies(AnimalSpecies species, int[] expectedIds)
    {
        var result = data.Vets
            .Where(v => v.Specialization?.FocusSpecies == species)
            .Select(v => v.Id)
            .ToList();

        Assert.Equal(expectedIds, result);
    }

    /// <summary>
    /// Все питомцы, записанные на прием к указанному врачу, упорядоченные по кличке
    /// </summary>
    [Theory]
    [InlineData(1, new[] { "Буян", "Жучка", "Рекс", "Тоби" })]
    [InlineData(6, new[] { "Буян", "Лаки" })]
    public void GetPetsByVetOrderedByNickname(int vetId, string[] expectedNicknames)
    {
        var result = data.Appointments
            .Where(a => a.Vet?.Id == vetId && a.Pet is not null)
            .Select(a => a.Pet!)
            .DistinctBy(p => p.Id)
            .OrderBy(p => p.Nickname)
            .Select(p => p.Nickname)
            .ToList();

        Assert.Equal(expectedNicknames, result);
    }

    /// <summary>
    /// Количество повторных приемов выбранной породы животного
    /// </summary>
    [Theory]
    [InlineData(1, 3)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public void GetRepeatAppointmentCountByBreed(int breedId, int expectedCount)
    {
        var result = data.Appointments
            .Count(a => a.IsRepeat && a.Pet?.Breed?.Id == breedId);

        Assert.Equal(expectedCount, result);
    }

    /// <summary>
    /// Владельцы, у которых больше одного питомца, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void GetOwnersWithMultiplePetsOrderedByFullName()
    {
        var expectedIds = new[] { 1, 2, 3 };

        var result = data.Owners
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.FullName)
            .Select(o => o.Id)
            .ToList();

        Assert.Equal(expectedIds, result);
    }

    /// <summary>
    /// Приемы за выбранный месяц в выбранном кабинете
    /// </summary>
    [Theory]
    [InlineData("101", 2024, 6, new[] { 1, 2, 3, 13 })]
    [InlineData("205", 2024, 6, new[] { 4, 5 })]
    [InlineData("303", 2024, 6, new[] { 6, 7 })]
    [InlineData("404", 2024, 6, new[] { 14, 15 })]
    [InlineData("101", 2024, 5, new[] { 8, 9 })]
    public void GetAppointmentsByRoomAndMonth(string room, int year, int month, int[] expectedIds)
    {
        var result = data.Appointments
            .Where(a => a.RoomNumber == room && a.DateTime.Year == year && a.DateTime.Month == month)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(expectedIds, result);
    }
}
