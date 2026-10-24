using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Tests.Fixtures;


namespace VetClinic.Tests;

/// <summary>
/// Класс, имитирующий linq запросы к домену
/// </summary>
public class ClinicDomainTests(VetClinicFixture fixture, ITestOutputHelper output) : IClassFixture<VetClinicFixture>
{
    /// <summary>
    /// Функция, которая распечатывает всех ветеринаров
    /// занимающихся кошками
    /// </summary>
    [Fact]
    public void FindAllVetsForSpecificAnimalType()
    {
        var specificAnimalType = Species.Cat;
        var expectedNumber = 2;
        var vets = fixture.Vets
            .Where(v => v.Specialization.TargetSpecies == specificAnimalType)
            .ToList();

        foreach (var v in vets)
        {
            output.WriteLine($"ID: {v.Id} | Врач: {v.FullName} | Стаж: {v.ExpirienceYears} лет | Специализация: {v.Specialization.Title}");
        }

        Assert.Equal(expectedNumber, vets.Count);
        Assert.Contains(fixture.Vets[0], vets);
        Assert.Contains(fixture.Vets[1], vets);
    }

    /// <summary>
    /// Функция, которая находит всех животных, которых принял 
    /// определённый врач, и сортирует в алфавитном порядке
    /// </summary>
    [Fact]
    public void FindAllPetsForSpecificVetsAndSortByName()
    {
        var specificVet = fixture.Vets[0];
        var expectedNumber = 2;
        var matchedPets = fixture.Appointments
            .Where(a => a.Vet == specificVet)
            .Select(a => a.Pet)
            .OrderBy(p => p.Name)
            .ToList();

        foreach (var p in matchedPets)
        {
            output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.Weight} кг");
            output.WriteLine($"  Дата рождения: {p.BirthDate}");
            output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            output.WriteLine("");
        }

        Assert.Equal(expectedNumber, matchedPets.Count);
        Assert.Contains(fixture.Pets[0], matchedPets);
        Assert.Contains(fixture.Pets[1], matchedPets);
    }

    /// <summary>
    /// Функция, которая находит количество повторных обращений 
    /// владельцев животных с выбранной породой
    /// </summary>
    [Fact]
    public void FindNumberOfSecondaryVisitForSpecificAnimalBreed()
    {
        var specificAnimalBreed = fixture.Breeds[10];

        List<Pet> expectedAnimals =
        [
            fixture.Pets[10] // в данном конкретном случае ожидаемый список состоит из одного значения
        ];


        var matchedPets = fixture.Appointments
            .Where(a => a.Pet.Breed == specificAnimalBreed && a.RepeatVisitIndicator == RepeatVisitIndicator.Yes)
            .Select(a => a.Pet)
            .OrderBy(p => p.Name)
            .ToList();

        foreach (var p in matchedPets)
        {
            output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.Weight} кг");
            output.WriteLine($"  Дата рождения: {p.BirthDate}");
            output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            output.WriteLine("");
        }

        output.WriteLine($" Кол-во питомцев: {matchedPets.Count}");
        Assert.NotEmpty(matchedPets);
        Assert.Single(matchedPets);
        Assert.Equal(expectedAnimals, matchedPets);
    }

    /// <summary>
    /// Функция, которая ищет владельцев сразу нескольких питомцев
    /// </summary>
    [Fact]
    public void FindOwnersWithMultiplePets()
    {
        List<Owner> expectedOwners =
        [
            fixture.Owners[0],
            fixture.Owners[10],
            fixture.Owners[3],
            fixture.Owners[9],
            fixture.Owners[1],
            fixture.Owners[6]
        ];

        var owners = fixture.Owners
            .Where(owner => fixture.Pets.Count(pet => pet.Owner.Id == owner.Id) > 1)
            .OrderBy(owner => owner.FullName);

        foreach (var o in owners)
        {
            output.WriteLine($"ID: {o.Id} | Владелец: {o.FullName} | Телефон: {o.Phone} | Адрес: {o.Adress}");
        }

        Assert.NotEmpty(owners);
        Assert.Equal(expectedOwners, owners);
    }

    /// <summary>
    /// Функция, которая находит приёмы, проходившие в определённом кабинете в сентябре 2026
    /// </summary>
    [Fact]
    public void FindAppointmentsInSpecificRoomsInSpecificMonth()
    {
        var specificRoom = 102;
        var specificYear = 2026;
        var specificMonth = 9;

        var appointments = fixture.Appointments
            .Where(appointment =>
                appointment.RoomNumber == specificRoom &&
                appointment.DateTime.Year == specificYear &&
                appointment.DateTime.Month == specificMonth);

        foreach (var a in appointments)
        {
            output.WriteLine($"Прием ID: {a.Id} | Дата: {a.DateTime} | Кабинет: {a.RoomNumber}");
            output.WriteLine($"Повторный визит: {a.RepeatVisitIndicator} | Диагноз: {a.Diagnosis}");
            output.WriteLine("");
        }

        Assert.NotEmpty(appointments);
        Assert.Contains(fixture.Appointments[6], appointments);
        Assert.Contains(fixture.Appointments[7], appointments);
    }
}