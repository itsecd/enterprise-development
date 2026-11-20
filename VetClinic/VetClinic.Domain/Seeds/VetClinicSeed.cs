using VetClinic.Domain.Models;
using VetClinic.Shared;

namespace VetClinic.Domain.Seeds;

/// <summary>
/// Начальные данные ветеринарной клиники, совпадающие с данными unit тестов первой лабораторной
/// </summary>
public class VetClinicSeed
{
    /// <summary>
    /// Специализации врачей
    /// </summary>
    public List<Specialization> Specializations { get; } =
    [
        new() { Id = 1, Name = "Хирург (собаки)", FocusSpecies = AnimalSpecies.Dog },
        new() { Id = 2, Name = "Терапевт (кошки)", FocusSpecies = AnimalSpecies.Cat },
        new() { Id = 3, Name = "Орнитолог", FocusSpecies = AnimalSpecies.Bird },
        new() { Id = 4, Name = "Экзотика (кролики)", FocusSpecies = AnimalSpecies.Rabbit },
        new() { Id = 5, Name = "Терапевт (общий)", FocusSpecies = null },
        new() { Id = 6, Name = "Кардиолог (собаки)", FocusSpecies = AnimalSpecies.Dog },
        new() { Id = 7, Name = "Дерматолог (кошки)", FocusSpecies = AnimalSpecies.Cat },
        new() { Id = 8, Name = "Экзотика (хомяки)", FocusSpecies = AnimalSpecies.Hamster },
        new() { Id = 9, Name = "Хирург (общий)", FocusSpecies = null },
        new() { Id = 10, Name = "Невролог (собаки)", FocusSpecies = AnimalSpecies.Dog },
    ];

    /// <summary>
    /// Породы животных
    /// </summary>
    public List<Breed> Breeds { get; } =
    [
        new() { Id = 1, Name = "Лабрадор", Species = AnimalSpecies.Dog },
        new() { Id = 2, Name = "Пудель", Species = AnimalSpecies.Dog },
        new() { Id = 3, Name = "Хаски", Species = AnimalSpecies.Dog },
        new() { Id = 4, Name = "Персидская", Species = AnimalSpecies.Cat },
        new() { Id = 5, Name = "Британская", Species = AnimalSpecies.Cat },
        new() { Id = 6, Name = "Волнистый попугай", Species = AnimalSpecies.Bird },
        new() { Id = 7, Name = "Канарейка", Species = AnimalSpecies.Bird },
        new() { Id = 8, Name = "Рекс", Species = AnimalSpecies.Rabbit },
        new() { Id = 9, Name = "Карликовый", Species = AnimalSpecies.Rabbit },
        new() { Id = 10, Name = "Сирийский", Species = AnimalSpecies.Hamster },
        new() { Id = 11, Name = "Такса", Species = AnimalSpecies.Dog },
    ];

    /// <summary>
    /// Владельцы питомцев
    /// </summary>
    public List<Owner> Owners { get; }

    /// <summary>
    /// Ветеринарные врачи
    /// </summary>
    public List<Vet> Vets { get; }

    /// <summary>
    /// Питомцы
    /// </summary>
    public List<Pet> Pets { get; }

    /// <summary>
    /// Записи на прием
    /// </summary>
    public List<Appointment> Appointments { get; }

    public VetClinicSeed()
    {
        Owners =
        [
            new Owner
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Address = "ул. Ленина, 1",
                Phone = "+79001110001"
            },
            new Owner
            {
                Id = 2,
                FullName = "Петров Петр Петрович",
                Address = "пр. Мира, 5",
                Phone = "+79001110002"
            },
            new Owner
            {
                Id = 3,
                FullName = "Сидорова Анна Викторовна",
                Address = "ул. Садовая, 12",
                Phone = "+79001110003"
            },
            new Owner
            {
                Id = 4,
                FullName = "Королев Дмитрий Сергеевич",
                Address = "ул. Заречная, 3",
                Phone = "+79001110004"
            },
            new Owner
            {
                Id = 5,
                FullName = "Никитина Ольга Юрьевна",
                Address = "пер. Тихий, 7",
                Phone = "+79001110005"
            },
            new Owner
            {
                Id = 6,
                FullName = "Федоров Алексей Николаевич",
                Address = "ул. Победы, 44",
                Phone = "+79001110006"
            },
            new Owner
            {
                Id = 7,
                FullName = "Морозова Елена Павловна",
                Address = "ул. Центральная, 2",
                Phone = "+79001110007"
            },
            new Owner
            {
                Id = 8,
                FullName = "Зайцев Михаил Олегович",
                Address = "ул. Северная, 9",
                Phone = "+79001110008"
            },
            new Owner
            {
                Id = 9,
                FullName = "Соколова Марина Ивановна",
                Address = "ул. Восточная, 18",
                Phone = "+79001110009"
            },
            new Owner
            {
                Id = 10,
                FullName = "Волков Сергей Андреевич",
                Address = "ул. Лесная, 31",
                Phone = "+79001110010"
            },
        ];

