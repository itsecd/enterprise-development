using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Infrastructure;

namespace VeterinaryClinic.Tests;

/// <summary>
/// Проверяет работу сервисов клиники
/// </summary>
public class ClinicApplicationTests
{
    private static ServiceProvider CreateProvider()
    {
        var services = new ServiceCollection();
        services.AddClinicRepositories();
        services.AddScoped<PetService>();
        services.AddScoped<OwnerService>();
        services.AddScoped<BreedService>();
        services.AddScoped<AppointmentService>();
        services.AddScoped<VeterinarianService>();
        var provider = services.BuildServiceProvider();
        ClinicSeedFactory.SeedClinicTestData(provider);
        return provider;
    }

    [Fact]
    public void CreatedPetAppearsInOwnersAnalyticQuery()
    {
        using var provider = CreateProvider();
        var pets = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<OwnerService>();
        var before = owners.OwnersWithMultiplePets().Select(x => x.Id).ToHashSet();
        var created = pets.Create(new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 8));
        Assert.DoesNotContain(8, before);
        Assert.Contains(8, owners.OwnersWithMultiplePets().Select(x => x.Id));
        pets.DeletePet(created.Id);
        Assert.DoesNotContain(8, owners.OwnersWithMultiplePets().Select(x => x.Id));
    }

    [Fact]
    public void InitialOwnersContainTheirPets()
    {
        using var provider = CreateProvider();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        foreach (var pet in pets.GetAll())
        {
            if (pet.Owner is not null)
            {
                Assert.Contains(pet, pet.Owner.Pets);
                Assert.Same(owners.Get(pet.Owner.Id), pet.Owner);
            }
        }
    }

    [Fact]
    public void CreatingAndReassigningPetUpdatesBothOwners()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var first = owners.Get(1)!;
        var second = owners.Get(2)!;
        var created = service.Create(new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1));
        var pet = pets.Get(created.Id)!;
        Assert.Contains(pet, first.Pets);

        service.Update(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 2));
        Assert.DoesNotContain(pet, first.Pets);
        Assert.Contains(pet, second.Pets);
        Assert.Same(second, pet.Owner);

        service.Update(created.Id, new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, null));
        Assert.DoesNotContain(pet, second.Pets);
        Assert.Null(pet.Owner);
    }

    [Fact]
    public void DeletingPetRemovesItFromOwnerCollection()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<PetService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var owner = owners.Get(1)!;
        var created = service.Create(new SavePetRequest("New pet", 1, new DateOnly(2022, 1, 1), 3.5, 1));
        Assert.Contains(owner.Pets, pet => pet.Id == created.Id);
        service.DeletePet(created.Id);
        Assert.DoesNotContain(owner.Pets, pet => pet.Id == created.Id);
    }

    [Fact]
    public void DeletingOwnerUnlinksTheirPets()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<OwnerService>();
        var owners = provider.GetRequiredService<IRepository<Owner>>();
        var pets = provider.GetRequiredService<IRepository<Pet>>();
        var owner = owners.Get(1)!;
        var relatedPets = pets.GetAll().Where(pet => pet.Owner?.Id == 1).ToArray();
        Assert.NotEmpty(relatedPets);
        service.DeleteOwner(1);
        Assert.Empty(owner.Pets);
        Assert.All(relatedPets, pet => Assert.Null(pet.Owner));
        Assert.Null(owners.Get(1));
    }

    [Fact]
    public void InvalidOwnerIsRejected()
    {
        using var provider = CreateProvider();
        var pets = provider.GetRequiredService<PetService>();
        Assert.Throws<ArgumentException>(() => pets.Create(
            new SavePetRequest("Test", 1, new DateOnly(2020, 1, 1), 2.5, 99999)));
    }

    [Fact]
    public void ChangingSpeciesOfUsedBreedIsRejected()
    {
        using var provider = CreateProvider();
        var breeds = provider.GetRequiredService<BreedService>();
        var existing = breeds.Breeds().First(x => x.Id == 1);
        var different = Enum.GetValues<VeterinaryClinic.Domain.Shared.Enums.AnimalType>()
            .First(x => x != existing.AnimalType);
        Assert.Throws<InvalidOperationException>(() => breeds.Update(1,
            new SaveBreedRequest(existing.Name, different)));
    }

    [Fact]
    public void PetBreedChangeRejectsIncompatibleExistingAppointment()
    {
        using var provider = CreateProvider();
        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        var pets = provider.GetRequiredService<PetService>();
        var breeds = provider.GetRequiredService<IRepository<Breed>>();
        var appointment = appointments.GetAll().First();
        var pet = appointment.Pet;
        var incompatible = breeds.GetAll().First(b
            => b.AnimalType != appointment.Veterinarian.Specialization.AnimalType);
        var originalBreed = pet.Breed;

        Assert.Throws<InvalidOperationException>(() => pets.Update(pet.Id,
            new SavePetRequest(pet.Name, incompatible.Id, pet.BirthDate, pet.Weight, pet.Owner?.Id)));
        Assert.Same(originalBreed, pet.Breed);
    }

    [Fact]
    public void VeterinarianSpecializationChangeRejectsIncompatibleExistingAppointment()
    {
        using var provider = CreateProvider();
        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        var veterinarians = provider.GetRequiredService<VeterinarianService>();
        var specializations = provider.GetRequiredService<IRepository<Specialization>>();
        var appointment = appointments.GetAll().First();
        var vet = appointment.Veterinarian;
        var incompatible = specializations.GetAll().First(s => s.AnimalType != appointment.Pet.Breed.AnimalType);
        var original = vet.Specialization;

        Assert.Throws<InvalidOperationException>(() => veterinarians.Update(vet.Id,
            new SaveVeterinarianRequest(vet.LastName, vet.FirstName, vet.Patronymic, vet.Phone,
                vet.PassportNumber, vet.BirthDate, incompatible.Id, vet.ExperienceYears)));
        Assert.Same(original, vet.Specialization);
    }

    [Fact]
    public void AppointmentRelationsAreAvailableFromBothSides()
    {
        using var provider = CreateProvider();
        var appointment = provider.GetRequiredService<IRepository<Appointment>>().GetAll().First();
        var pets = provider.GetRequiredService<PetService>();
        var vets = provider.GetRequiredService<VeterinarianService>();
        Assert.Contains(pets.AppointmentsByPet(appointment.Pet.Id), a => a.Id == appointment.Id);
        Assert.Contains(vets.AppointmentsByVeterinarian(appointment.Veterinarian.Id), a => a.Id == appointment.Id);
        Assert.Throws<KeyNotFoundException>(() => pets.AppointmentsByPet(-1));
        Assert.Throws<KeyNotFoundException>(() => vets.AppointmentsByVeterinarian(-1));
    }

    [Fact]
    public void SeedContainsTenSpecializationsWithSequentialIds()
    {
        using var provider = CreateProvider();
        var ids = provider.GetRequiredService<IRepository<Specialization>>()
            .GetAll().Select(x => x.Id).OrderBy(x => x).ToArray();
        Assert.Equal(Enumerable.Range(1, 10), ids);
    }

    [Fact]
    public void HistoricalAppointmentIsRejected()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<AppointmentService>();
        Assert.Throws<ArgumentException>(() => service.Create(
            new SaveAppointmentRequest(1, 1, new DateTime(1999, 1, 1), 101, false)));
    }

    [Fact]
    public void DuplicateRoomAndTimeIsRejectedOnCreateAndUpdate()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<AppointmentService>();
        var time = DateTime.Today.AddDays(30).AddHours(10);
        var first = service.Create(new SaveAppointmentRequest(1, 1, time, 909, false));
        Assert.Throws<InvalidOperationException>(() => service.Create(
            new SaveAppointmentRequest(1, 1, time, 909, true)));
        var second = service.Create(new SaveAppointmentRequest(1, 1, time.AddHours(1), 909, false));
        Assert.Throws<InvalidOperationException>(() => service.Update(second.Id,
            new SaveAppointmentRequest(1, 1, time, 909, false)));
        Assert.Equal(first.Id, service.Update(first.Id,
            new SaveAppointmentRequest(1, 1, time, 909, false)).Id);
    }

    [Fact]
    public void ImpossibleVeterinarianExperienceIsRejected()
    {
        using var provider = CreateProvider();
        var service = provider.GetRequiredService<VeterinarianService>();
        var invalid = new SaveVeterinarianRequest("Test", "Doctor", null, "123", "12345",
            new DateOnly(2020, 1, 1), 1, 30);
        Assert.Throws<ArgumentException>(() => service.Create(invalid));
        Assert.Throws<ArgumentException>(() => service.Update(1, invalid));
    }
}
