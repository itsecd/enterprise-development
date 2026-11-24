using System.Net;
using System.Net.Http.Json;

namespace VetClinic.IntegrationTests;

/// <summary>
/// Проверки API на настоящей базе SQL Server, поднятой через Aspire
/// </summary>
public class ApiTests(ClinicApplicationFixture fixture) : IClassFixture<ClinicApplicationFixture>
{
    private readonly ClinicApplicationFixture _fixture = fixture;

    /// <summary>
    /// Миграции применились и начальные данные на месте
    /// </summary>
    [Fact]
    public async Task GetOwners_AfterStart_ReturnsSeedOwners()
    {
        var owners = await GetAsync<List<OwnerResponse>>("api/owners");

        Assert.True(owners.Count >= 10);
    }

    /// <summary>
    /// Без токена изменяющий запрос отклоняется
    /// </summary>
    [Fact]
    public async Task CreatePet_WithoutToken_ReturnsUnauthorized()
    {
        using var client = _fixture.CreateAnonymousClient();
        using var response = await client.PostAsJsonAsync("api/pets", Pet(species: "Dog", breedId: 4),
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    /// <summary>
    /// Порода другого вида не принимается: «Персидская» относится к кошкам
    /// </summary>
    [Fact]
    public async Task CreatePet_WithBreedOfAnotherSpecies_ReturnsBadRequest()
    {
        using var response = await _fixture.Api.PostAsJsonAsync("api/pets", Pet(species: "Dog", breedId: 4),
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Вид породы, к которой привязаны питомцы, менять нельзя
    /// </summary>
    [Fact]
    public async Task UpdateBreed_SpeciesWithPetsAttached_ReturnsConflict()
    {
        using var response = await _fixture.Api.PutAsJsonAsync("api/breeds/1",
            new { name = "Лабрадор", species = "Cat" }, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    /// <summary>
    /// Повтор номера паспорта врача отклоняется базой
    /// </summary>
    [Fact]
    public async Task CreateVet_WithDuplicatePassport_ReturnsConflict()
    {
        var vet = new
        {
            passportNumber = "4501 000001",
            fullName = "Тестов Тест Тестович",
            birthYear = 1990,
            workExperienceYears = 5
        };

        using var response = await _fixture.Api.PostAsJsonAsync("api/vets", vet, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    /// <summary>
    /// Удаление владельца удаляет его питомцев на стороне базы
    /// </summary>
    [Fact]
    public async Task DeleteOwner_RemovesHisPets()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var owner = await PostAsync<OwnerResponse>("api/owners", new
        {
            fullName = "Временный Владелец Тестович",
            address = "ул. Проверочная, 1",
            phone = "+79001234567"
        });
        var pet = await PostAsync<PetResponse>("api/pets", Pet(species: "Dog", breedId: 1, ownerId: owner.Id));

        using var deleteResponse = await _fixture.Api.DeleteAsync($"api/owners/{owner.Id}", cancellationToken);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        using var petCheck = await _fixture.Api.GetAsync($"api/pets/{pet.Id}", cancellationToken);
        Assert.Equal(HttpStatusCode.NotFound, petCheck.StatusCode);
    }

    /// <summary>
    /// Одновременное создание питомца и смена вида его породы не оставляют питомца с чужим видом
    /// </summary>
    [Fact]
    public async Task CreatePetAndChangeBreed_InParallel_KeepDataConsistent()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var breed = await PostAsync<BreedResponse>("api/breeds", new { name = "Параллельная", species = "Dog" });

        var responses = await Task.WhenAll(
            _fixture.Api.PostAsJsonAsync("api/pets", Pet(species: "Dog", breedId: breed.Id), cancellationToken),
            _fixture.Api.PutAsJsonAsync($"api/breeds/{breed.Id}", new { name = "Параллельная", species = "Cat" }, cancellationToken));
        foreach (var response in responses)
        {
            response.Dispose();
        }

        var storedBreed = (await GetAsync<List<BreedResponse>>("api/breeds")).Single(item => item.Id == breed.Id);
        var pets = await GetAsync<List<PetResponse>>("api/pets");
        Assert.DoesNotContain(pets, pet => pet.BreedId == breed.Id && pet.Species != storedBreed.Species);
    }

    private static object Pet(string species, int breedId, int ownerId = 1) => new
    {
        nickname = "Проверка",
        species,
        breedId,
        birthDate = "2020-01-01",
        weight = 10,
        ownerId
    };

    private async Task<T> GetAsync<T>(string url) =>
        (await _fixture.Api.GetFromJsonAsync<T>(url, TestContext.Current.CancellationToken))!;

    private async Task<T> PostAsync<T>(string url, object body)
    {
        using var response = await _fixture.Api.PostAsJsonAsync(url, body, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(TestContext.Current.CancellationToken))!;
    }

    /// <summary>
    /// Владелец из ответа API
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    private sealed record OwnerResponse(int Id);

    /// <summary>
    /// Питомец из ответа API
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="BreedId">Идентификатор породы</param>
    /// <param name="Species">Вид питомца</param>
    private sealed record PetResponse(int Id, int? BreedId, string Species);

    /// <summary>
    /// Порода из ответа API
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="Species">Вид животного</param>
    private sealed record BreedResponse(int Id, string Species);
}