        Vets =
        [
            new Vet
            {
                Id = 1,
                PassportNumber = "4501 000001",
                FullName = "Смирнов Антон Валерьевич",
                BirthYear = 1980,
                Specialization = Specializations[0],
                WorkExperienceYears = 15
            },
            new Vet
            {
                Id = 2,
                PassportNumber = "4501 000002",
                FullName = "Кузнецов Игорь Петрович",
                BirthYear = 1975,
                Specialization = Specializations[1],
                WorkExperienceYears = 22
            },
            new Vet
            {
                Id = 3,
                PassportNumber = "4501 000003",
                FullName = "Попова Наталья Сергеевна",
                BirthYear = 1988,
                Specialization = Specializations[2],
                WorkExperienceYears = 8
            },
            new Vet
            {
                Id = 4,
                PassportNumber = "4501 000004",
                FullName = "Новиков Роман Михайлович",
                BirthYear = 1983,
                Specialization = Specializations[3],
                WorkExperienceYears = 12
            },
            new Vet
            {
                Id = 5,
                PassportNumber = "4501 000005",
                FullName = "Лебедев Павел Денисович",
                BirthYear = 1990,
                Specialization = Specializations[4],
                WorkExperienceYears = 6
            },
            new Vet
            {
                Id = 6,
                PassportNumber = "4501 000006",
                FullName = "Королева Светлана Алексеевна",
                BirthYear = 1978,
                Specialization = Specializations[5],
                WorkExperienceYears = 18
            },
            new Vet
            {
                Id = 7,
                PassportNumber = "4501 000007",
                FullName = "Михайлов Денис Юрьевич",
                BirthYear = 1985,
                Specialization = Specializations[6],
                WorkExperienceYears = 10
            },
            new Vet
            {
                Id = 8,
                PassportNumber = "4501 000008",
                FullName = "Горбунов Кирилл Олегович",
                BirthYear = 1992,
                Specialization = Specializations[7],
                WorkExperienceYears = 4
            },
            new Vet
            {
                Id = 9,
                PassportNumber = "4501 000009",
                FullName = "Титова Ирина Владимировна",
                BirthYear = 1981,
                Specialization = Specializations[8],
                WorkExperienceYears = 14
            },
            new Vet
            {
                Id = 10,
                PassportNumber = "4501 000010",
                FullName = "Сорокин Владимир Федорович",
                BirthYear = 1970,
                Specialization = Specializations[9],
                WorkExperienceYears = 28
            },
        ];

        Pets =
        [
            new Pet
            {
                Id = 1,
                Nickname = "Барс",
                Species = AnimalSpecies.Cat,
                Breed = Breeds[3],
                BirthDate = new DateTime(2019, 3, 10),
                Weight = 4.5,
                Owner = Owners[0]
            },
            new Pet
            {
                Id = 2,
                Nickname = "Мурка",
                Species = AnimalSpecies.Cat,
                Breed = Breeds[4],
                BirthDate = new DateTime(2020, 7, 22),
                Weight = 3.8,
                Owner = Owners[1]
            },
            new Pet
            {
                Id = 3,
                Nickname = "Рекс",
                Species = AnimalSpecies.Dog,
                Breed = Breeds[0],
                BirthDate = new DateTime(2018, 5, 15),
                Weight = 28.0,
                Owner = Owners[2]
            },
            new Pet
            {
                Id = 4,
                Nickname = "Буян",
                Species = AnimalSpecies.Dog,
                Breed = Breeds[2],
                BirthDate = new DateTime(2017, 9, 3),
                Weight = 23.5,
                Owner = Owners[3]
            },
            new Pet
            {
                Id = 5,
                Nickname = "Снежок",
                Species = AnimalSpecies.Rabbit,
                Breed = Breeds[7],
                BirthDate = new DateTime(2021, 1, 18),
                Weight = 1.8,
                Owner = Owners[4]
            },
            new Pet
            {
                Id = 6,
                Nickname = "Кеша",
                Species = AnimalSpecies.Bird,
                Breed = Breeds[5],
                BirthDate = new DateTime(2020, 11, 5),
                Weight = 0.04,
                Owner = Owners[5]
            },
            new Pet
            {
                Id = 7,
                Nickname = "Белка",
                Species = AnimalSpecies.Hamster,
                Breed = Breeds[9],
                BirthDate = new DateTime(2022, 6, 1),
                Weight = 0.15,
                Owner = Owners[6]
            },
            new Pet
            {
                Id = 8,
                Nickname = "Пушок",
                Species = AnimalSpecies.Rabbit,
                Breed = Breeds[8],
                BirthDate = new DateTime(2021, 4, 14),
                Weight = 1.2,
                Owner = Owners[7]
            },
            new Pet
            {
                Id = 9,
                Nickname = "Тоби",
                Species = AnimalSpecies.Dog,
                Breed = Breeds[1],
                BirthDate = new DateTime(2019, 8, 28),
                Weight = 7.0,
                Owner = Owners[0]
            },
            new Pet
            {
                Id = 10,
                Nickname = "Лаки",
                Species = AnimalSpecies.Dog,
                Breed = Breeds[0],
                BirthDate = new DateTime(2020, 2, 11),
                Weight = 25.5,
                Owner = Owners[2]
            },
            new Pet
            {
                Id = 11,
                Nickname = "Жучка",
                Species = AnimalSpecies.Dog,
                Breed = Breeds[10],
                BirthDate = new DateTime(2018, 12, 30),
                Weight = 9.5,
                Owner = Owners[1]
            },
        ];

