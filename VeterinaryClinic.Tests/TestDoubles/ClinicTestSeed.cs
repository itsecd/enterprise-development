using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;
using VeterinaryClinic.Domain.Data;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Tests.TestDoubles;

/// <summary>
/// Заполняет зарегистрированные репозитории тестовыми данными
/// </summary>
public static class ClinicTestSeed
{
    /// <summary>
    /// Добавляет согласованные тестовые данные в существующие репозитории
    /// </summary>
    /// <param name="provider">Контейнер зарегистрированных зависимостей</param>
    public static async Task SeedClinicTestDataAsync(IServiceProvider provider)
    {
        var breeds = provider.GetRequiredService<IRepository<Breed>>();
        foreach (var x in VeterinaryClinicTestData.Breeds)
        {
            await breeds.CreateAsync(new Breed { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType });
        }

        var specializations = provider.GetRequiredService<IRepository<Specialization>>();
        foreach (var x in VeterinaryClinicTestData.Specializations)
        {
            await specializations.CreateAsync(new Specialization { Id = x.Id, Name = x.Name, AnimalType = x.AnimalType });
        }

        var owners = provider.GetRequiredService<IRepository<Owner>>();
        foreach (var x in VeterinaryClinicTestData.Owners)
        {
            await owners.CreateAsync(new Owner
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Address = x.Address
            });
        }

        var veterinarians = provider.GetRequiredService<IRepository<Veterinarian>>();
        foreach (var x in VeterinaryClinicTestData.Veterinarians)
        {
            var specialization = await RequireAsync(specializations, x.Specialization.Id, "Specialization");
            await veterinarians.CreateAsync(new Veterinarian
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                PassportNumber = x.PassportNumber,
                BirthDate = x.BirthDate,
                ExperienceYears = x.ExperienceYears,
                Specialization = specialization
            });
        }

        var pets = provider.GetRequiredService<IRepository<Pet>>();
        foreach (var x in VeterinaryClinicTestData.Pets)
        {
            var breed = await RequireAsync(breeds, x.Breed.Id, "Breed");
            var owner = x.Owner is null ? null : await owners.GetAsync(x.Owner.Id);
            var pet = new Pet
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Weight = x.Weight,
                Breed = breed,
            };
            pet.ChangeOwner(owner);
            await pets.CreateAsync(pet);
        }

        var appointments = provider.GetRequiredService<IRepository<Appointment>>();
        foreach (var x in VeterinaryClinicTestData.Appointments)
        {
            var pet = await RequireAsync(pets, x.Pet.Id, "Pet");
            var veterinarian = await RequireAsync(veterinarians, x.Veterinarian.Id, "Veterinarian");
            await appointments.CreateAsync(new Appointment
            {
                Id = x.Id,
                AppointmentDate = x.AppointmentDate,
                RoomNumber = x.RoomNumber,
                IsRepeated = x.IsRepeated,
                Pet = pet,
                Veterinarian = veterinarian
            });
        }
    }

    private static async Task<T> RequireAsync<T>(IRepository<T> repository, int id, string entityName) where T : class, IEntity
    {
        return await repository.GetAsync(id)
            ?? throw new InvalidOperationException($"В тестовых данных отсутствует {entityName} с идентификатором {id}");
    }
}
