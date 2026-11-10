using Microsoft.Extensions.Logging.Abstractions;
using Vet.Application.Services;
using Vet.Domain.DataSeed;
using Vet.Domain.Enums;
using Vet.Infrastructure.InMemory.Repositories;

namespace Vet.Tests;

/// <summary>
/// Тесты аналитических запросов к данным ветеринарной клиники
/// </summary>
public class VetClinicTest : IClassFixture<VetClinicTestFixture>
{
    private readonly AnalyticsService _analyticsService;

    /// <summary>
    /// Инициализация сервиса аналитики со всеми репозиториями
    /// </summary>
    /// <param name="fixture">Тестовый набор данных (датасид)</param>
    public VetClinicTest(VetClinicTestFixture fixture)
    {
        var veterinarianRepository = new VeterinarianRepository(fixture);
        var appointmentRepository = new AppointmentRepository(fixture);
        var ownerRepository = new OwnerRepository(fixture);
        var breedRepository = new BreedRepository(fixture);


        _analyticsService = new AnalyticsService(
            veterinarianRepository,
            appointmentRepository,
            ownerRepository,
            breedRepository,
            NullLogger<AnalyticsService>.Instance);
    }

    /// <summary>
    /// Запрос 1: Проверяет получение ветеринаров, специализирующихся на выбранном виде животного
    /// </summary>
    [Fact]
    public async Task GetVeterinariansBySpecies_ReturnsVetsSpecializedOnCat()
    {
        var targetSpecies = Species.Cat;
        var expectedIds = new HashSet<int> { 0, 3, 4, 7, 8 };
        var excludedIds = new HashSet<int> { 1, 2, 5, 6, 9 };

        var resultVets = await _analyticsService.GetVeterinariansBySpeciesAsync(targetSpecies);

        Assert.Equal(expectedIds.Count, resultVets.Count);
        Assert.All(expectedIds, id => Assert.Contains(resultVets, v => v.Id == id));
        Assert.All(excludedIds, id => Assert.DoesNotContain(resultVets, v => v.Id == id));
    }

    /// <summary>
    /// Запрос 2: Проверяет получение питомцев, записанных на приём к указанному врачу, упорядоченных по кличке
    /// </summary>
    [Fact]
    public async Task GetPetsByVeterinarian_OrderedByName_DistinctByPet()
    {
        var targetVetId = 0;
        var firstExpectedName = "Барсик";
        var secondExpectedName = "Рекс";

        var resultPets = await _analyticsService.GetPetsByVeterinarianAsync(targetVetId);

        Assert.Equal(2, resultPets.Count);
        Assert.Equal(firstExpectedName, resultPets[0].Name);
        Assert.Equal(secondExpectedName, resultPets[1].Name);
    }

    /// <summary>
    /// Запрос 3: Проверяет получение количества повторных приёмов у выбранной породы
    /// </summary>
    [Fact]
    public async Task GetRepeatAppointmentsCountByBreed_ReturnsExpected()
    {
        var labradorBreedId = 0;
        var corellaBreedId = 5;
        var expectedLabradorCount = 3;
        var expectedCorellaRepeatCount = 0;

        var labradorRepeatCount = await _analyticsService.GetRepeatAppointmentsCountByBreedAsync(labradorBreedId);
        var corellaRepeatCount = await _analyticsService.GetRepeatAppointmentsCountByBreedAsync(corellaBreedId);

        Assert.Equal(expectedLabradorCount, labradorRepeatCount);
        Assert.Equal(expectedCorellaRepeatCount, corellaRepeatCount);
    }

    /// <summary>
    /// Запрос 4: Проверяет получение владельцев с более чем одним питомцем, упорядоченных по ФИО
    /// </summary>
    [Fact]
    public async Task GetOwnersWithMultiplePets_OrderedByFullName()
    {
        var expectedNames = new[]
        {
            "Иванов Иван Иванович",
            "Кузнецова Ольга Андреевна",
            "Сидоров Алексей Владимирович"
        };

        var resultOwners = await _analyticsService.GetOwnersWithMultiplePetsAsync();

        Assert.Equal(expectedNames.Length, resultOwners.Count);
        for (var i = 0; i < expectedNames.Length; i++)
        {
            Assert.Equal(expectedNames[i], resultOwners[i].FullName);
        }
    }

    /// <summary>
    /// Запрос 5: Проверяет получение приёмов за выбранный месяц в выбранном кабинете
    /// </summary>
    [Fact]
    public async Task GetAppointmentsByRoomThisMonth_Room101_ReturnsEight()
    {
        var targetRoom = 101;
        var referenceDate = VetClinicTestFixture.ReferenceDate;
        var expectedCount = 8;
        var expectedFirstAppointmentId = 0;

        var resultAppointments = await _analyticsService.GetAppointmentsByRoomAsync(
            targetRoom,
            referenceDate.Year,
            referenceDate.Month);

        Assert.Equal(expectedCount, resultAppointments.Count);
        Assert.Equal(expectedFirstAppointmentId, resultAppointments[0].Id);
        Assert.All(resultAppointments, a => Assert.Equal(targetRoom, a.RoomNumber));
    }
}