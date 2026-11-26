using VetClinic.Domain.Entities;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Класс отвечающий за тестирование связности домена
/// </summary>
public class ClinicIntegrityTests(VetClinicFixture fixture) : IClassFixture<VetClinicFixture>
{
    /// <summary>
    /// Функция, проверяющая, заполнено ли поле владельца и животного и
    /// проверяющая соответствие поля владельца в таблицах приёмов и питомцев
    /// </summary>
    [Fact]
    public void EveryAppointmentShouldHaveValidAndMatchingPetAndOwner()
    {
        foreach (Appointment appointment in fixture.Appointments)
        {
            Assert.NotNull(appointment.Pet);
            Assert.NotNull(appointment.Pet.Owner);
            Assert.Equal(appointment.PetId, appointment.Pet.Id);
            Assert.Equal(appointment.Pet.OwnerId, appointment.Pet.Owner.Id);
        }
    }

    /// <summary>
    /// Функция, проверяющая соответствие породы животного и его вида
    /// </summary>
    [Fact]
    public void PetSpeciesShouldStrictlyMatchItsBreedTargetSpecies()
    {
        foreach (var pet in fixture.Pets)
        {
            Assert.NotNull(pet.Breed);
            Assert.Equal(pet.BreedId, pet.Breed.Id);
            Assert.Equal(pet.Type, pet.Breed.Type);
        }
    }
    /// <summary>
    /// Функция, проверяющая соответствие специализации врача и вида животного
    /// </summary>
    [Fact]
    public void AppointmentVetShouldBeSpecializedInAppointmentPetType()
    {
        foreach (Appointment appointment in fixture.Appointments)
        {
            Assert.NotNull(appointment.Vet);
            Assert.NotNull(appointment.Pet);
            Assert.Equal(appointment.Pet.Type, appointment.Vet.Specialization.TargetSpecies);
        }
    }
}