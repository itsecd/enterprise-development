using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Tests;

/// <summary>
/// Проверяет работу тестовых данных клиники
/// </summary>
public class VeterinaryClinicTests
{
    public static IEnumerable<object[]> VeterinarianCases =>
    [
        [1, new[] { 2, 11, 1 }],
        [2, new[] { 3, 4 }],
        [3, new[] { 5, 6 }],
        [4, new[] { 17, 8, 7 }],
        [5, new[] { 9, 10, 19 }],
        [6, new[] { 13, 12, 20 }],
        [7, new[] { 14, 15 }],
        [8, new[] { 16 }],
        [9, new[] { 18 }],
        [10, Array.Empty<int>()],
        [11, Array.Empty<int>()]
    ];

    public static IEnumerable<object[]> BreedCases =>
    [
        [1, 6],
        [2, 2],
        [3, 0],
        [4, 2],
        [5, 0],
        [6, 1],
        [7, 0],
        [8, 1],
        [9, 0],
        [10, 2],
        [11, 0]
    ];
    [Theory]
    [InlineData(AnimalType.Dog, new int[] { 1, 6, 8 })]
    [InlineData(AnimalType.Cat, new int[] { 2, 7 })]
    [InlineData(AnimalType.Bird, new int[] { 3, 9 })]
    [InlineData(AnimalType.Rabbit, new int[] { 4 })]
    [InlineData(AnimalType.Reptile, new int[] { 5 })]
    [InlineData(AnimalType.Rodent, new int[] { 10, 11 })]
    public void GetVeterinariansByAnimalType(AnimalType animalType, int[] expectedIds)
    {
        var result = VeterinaryClinicTestData.Veterinarians
            .Where(veterinarian => veterinarian.Specialization.AnimalType == animalType)
            .OrderBy(veterinarian => veterinarian.LastName)
            .ThenBy(veterinarian => veterinarian.FirstName)
            .Select(veterinarian => veterinarian.Id)
            .ToArray();

        Assert.Equal(expectedIds, result);
    }
    [Theory]
    [MemberData(nameof(VeterinarianCases))]
    public void GetPetsByVeterinarian(int veterinarianId, int[] expectedPetIds)
    {
        var veterinarian = VeterinaryClinicTestData.Veterinarians
            .Single(vet => vet.Id == veterinarianId);

        var result = VeterinaryClinicTestData.Appointments
            .Where(appointment => appointment.Veterinarian.Id == veterinarian.Id)
            .Select(appointment => appointment.Pet)
            .DistinctBy(pet => pet.Id)
            .OrderBy(pet => pet.Name)
            .Select(pet => pet.Id)
            .ToArray();

        Assert.Equal(expectedPetIds, result);
    }
    [Theory]
    [MemberData(nameof(BreedCases))]
    public void GetRepeatedAppointmentsByBreed(int breedId, int expectedCount)
    {
        var breed = VeterinaryClinicTestData.Breeds.Single(item => item.Id == breedId);

        var result = VeterinaryClinicTestData.Appointments
            .Count(appointment => appointment.IsRepeated && appointment.Pet.Breed.Id == breed.Id);

        Assert.Equal(expectedCount, result);
    }
    [Fact]
    public void GetOwnersWithMoreThanOnePet()
    {
        var result = VeterinaryClinicTestData.Owners
            .Where(owner => owner.Pets.Count > 1)
            .OrderBy(owner => owner.LastName)
            .ThenBy(owner => owner.FirstName)
            .ThenBy(owner => owner.Patronymic)
            .Select(owner => owner.Id)
            .ToArray();

        var expectedIds = new[] { 1, 2, 3, 4, 5, 6, 7, 9, 10 };

        Assert.Equal(expectedIds, result);
    }
    [Fact]
    public void GetAppointmentsForCurrentMonthByRoom()
    {
        var result = VeterinaryClinicTestData.Appointments
            .Where(appointment =>
                appointment.RoomNumber == 101 &&
                appointment.AppointmentDate.Month == DateTime.Today.Month &&
                appointment.AppointmentDate.Year == DateTime.Today.Year)
            .OrderBy(appointment => appointment.AppointmentDate)
            .Select(appointment => appointment.Id)
            .ToArray();

        var expectedIds = new[] { 1, 2, 5 };

        Assert.Equal(expectedIds, result);
    }
}
