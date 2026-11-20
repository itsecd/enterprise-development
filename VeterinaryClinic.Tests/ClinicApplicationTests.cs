using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Tests.TestDoubles;

namespace VeterinaryClinic.Tests;

/// <summary>
/// Проверяет работу сервисов клиники
/// </summary>
public class ClinicApplicationTests
{
    private static async Task<ServiceProvider> CreateProviderAsync()
    {
        var services = new ServiceCollection();
        services.AddTestRepositories();
        services.AddScoped<PetService>();
        services.AddScoped<OwnerService>();
        services.AddScoped<BreedService>();
        services.AddScoped<AppointmentService>();
        services.AddScoped<VeterinarianService>();
        var provider = services.BuildServiceProvider();
        await ClinicTestSeed.SeedClinicTestDataAsync(provider);
        return provider;
    }

    [Fact]
    public async Task CreatedPetAppearsInOwnersAnalyticQuery()
    {
        using var provider = await CreateProviderAsync();
        var pets = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<OwnerService>();
        var before = (await owners.OwnersWithMultiplePetsAsync(TestContext.Current.CancellationToken)).Select(x => x.Id).ToHashSet();
        var created = await pets.CreateAsync(new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 8), TestContext.Current.CancellationToken);
        Assert.DoesNotContain(8, before);
        Assert.Contains(8, (await owners.OwnersWithMultiplePetsAsync(TestContext.Current.CancellationToken)).Select(x => x.Id));
        await pets.DeletePetAsync(created.Id, TestContext.Current.CancellationToken);
        Assert.DoesNotContain(8, (await owners.OwnersWithMultiplePetsAsync(TestContext.Current.CancellationToken)).Select(x => x.Id));
    }

    [Fact]
    public async Task InitialOwnersContainTheirPets()
    {
        using var provider = await CreateProviderAsync();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        foreach (var pet in await pets.GetAllAsync(TestContext.Current.CancellationToken))
        {
            if (pet.Owner is not null)
            {
                Assert.Contains(pet, pet.Owner.Pets);
                Assert.Same(await owners.GetAsync(pet.Owner.Id, TestContext.Current.CancellationToken), pet.Owner);
            }
        }
    }

    [Fact]
    public async Task CreatingAndReassigningPetUpdatesBothOwners()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var first = (await owners.GetAsync(1, TestContext.Current.CancellationToken))!;
        var second = (await owners.GetAsync(2, TestContext.Current.CancellationToken))!;
        var created = await service.CreateAsync(new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1), TestContext.Current.CancellationToken);
        var pet = (await pets.GetAsync(created.Id, TestContext.Current.CancellationToken))!;
        Assert.Contains(pet, first.Pets);

        await service.UpdateAsync(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 2), TestContext.Current.CancellationToken);
        Assert.DoesNotContain(pet, first.Pets);
        Assert.Contains(pet, second.Pets);
        Assert.Same(second, pet.Owner);

        await service.UpdateAsync(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, null), TestContext.Current.CancellationToken);
        Assert.DoesNotContain(pet, second.Pets);
        Assert.Null(pet.Owner);
    }

    [Fact]
    public async Task DeletingPetRemovesItFromOwnerCollection()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var owner = (await owners.GetAsync(1, TestContext.Current.CancellationToken))!;
        var created = await service.CreateAsync(new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1), TestContext.Current.CancellationToken);
        Assert.Contains(owner.Pets, pet => pet.Id == created.Id);
        await service.DeletePetAsync(created.Id, TestContext.Current.CancellationToken);
        Assert.DoesNotContain(owner.Pets, pet => pet.Id == created.Id);
    }

    [Fact]
    public async Task DeletingOwnerUnlinksTheirPets()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<OwnerService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var owner = (await owners.GetAsync(1, TestContext.Current.CancellationToken))!;
        var relatedPets = (await pets.GetAllAsync(TestContext.Current.CancellationToken)).Where(pet => pet.Owner?.Id == 1).ToArray();
        Assert.NotEmpty(relatedPets);
        await service.DeleteOwnerAsync(1, TestContext.Current.CancellationToken);
        Assert.Empty(owner.Pets);
        Assert.All(relatedPets, pet => Assert.Null(pet.Owner));
        Assert.Null(await owners.GetAsync(1, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task InvalidOwnerIsRejected()
    {
        using var provider = await CreateProviderAsync();
        var pets = provider.GetRequiredService<PetService>();
        await Assert.ThrowsAsync<ArgumentException>(async () => await pets.CreateAsync(
            new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 99999), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ChangingSpeciesOfUsedBreedIsRejected()
    {
        using var provider = await CreateProviderAsync();
        var breeds = provider.GetRequiredService<BreedService>();
        var existing = (await breeds.BreedsAsync(TestContext.Current.CancellationToken)).First(x => x.Id == 1);
        var different = Enum.GetValues<VeterinaryClinic.Domain.Shared.Enums.AnimalType>()
            .First(x => x != existing.AnimalType);
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await breeds.UpdateAsync(1,
            new SaveBreedRequest(existing.Name, different), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task PetBreedChangeRejectsIncompatibleExistingAppointment()
    {
        using var provider = await CreateProviderAsync();
        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        var pets = provider.GetRequiredService<PetService>();
        var breeds = provider.GetRequiredService<IRepository<Breed>>();
        var appointment = (await appointments.GetAllAsync(TestContext.Current.CancellationToken)).First();
        var pet = appointment.Pet;
        var incompatible = (await breeds.GetAllAsync(TestContext.Current.CancellationToken)).First(b
            => b.AnimalType != appointment.Veterinarian.Specialization.AnimalType);
        var originalBreed = pet.Breed;

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await pets.UpdateAsync(pet.Id,
            new SavePetRequest(pet.Name, incompatible.Id, pet.BirthDate, pet.Weight, pet.Owner?.Id), TestContext.Current.CancellationToken));
        Assert.Same(originalBreed, pet.Breed);
    }

    [Fact]
    public async Task VeterinarianSpecializationChangeRejectsIncompatibleExistingAppointment()
    {
        using var provider = await CreateProviderAsync();
        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        var veterinarians = provider.GetRequiredService<VeterinarianService>();
        var specializations = provider.GetRequiredService<IRepository<Specialization>>();
        var appointment = (await appointments.GetAllAsync(TestContext.Current.CancellationToken)).First();
        var vet = appointment.Veterinarian;
        var incompatible = (await specializations.GetAllAsync(TestContext.Current.CancellationToken)).First(s => s.AnimalType != appointment.Pet.Breed.AnimalType);
        var original = vet.Specialization;

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await veterinarians.UpdateAsync(vet.Id,
            new SaveVeterinarianRequest(vet.LastName, vet.FirstName, vet.Patronymic, vet.Phone,
                vet.PassportNumber, vet.BirthDate, incompatible.Id, vet.ExperienceYears), TestContext.Current.CancellationToken));
        Assert.Same(original, vet.Specialization);
    }

    [Fact]
    public async Task AppointmentRelationsAreAvailableFromBothSides()
    {
        using var provider = await CreateProviderAsync();
        var appointment = (await provider.GetRequiredService<IRepository<Appointment>>().GetAllAsync(TestContext.Current.CancellationToken)).First();
        var pets = provider.GetRequiredService<PetService>();
        var vets = provider.GetRequiredService<VeterinarianService>();
        Assert.Contains(await pets.AppointmentsByPetAsync(appointment.Pet.Id, TestContext.Current.CancellationToken), a => a.Id == appointment.Id);
        Assert.Contains(await vets.AppointmentsByVeterinarianAsync(appointment.Veterinarian.Id, TestContext.Current.CancellationToken), a => a.Id == appointment.Id);
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await pets.AppointmentsByPetAsync(-1, TestContext.Current.CancellationToken));
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await vets.AppointmentsByVeterinarianAsync(-1, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task SeedContainsTenSpecializationsWithSequentialIds()
    {
        using var provider = await CreateProviderAsync();
        var ids = (await provider.GetRequiredService<IRepository<Specialization>>()
            .GetAllAsync(TestContext.Current.CancellationToken)).Select(x => x.Id).OrderBy(x => x).ToArray();
        Assert.Equal(Enumerable.Range(1, 10), ids);
    }

    [Fact]
    public async Task HistoricalAppointmentIsRejected()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<AppointmentService>();
        await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(
            new SaveAppointmentRequest(1, 1, new DateTime(1999, 1, 1), 101, false), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task DuplicateRoomAndTimeIsRejectedOnCreateAndUpdate()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<AppointmentService>();
        var time = DateTime.Today.AddDays(30).AddHours(10);
        var first = await service.CreateAsync(new SaveAppointmentRequest(1, 1, time, 909, false), TestContext.Current.CancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateAsync(
            new SaveAppointmentRequest(1, 1, time, 909, true), TestContext.Current.CancellationToken));
        var second = await service.CreateAsync(new SaveAppointmentRequest(1, 1, time.AddHours(1), 909, false), TestContext.Current.CancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.UpdateAsync(second.Id,
            new SaveAppointmentRequest(1, 1, time, 909, false), TestContext.Current.CancellationToken));
        Assert.Equal(first.Id, (await service.UpdateAsync(first.Id,
            new SaveAppointmentRequest(1, 1, time, 909, false), TestContext.Current.CancellationToken)).Id);
    }

    [Fact]
    public async Task ImpossibleVeterinarianExperienceIsRejected()
    {
        using var provider = await CreateProviderAsync();
        var service = provider.GetRequiredService<VeterinarianService>();
        var invalid = new SaveVeterinarianRequest("Test", "Doctor", null, "123", "12345",
            new DateOnly(2020, 1, 1), 1, 30);
        await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(invalid, TestContext.Current.CancellationToken));
        await Assert.ThrowsAsync<ArgumentException>(async () => await service.UpdateAsync(1, invalid, TestContext.Current.CancellationToken));
    }
}
