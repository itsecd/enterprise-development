using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using VetClinic.Application.Contracts.Appointments;
using VetClinic.Application.Contracts.Owners;
using VetClinic.Application.Contracts.Pets;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Domain.Shared.Enums;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Сквозные интеграционные тесты REST API поверх реальной базы данных MySQL в Docker.
/// </summary>
public class VetClinicIntegrationTests(VetClinicAspireFixture fixture) : IClassFixture<VetClinicAspireFixture>
{
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    /// Проверяет, что миграция успешно применилась и начальные данные из сидера загружаются из MySQL.
    /// </summary>
    [Fact]
    public async Task GetAllPetsFromMySql()
    {
        var response = await fixture.HttpClient.GetAsync("/api/Pet", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var pets = await response.Content.ReadFromJsonAsync<List<PetDto>>(_jsonOptions, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(pets);
        Assert.True(pets.Count >= 17, "Ожидалось минимум 17 питомцев из начального сидера.");
    }

    /// <summary>
    /// Проверяет полный жизненный цикл сущности (POST -> GET -> PUT -> DELETE -> 404) в реальной БД
    /// </summary>
    [Fact]
    public async Task PetCrudLifecycleInMySql()
    {
        // CREATE (POST)
        var createDto = new PetCreateUpdateDto(
            Name: "Интеграционный Кот",
            Type: Species.Cat,
            BirthDate: new DateOnly(2024, 5, 10),
            Weight: 5,
            BreedId: 1,
            OwnerId: 1
        );

        var postResponse = await fixture.HttpClient.PostAsJsonAsync("/api/Pet", createDto, _jsonOptions, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<PetDto>(_jsonOptions, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal("Интеграционный Кот", created.Name);

        var getResponse = await fixture.HttpClient.GetAsync($"/api/Pet/{created.Id}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var updateDto = createDto with { Name = "Обновленный Кот", Weight = 6 };
        var putResponse = await fixture.HttpClient.PutAsJsonAsync($"/api/Pet/{created.Id}", updateDto, _jsonOptions, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);

        var updated = await putResponse.Content.ReadFromJsonAsync<PetDto>(_jsonOptions, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(updated);
        Assert.Equal("Обновленный Кот", updated.Name);

        var deleteResponse = await fixture.HttpClient.DeleteAsync($"/api/Pet/{created.Id}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var checkResponse = await fixture.HttpClient.GetAsync($"/api/Pet/{created.Id}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NotFound, checkResponse.StatusCode);
    }

    /// <summary>
    /// Проверяет корректную обработку ошибки при попытке сослаться на несуществующего владельца.
    /// </summary>
    [Fact]
    public async Task CreatePetWithInvalidOwnerId()
    {
        var invalidDto = new PetCreateUpdateDto(
            Name: "Призрак",
            Type: Species.Dog,
            BirthDate: new DateOnly(2023, 1, 1),
            Weight: 10,
            BreedId: 1,
            OwnerId: 99999
        );

        var response = await fixture.HttpClient.PostAsJsonAsync("/api/Pet", invalidDto, _jsonOptions, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Проверяет выполнение всех 5 аналитических запросов из первой лабы
    /// </summary>
    [Fact]
    public async Task AnalyticsQueries_ReturnExpectedResults_FromMySql()
    {
        const string baseRoute = "/api/Analytic";
        var ct = TestContext.Current.CancellationToken;

        var vetsResp = await fixture.HttpClient.GetAsync($"{baseRoute}/vets/by-species/Cat", ct);
        Assert.Equal(HttpStatusCode.OK, vetsResp.StatusCode);
        var vets = await vetsResp.Content.ReadFromJsonAsync<List<VetDto>>(_jsonOptions, cancellationToken: ct);
        Assert.NotNull(vets);
        Assert.NotEmpty(vets);

        var petsByVetResp = await fixture.HttpClient.GetAsync($"{baseRoute}/pets/by-vet/1", ct);
        Assert.Equal(HttpStatusCode.OK, petsByVetResp.StatusCode);

        var repeatResp = await fixture.HttpClient.GetAsync($"{baseRoute}/appointments/repeat-count/by-breed/1", ct);
        Assert.Equal(HttpStatusCode.OK, repeatResp.StatusCode);
        var repeatCount = await repeatResp.Content.ReadFromJsonAsync<int>(_jsonOptions, cancellationToken: ct);
        Assert.True(repeatCount >= 0);

        var ownersResp = await fixture.HttpClient.GetAsync($"{baseRoute}/owners/multiple-pets", ct);
        Assert.Equal(HttpStatusCode.OK, ownersResp.StatusCode);
        var owners = await ownersResp.Content.ReadFromJsonAsync<List<OwnerDto>>(_jsonOptions, cancellationToken: ct);
        Assert.NotNull(owners);
        Assert.NotEmpty(owners);

        var monthResp = await fixture.HttpClient.GetAsync($"{baseRoute}/appointments/current-month/by-room/102?month=9&year=2026", ct);
        Assert.Equal(HttpStatusCode.OK, monthResp.StatusCode);
        var appointments = await monthResp.Content.ReadFromJsonAsync<List<AppointmentDto>>(_jsonOptions, cancellationToken: ct);
        Assert.NotNull(appointments);
        Assert.NotEmpty(appointments);
    }
}
