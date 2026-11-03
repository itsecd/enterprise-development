using VetClinic.Application.Services;
using VetClinic.Shared;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Тесты сервиса аналитики: те же запросы, что и в лабораторной 1, но через репозитории и DTO
/// </summary>
public class AnalyticsServiceTests(AnalyticsServiceFixture fixture) : IClassFixture<AnalyticsServiceFixture>
{
    private readonly AnalyticsService _service = fixture.Service;

    /// <summary>
    /// Все ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    [Theory]
    [InlineData(AnimalSpecies.Dog, new[] { 1, 6, 10 })]
    [InlineData(AnimalSpecies.Cat, new[] { 2, 7 })]
    [InlineData(AnimalSpecies.Hamster, new[] { 8 })]
    public async Task GetVetsBySpecies(AnimalSpecies species, int[] expectedIds)
    {
        var result = await _service.GetVetsBySpeciesAsync(species);

        Assert.Equal(expectedIds, result.Select(v => v.Id));
    }

    /// <summary>
    /// Питомцы врача упорядочены по кличке и не повторяются
    /// </summary>
    [Theory]
    [InlineData(1, new[] { "Буян", "Жучка", "Рекс", "Тоби" })]
    [InlineData(6, new[] { "Буян", "Лаки" })]
    public async Task GetPetsByVetOrderedByNickname(int vetId, string[] expectedNicknames)
    {
        var result = await _service.GetPetsByVetOrderedByNicknameAsync(vetId);

        Assert.Equal(expectedNicknames, result.Select(p => p.Nickname));
    }

    /// <summary>
    /// Количество повторных приемов выбранной породы
    /// </summary>
    [Theory]
    [InlineData(1, 3)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public async Task GetRepeatAppointmentCountByBreed(int breedId, int expected)
    {
        var result = await _service.GetRepeatAppointmentCountByBreedAsync(breedId);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Владельцы, у которых больше одного питомца, по ФИО
    /// </summary>
    [Fact]
    public async Task GetOwnersWithMultiplePetsOrderedByFullName()
    {
        var result = await _service.GetOwnersWithMultiplePetsOrderedByFullNameAsync();

        Assert.Equal([1, 2, 3], result.Select(o => o.Id));
    }

    /// <summary>
    /// Приемы за месяц в выбранном кабинете
    /// </summary>
    [Theory]
    [InlineData("101", 2024, 6, new[] { 1, 2, 3, 13 })]
    [InlineData("205", 2024, 6, new[] { 4, 5 })]
    [InlineData("101", 2024, 5, new[] { 8, 9 })]
    public async Task GetAppointmentsByRoomAndMonth(string room, int year, int month, int[] expectedIds)
    {
        var result = await _service.GetAppointmentsByRoomAndMonthAsync(room, year, month);

        Assert.Equal(expectedIds, result.Select(a => a.Id));
    }
}