        Owners[0].Pets.AddRange([Pets[0], Pets[8]]);
        Owners[1].Pets.AddRange([Pets[1], Pets[10]]);
        Owners[2].Pets.AddRange([Pets[2], Pets[9]]);
        Owners[3].Pets.Add(Pets[3]);
        Owners[4].Pets.Add(Pets[4]);
        Owners[5].Pets.Add(Pets[5]);
        Owners[6].Pets.Add(Pets[6]);
        Owners[7].Pets.Add(Pets[7]);

        Appointments =
        [
            new Appointment
            {
                Id = 1,
                DateTime = new DateTime(2024, 6, 3, 10, 0, 0),
                RoomNumber = "101",
                IsRepeat = true,
                Pet = Pets[2],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 2,
                DateTime = new DateTime(2024, 6, 5, 11, 30, 0),
                RoomNumber = "101",
                IsRepeat = true,
                Pet = Pets[3],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 3,
                DateTime = new DateTime(2024, 6, 10, 9, 0, 0),
                RoomNumber = "101",
                IsRepeat = true,
                Pet = Pets[9],
                Vet = Vets[5]
            },
            new Appointment
            {
                Id = 4,
                DateTime = new DateTime(2024, 6, 12, 14, 0, 0),
                RoomNumber = "205",
                IsRepeat = false,
                Pet = Pets[0],
                Vet = Vets[1]
            },
            new Appointment
            {
                Id = 5,
                DateTime = new DateTime(2024, 6, 18, 16, 30, 0),
                RoomNumber = "205",
                IsRepeat = true,
                Pet = Pets[1],
                Vet = Vets[6]
            },
            new Appointment
            {
                Id = 6,
                DateTime = new DateTime(2024, 6, 20, 10, 0, 0),
                RoomNumber = "303",
                IsRepeat = false,
                Pet = Pets[5],
                Vet = Vets[2]
            },
            new Appointment
            {
                Id = 7,
                DateTime = new DateTime(2024, 6, 25, 13, 0, 0),
                RoomNumber = "303",
                IsRepeat = true,
                Pet = Pets[4],
                Vet = Vets[3]
            },
            new Appointment
            {
                Id = 8,
                DateTime = new DateTime(2024, 5, 14, 9, 0, 0),
                RoomNumber = "101",
                IsRepeat = true,
                Pet = Pets[2],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 9,
                DateTime = new DateTime(2024, 5, 20, 11, 0, 0),
                RoomNumber = "101",
                IsRepeat = false,
                Pet = Pets[3],
                Vet = Vets[5]
            },
            new Appointment
            {
                Id = 10,
                DateTime = new DateTime(2024, 4, 5, 10, 0, 0),
                RoomNumber = "205",
                IsRepeat = false,
                Pet = Pets[2],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 11,
                DateTime = new DateTime(2024, 4, 7, 15, 0, 0),
                RoomNumber = "205",
                IsRepeat = false,
                Pet = Pets[9],
                Vet = Vets[5]
            },
            new Appointment
            {
                Id = 12,
                DateTime = new DateTime(2024, 3, 1, 9, 30, 0),
                RoomNumber = "101",
                IsRepeat = false,
                Pet = Pets[8],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 13,
                DateTime = new DateTime(2024, 6, 7, 14, 0, 0),
                RoomNumber = "101",
                IsRepeat = false,
                Pet = Pets[10],
                Vet = Vets[0]
            },
            new Appointment
            {
                Id = 14,
                DateTime = new DateTime(2024, 6, 22, 10, 0, 0),
                RoomNumber = "404",
                IsRepeat = true,
                Pet = Pets[6],
                Vet = Vets[7]
            },
            new Appointment
            {
                Id = 15,
                DateTime = new DateTime(2024, 6, 28, 11, 0, 0),
                RoomNumber = "404",
                IsRepeat = false,
                Pet = Pets[7],
                Vet = Vets[3]
            },
        ];
    }
}
