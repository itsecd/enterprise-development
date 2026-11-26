using VetClinic.Domain.Entities;
using VetClinic.Domain.Seeder;
namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Данный класс загружает и кэширует эталонный датасет из центрального сидера слоя данных.
/// </summary>

public class VetClinicFixture
{
    public List<Specialization> Specializations { get; }
    public List<Breed> Breeds { get; }
    public List<Owner> Owners { get; }
    public List<Vet> Vets { get; }
    public List<Service> Services { get; }
    public List<Pet> Pets { get; }
    public List<Appointment> Appointments { get; }
    public VetClinicFixture()
    {
        (Specializations, Breeds, Owners, Vets, Services, Pets, Appointments) = new VetClinicDataSeeder().Deconstruct();
    }
}
