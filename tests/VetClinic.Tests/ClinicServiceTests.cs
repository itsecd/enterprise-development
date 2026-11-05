using VetClinic.Contracts.Appointments;
using VetClinic.Contracts.Breeds;
using VetClinic.Contracts.Owners;
using VetClinic.Contracts.Pets;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;

namespace VetClinic.Tests;

public class ClinicServiceTests
{
    private static CancellationToken Cancel => TestContext.Current.CancellationToken;

    [Fact]
    public async Task VeterinariansOfCat_MatchLabQuery()
    {
        var clinic = new ClinicHarness();

        var ids = (await clinic.Veterinarians.GetBySpeciesAsync(AnimalSpecies.Cat, Cancel))
            .Select(veterinarian => veterinarian.Id)
            .ToArray();

        Assert.Equal([1, 2, 6, 11], ids);
    }

    [Fact]
    public async Task PetsOfVeterinarian_AreOrderedByName()
    {
        var clinic = new ClinicHarness();

        var ids = (await clinic.Pets.GetByVeterinarianAsync(3, Cancel)).Select(pet => pet.Id).ToArray();

        Assert.Equal([4, 3, 15], ids);
    }

    [Fact]
    public async Task FollowUpsOfBreed_MatchLabQuery()
    {
        var clinic = new ClinicHarness();

        Assert.Equal(3, (await clinic.Appointments.CountFollowUpsAsync(1, Cancel)).Count);
        Assert.Equal(0, (await clinic.Appointments.CountFollowUpsAsync(7, Cancel)).Count);
    }

    [Fact]
    public async Task OwnersWithSeveralPets_AreOrderedByFullName()
    {
        var clinic = new ClinicHarness();

        var ids = (await clinic.Owners.GetWithSeveralPetsAsync(Cancel)).Select(owner => owner.Id).ToArray();

        Assert.Equal([2, 5, 7, 1], ids);
    }

    [Fact]
    public async Task SeptemberAppointmentsInRoom101_MatchLabQuery()
    {
        var clock = new FixedTimeProvider(new DateTimeOffset(2026, 9, 15, 12, 0, 0, TimeSpan.Zero));
        var clinic = new ClinicHarness(clock);

        var ids = (await clinic.Appointments.GetCurrentMonthAsync("101", Cancel))
            .Select(appointment => appointment.Id)
            .ToArray();

        Assert.Equal([13, 15, 23, 24, 28], ids);
    }

    [Fact]
    public async Task NewBreed_DoesNotReuseDeletedIdentifier()
    {
        var clinic = new ClinicHarness();
        var created = await clinic.Breeds.CreateAsync(new BreedEditDto { Name = "Дворняга", Species = AnimalSpecies.Dog }, Cancel);

        await clinic.Breeds.DeleteAsync(created.Id, Cancel);
        var next = await clinic.Breeds.CreateAsync(new BreedEditDto { Name = "Мопс", Species = AnimalSpecies.Dog }, Cancel);

        Assert.NotEqual(created.Id, next.Id);
    }

    [Fact]
    public async Task DeleteUsedBreed_Throws()
    {
        var clinic = new ClinicHarness();

        var exception = await Assert.ThrowsAsync<EntityInUseException>(() => clinic.Breeds.DeleteAsync(1, Cancel));

        Assert.Contains("Мейн-кун", exception.Message);
    }

    [Fact]
    public async Task CreateAppointment_RejectsSpeciesMismatch()
    {
        var clinic = new ClinicHarness();

        var exception = await Assert.ThrowsAsync<DomainRuleException>(() => clinic.Appointments.CreateAsync(new AppointmentEditDto
        {
            PetId = 1,
            VeterinarianId = 3,
            VisitDateTime = new DateTime(2026, 10, 20, 10, 0, 0),
            RoomNumber = "101",
            IsFollowUp = false
        }, Cancel));

        Assert.Contains("специализацией", exception.Message);
    }

    [Fact]
    public async Task CreateBreed_WithoutSpecies_Throws()
    {
        var clinic = new ClinicHarness();

        await Assert.ThrowsAsync<DomainRuleException>(() => clinic.Breeds.CreateAsync(new BreedEditDto
        {
            Name = "Беспородная",
            Species = null
        }, Cancel));
    }

    [Fact]
    public async Task CreateOwner_RejectsInvalidPhone()
    {
        var clinic = new ClinicHarness();

        await Assert.ThrowsAsync<DomainRuleException>(() => clinic.Owners.CreateAsync(new OwnerEditDto
        {
            FullName = "Тестов Тест Тестович",
            PhoneNumber = "не телефон",
            Address = null
        }, Cancel));
    }

    [Fact]
    public async Task SeedOwnerPhone_PassesValidation()
    {
        var clinic = new ClinicHarness();
        var owner = await clinic.Owners.GetByIdAsync(1, Cancel);

        var updated = await clinic.Owners.UpdateAsync(owner.Id, new OwnerEditDto
        {
            FullName = owner.FullName,
            PhoneNumber = owner.PhoneNumber,
            Address = owner.Address
        }, Cancel);

        Assert.Equal(owner.PhoneNumber, updated.PhoneNumber);
    }

    [Fact]
    public async Task CreatePet_AppearsInOwnerCollection()
    {
        var clinic = new ClinicHarness();

        var created = await clinic.Pets.CreateAsync(new PetEditDto
        {
            Name = "Бобик",
            BreedId = 4,
            OwnerId = 8,
            BirthDate = new DateOnly(2024, 1, 10),
            Weight = 12.5
        }, Cancel);

        var pets = await clinic.Pets.GetByOwnerAsync(8, Cancel);
        Assert.Contains(pets, pet => pet.Id == created.Id);
    }
}
