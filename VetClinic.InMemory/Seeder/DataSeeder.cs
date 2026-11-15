using VetClinic.Domain.Entities;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.InMemory.Seeder;

/// <summary>
/// Изолированный сидер тестовых данных.
/// Отвечает исключительно за хардкод эталонного датасета клиники.
/// </summary>


public class VetClinicDataSeeder
{
    /// <summary>
    /// Список пород животных
    /// </summary>
    public List<Breed> Breeds { get; init; }

    /// <summary>
    /// Список владельцев
    /// </summary>
    public List<Owner> Owners { get; init; }

    /// <summary>
    /// Список специализацией
    /// </summary>
    public List<Specialization> Specializations { get; init; }

    /// <summary>
    /// Список врачей
    /// </summary>
    public List<Vet> Vets { get; init; }

    /// <summary>
    /// Список услуг
    /// </summary>
    public List<Service> Services { get; init; }

    /// <summary>
    /// Список животных
    /// </summary>
    public List<Pet> Pets { get; init; }

    /// <summary>
    /// Список приёмов
    /// </summary>
    public List<Appointment> Appointments { get; init; }


    public (List<Specialization>, List<Breed>, List<Owner>, List<Vet>, List<Service>, List<Pet>, List<Appointment>) Deconstruct()
    => (Specializations, Breeds, Owners, Vets, Services, Pets, Appointments);


    /// <summary>
    /// Функция, создающая списки объектов
    /// </summary>

    public VetClinicDataSeeder()
    {
        // Объявляем независимые справочники
        Breeds = GetBreedsList();
        Owners = GetOwnersList();
        Specializations = GetSpecializationsList();

        // Затем объявляем врачей и услуги (они зависят только от enum Species)
        Vets = GetVetsList();
        Services = GetServicesList();

        // Питомцы собираются из Владельцев и Пород
        Pets = GetPetsList();

        // Приемы связывают воедино Питомцев, Врачей и Услуги
        Appointments = GetAppointmentsList();
    }

    /// <summary>
    /// Функция, создающая список пород животных
    /// </summary>
    private static List<Breed> GetBreedsList() =>
    [
        new Breed
        {
            Id = 0,
            Title = "Мейн-кун",
            Type = Species.Cat
        },
        new Breed
        {
            Id = 1,
            Title = "Сиамская",
            Type = Species.Cat
        },
        new Breed
        {
            Id = 2,
            Title = "Британская короткошерстная",
            Type = Species.Cat
        },
        new Breed
        {
            Id = 3,
            Title = "Немецкая овчарка",
            Type = Species.Dog
        },
        new Breed
        {
            Id = 4,
            Title = "Лабрадор-ретривер",
            Type = Species.Dog
        },
        new Breed
        {
            Id = 5,
            Title = "Джек-рассел-терьер",
            Type = Species.Dog
        },
        new Breed
        {
            Id = 6,
            Title = "Сизый голубь",
            Type = Species.Pigeon
        },
        new Breed
        {
            Id = 7,
            Title = "Почтовый белый",
            Type = Species.Pigeon
        },
        new Breed
        {
            Id = 8,
            Title = "Якобин",
            Type = Species.Pigeon
        },
        new Breed
        {
            Id = 9,
            Title = "Ангорский кролик",
            Type = Species.Rabbit
        },
        new Breed
        {
            Id = 10,
            Title = "Голландский вислоухий",
            Type = Species.Rabbit
        },
        new Breed
        {
            Id = 11,
            Title = "Калифорнийский кролик",
            Type = Species.Rabbit
        },
        new Breed
        {
            Id = 12,
            Title = "Орловский рысак",
            Type = Species.Horse
        },
        new Breed
        {
            Id = 13,
            Title = "Арабская чистокровная",
            Type = Species.Horse
        },
        new Breed
        {
            Id = 14,
            Title = "Фризская",
            Type = Species.Horse
        }
    ];

    /// <summary>
    /// Функция, создающая список владельцев
    /// </summary>
    private List<Owner> GetOwnersList() =>
    [
        new Owner
        {
            Id = 0,
            FullName = "Иванов Иван Иванович",
            Phone = "+7 (999) 111-22-33",
            Adress = "ул. Ленина, д. 10"
        },
        new Owner
        {
            Id = 1,
            FullName = "Петрова Анна Сергеевна",
            Phone = "+7 (999) 444-55-66",
            Adress = "ул. Садовая, д. 24"
        },
        new Owner
        {
            Id = 2,
            FullName = "Сидоров Пётр Петрович",
            Phone = "+7 (999) 777-88-99",
            Adress = "пр. Мира, д. 5"
        },
        new Owner
        {
            Id = 3,
            FullName = "Кузнецова Ольга Владимировна",
            Phone = "+7 (999) 222-33-44",
            Adress = "ул. Гагарина, д. 12"
        },
        new Owner
        {
            Id = 4,
            FullName = "Смирнов Дмитрий Александрович",
            Phone = "+7 (999) 555-66-77",
            Adress = "ул. Чехова, д. 8"
        },
        new Owner
        {
            Id = 5,
            FullName = "Васильева Елена Николаевна",
            Phone = "+7 (999) 888-99-00",
            Adress = "ул. Пушкина, д. 15"
        },
        new Owner
        {
            Id = 6,
            FullName = "Попов Сергей Игоревич",
            Phone = "+7 (999) 333-44-55",
            Adress = "ул. Новая, д. 3"
        },
        new Owner
        {
            Id = 7,
            FullName = "Михайлова Татьяна Юрьевна",
            Phone = "+7 (999) 666-77-88",
            Adress = "ул. Полевая, д. 21"
        },
        new Owner
        {
            Id = 8,
            FullName = "Федоров Андрей Олегович",
            Phone = "+7 (999) 999-00-11",
            Adress = "ул. Лесная, д. 4"
        },
        new Owner
        {
            Id = 9,
            FullName = "Морозова Наталья Сергеевна",
            Phone = "+7 (999) 123-98-76",
            Adress = "ул. Набережная, д. 30"
        },
        new Owner
        {
            Id = 10,
            FullName = "Иванов Сергей Викторович",
            Phone = "+7 (999) 477-55-66",
            Adress = "ул. Первомайская, д. 23"
        }
    ];

    /// <summary>
    /// Функция, создающая список специализаций
    /// </summary>
    private List<Specialization> GetSpecializationsList() =>
    [
        new Specialization
        {
            Id = 0,
            Title = "Терапевт-фелинолог",
            TargetSpecies = Species.Cat
        },
        new Specialization
        {
            Id = 1,
            Title = "Хирург-онколог кошачьих отделений",
            TargetSpecies = Species.Cat
        },
        new Specialization
        {
            Id = 2,
            Title = "Кинолог-терапевт",
            TargetSpecies = Species.Dog
        },
        new Specialization
        {
            Id = 3,
            Title = "Травматолог-ортопед собак крупных пород",
            TargetSpecies = Species.Dog
        },
        new Specialization
        {
            Id = 4,
            Title = "Ветеринар-орнитолог",
            TargetSpecies = Species.Pigeon
        },
        new Specialization
        {
            Id = 5,
            Title = "Хирург-авиарлог",
            TargetSpecies = Species.Pigeon
        },
        new Specialization
        {
            Id = 6,
            Title = "Ветеринар-ратолог",
            TargetSpecies = Species.Rabbit
        },
        new Specialization
        {
            Id = 7,
            Title = "Стоматолог-родентолог",
            TargetSpecies = Species.Rabbit
        },
        new Specialization
        {
            Id = 8,
            Title = "Ветеринар-ипполог",
            TargetSpecies = Species.Horse
        },
        new Specialization
        {
            Id = 9,
            Title = "Ортопед-подолог",
            TargetSpecies = Species.Horse
        }
    ];

    /// <summary>
    /// Функция, создающая список ветеринаров
    /// </summary>
    private List<Vet> GetVetsList() =>
    [
        new Vet
        {
            Id = 0,
            FullName = "Др. Алексей Айболит",
            PassportNumber = "4511 111111",
            BirthDate = new DateOnly(1980, 5, 12),
            ExpirienceYears = 20,
            SpecializationId = 0,
            Specialization = Specializations[0]
        },
        new Vet
        {
            Id = 1,
            FullName = "Др. Степан Котов",
            PassportNumber = "4511 222222",
            BirthDate = new DateOnly(1990, 8, 14),
            ExpirienceYears = 6,
            SpecializationId = 1,
            Specialization = Specializations[1]
        },
        new Vet
        {
            Id = 2,
            FullName = "Др. Елена Быкова",
            PassportNumber = "4512 333333",
            BirthDate = new DateOnly(1985, 11, 23),
            ExpirienceYears = 12,
            SpecializationId = 2,
            Specialization = Specializations[2]
        },
        new Vet
        {
            Id = 3,
            FullName = "Др. Михаил Шариков",
            PassportNumber = "4512 444444",
            BirthDate = new DateOnly(1993, 1, 19),
            ExpirienceYears = 4,
            SpecializationId = 3,
            Specialization = Specializations[3]
        },
        new Vet
        {
            Id = 4,
            FullName = "Др. Анна Смит",
            PassportNumber = "4514 777777",
            BirthDate = new DateOnly(1992, 7, 4),
            ExpirienceYears = 6,
            SpecializationId = 4,
            Specialization = Specializations[4]
        },
        new Vet
        {
            Id = 5,
            FullName = "Др. Григорий Птицын",
            PassportNumber = "4514 888888",
            BirthDate = new DateOnly(1983, 4, 27),
            ExpirienceYears = 15,
            SpecializationId = 5,
            Specialization = Specializations[5]
        },
        new Vet
        {
            Id = 6,
            FullName = "Др. Кэррот Роджерс",
            PassportNumber = "4515 999999",
            BirthDate = new DateOnly(1988, 9, 30),
            ExpirienceYears = 10,
            SpecializationId = 6,
            Specialization = Specializations[6]
        },
        new Vet
        {
            Id = 7,
            FullName = "Др. Ольга Ушастая",
            PassportNumber = "4515 000000",
            BirthDate = new DateOnly(1995, 2, 10),
            ExpirienceYears = 3,
            SpecializationId = 7,
            Specialization = Specializations[7]
        },
        new Vet
        {
            Id = 8,
            FullName = "Др. Иван Дулиттл",
            PassportNumber = "4513 555555",
            BirthDate = new DateOnly(1978, 3, 15),
            ExpirienceYears = 25,
            SpecializationId = 8,
            Specialization = Specializations[8]
        },
        new Vet
        {
            Id = 9,
            FullName = "Др. Ксения Конева",
            PassportNumber = "4513 666666",
            BirthDate = new DateOnly(1989, 12, 5),
            ExpirienceYears = 9,
            SpecializationId = 9,
            Specialization = Specializations[9]
        }
    ];

    /// <summary>
    /// Функция, создающая список услуг
    /// </summary>
    private List<Service> GetServicesList() =>
    [
        new Service
        {
            Id = 0,
            Title = "Первичный осмотр и термометрия кошек",
            Price = 1000m,
            SpecializationId = 1,
            Specialization = Specializations[0]
        },
        new Service
        {
            Id = 1,
            Title = "Комплексная вакцинация кошачьих",
            Price = 1500m,
            SpecializationId = 1,
            Specialization = Specializations[0]
        },
        new Service
        {
            Id = 2,
            Title = "Гигиеническая чистка зубов кошкам",
            Price = 1800m,
            SpecializationId = 1,
            Specialization = Specializations[0]
        },
        new Service
        {
            Id = 3,
            Title = "Клинический осмотр и консультация собак",
            Price = 1200m,
            SpecializationId = 3,
            Specialization = Specializations[2]
        },
        new Service
        {
            Id = 4,
            Title = "Комплексная вакцинация собак (с бешенством)",
            Price = 1700m,
            SpecializationId = 3,
            Specialization = Specializations[2]
        },
        new Service
        {
            Id = 5,
            Title = "УЗИ органов брюшной полости собак",
            Price = 2500m,
            SpecializationId = 3,
            Specialization = Specializations[2]
        },
        new Service
        {
            Id = 6,
            Title = "Осмотр птицы и микроскопия мазка",
            Price = 900m,
            SpecializationId = 5,
            Specialization = Specializations[4]
        },
        new Service
        {
            Id = 7,
            Title = "Коррекция (подрезка) переросшего клюва",
            Price = 800m,
            SpecializationId = 5,
            Specialization = Specializations[4]
        },
        new Service
        {
            Id = 8,
            Title = "Лабораторный анализ оперения на паразитов",
            Price = 1100m,
            SpecializationId = 5,
            Specialization = Specializations[4]
        },
        new Service
        {
            Id = 9,
            Title = "Прием ратолога и визуальная диагностика",
            Price = 950m,
            SpecializationId = 7,
            Specialization = Specializations[6]
        },
        new Service
        {
            Id = 10,
            Title = "Коррекция и подпилка резцов у грызунов",
            Price = 1300m,
            SpecializationId = 7,
            Specialization = Specializations[6]
        },
        new Service
        {
            Id = 11,
            Title = "Вакцинация кроликов от ВГБК и миксоматоза",
            Price = 1100m,
            SpecializationId = 7,
            Specialization = Specializations[6]
        },
        new Service
        {
            Id = 12,
            Title = "Выездной осмотр и аускультация лошади",
            Price = 4000m,
            SpecializationId = 9,
            Specialization = Specializations[8]
        },
        new Service
        {
            Id = 13,
            Title = "Расчистка и ортопедическая обработка копыт",
            Price = 5000m,
            SpecializationId = 9,
            Specialization = Specializations[8]
        },
        new Service
        {
            Id = 14,
            Title = "Рентгенография конечностей лошади",
            Price = 6500m,
            SpecializationId = 9,
            Specialization = Specializations[8]
        }
    ];

    /// <summary>
    /// Функция, создающая список питомцев
    /// </summary>
    private List<Pet> GetPetsList() =>
    [
        new Pet
        {
            Id = 0,
            Name = "Барсик",
            Type = Species.Cat,
            BirthDate = new DateOnly(2022, 4, 10),
            Weight = 6.5m,
            BreedId = Breeds[0].Id,
            Breed = Breeds[0],
            OwnerId = Owners[0].Id,
            Owner = Owners[0]
        },
        new Pet
        {
            Id = 1,
            Name = "Мурка",
            Type = Species.Cat,
            BirthDate = new DateOnly(2023, 6, 15),
            Weight = 4,
            BreedId = Breeds[1].Id,
            Breed = Breeds[1],
            OwnerId = Owners[1].Id,
            Owner = Owners[1]
        },
        new Pet
        {
            Id = 2,
            Name = "Симба",
            Type = Species.Cat,
            BirthDate = new DateOnly(2025, 1, 20),
            Weight = 4.8m,
            BreedId = Breeds[2].Id,
            Breed = Breeds[2],
            OwnerId = Owners[2].Id,
            Owner = Owners[2]
        },
        new Pet
        {
            Id = 3,
            Name = "Шарик",
            Type = Species.Dog,
            BirthDate = new DateOnly(2020, 11, 5),
            Weight = 30.5m,
            BreedId = Breeds[3].Id,
            Breed = Breeds[3],
            OwnerId = Owners[0].Id,
            Owner = Owners[0]
        },
        new Pet
        {
            Id = 4,
            Name = "Рекс",
            Type = Species.Dog,
            BirthDate = new DateOnly(2021, 8, 25),
            Weight = 28.5m,
            BreedId = Breeds[4].Id,
            Breed = Breeds[4],
            OwnerId = Owners[3].Id,
            Owner = Owners[3]
        },
        new Pet
        {
            Id = 5,
            Name = "Граф",
            Type = Species.Dog,
            BirthDate = new DateOnly(2024, 3, 14),
            Weight = 8.7m,
            BreedId = Breeds[5].Id,
            Breed = Breeds[5],
            OwnerId = Owners[4].Id,
            Owner = Owners[4]
        },
        new Pet
        {
            Id = 6,
            Name = "Кеша",
            Type = Species.Pigeon,
            BirthDate = new DateOnly(2025, 5, 12),
            Weight = 0.65m,
            BreedId = Breeds[6].Id,
            Breed = Breeds[6],
            OwnerId = Owners[5].Id,
            Owner = Owners[5]
        },
        new Pet
        {
            Id = 7,
            Name = "Ворчун",
            Type = Species.Pigeon,
            BirthDate = new DateOnly(2024, 7, 19),
            Weight = 0.8m,
            BreedId = Breeds[7].Id,
            Breed = Breeds[7],
            OwnerId = Owners[6].Id,
            Owner = Owners[6]
        },
        new Pet
        {
            Id = 8,
            Name = "Пират",
            Type = Species.Pigeon,
            BirthDate = new DateOnly(2026, 2, 1),
            Weight = 1.15m,
            BreedId = Breeds[8].Id,
            Breed = Breeds[8],
            OwnerId = Owners[7].Id,
            Owner = Owners[7]
        },
        new Pet
        {
            Id = 9,
            Name = "Пушок",
            Type = Species.Rabbit,
            BirthDate = new DateOnly(2023, 10, 1),
            Weight = 3.7m,
            BreedId = Breeds[9].Id,
            Breed = Breeds[9],
            OwnerId = Owners[1].Id,
            Owner = Owners[1]
        },
        new Pet
        {
            Id = 10,
            Name = "Снежок",
            Type = Species.Rabbit,
            BirthDate = new DateOnly(2024, 12, 11),
            Weight = 2.3m,
            BreedId = Breeds[10].Id,
            Breed = Breeds[10],
            OwnerId = Owners[8].Id,
            Owner = Owners[8]
        },
        new Pet
        {
            Id = 11,
            Name = "Банни",
            Type = Species.Rabbit,
            BirthDate = new DateOnly(2025, 4, 3),
            Weight = 4.5m,
            BreedId = Breeds[11].Id,
            Breed = Breeds[11],
            OwnerId = Owners[9].Id,
            Owner = Owners[9]
        },
        new Pet
        {
            Id = 12,
            Name = "Буцефал",
            Type = Species.Horse,
            BirthDate = new DateOnly(2019, 5, 20),
            Weight = 450,
            BreedId = Breeds[12].Id,
            Breed = Breeds[12],
            OwnerId = Owners[3].Id,
            Owner = Owners[3]
        },
        new Pet
        {
            Id = 13,
            Name = "Искра",
            Type = Species.Horse,
            BirthDate = new DateOnly(2021, 9, 15),
            Weight = 420,
            BreedId = Breeds[13].Id,
            Breed = Breeds[13],
            OwnerId = Owners[6].Id,
            Owner = Owners[6]
        },
        new Pet
        {
            Id = 14,
            Name = "Тайфон",
            Type = Species.Horse,
            BirthDate = new DateOnly(2020, 6, 30),
            Weight = 510,
            BreedId = Breeds[14].Id,
            Breed = Breeds[14],
            OwnerId = Owners[9].Id,
            Owner = Owners[9]
        },
        new Pet
        {
            Id = 15,
            Name = "Мурзик",
            Type = Species.Cat,
            BirthDate = new DateOnly(2020, 4, 10),
            Weight = 6,
            BreedId = Breeds[0].Id,
            Breed = Breeds[0],
            OwnerId = Owners[10].Id,
            Owner = Owners[10]
        },
        new Pet
        {
            Id = 16,
            Name = "Боцик",
            Type = Species.Horse,
            BirthDate = new DateOnly(2023, 9, 15),
            Weight = 375,
            BreedId = Breeds[13].Id,
            Breed = Breeds[13],
            OwnerId = Owners[10].Id,
            Owner = Owners[10]
        }
    ];

    /// <summary>
    /// Функция, создающая список приёмов
    /// </summary>
    private List<Appointment> GetAppointmentsList() =>
    [
        new Appointment
        {
            Id = 0,
            DateTime = new DateTime(2026, 9, 1, 10, 0, 0),
            Diagnosis = "Здоров",
            RoomNumber = 101,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[0].Id,
            Pet = Pets[0],
            VetId = Vets[0].Id,
            Vet = Vets[0],
            ServiceId = Services[0].Id,
            Service = Services[0]
        },
        new Appointment
        {
            Id = 1,
            DateTime = new DateTime(2026, 2, 2, 11, 30, 0),
            Diagnosis = "Здорова",
            RoomNumber = 103,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[1].Id,
            Pet = Pets[1],
            VetId = Vets[1].Id,
            Vet = Vets[1],
            ServiceId = Services[0].Id,
            Service = Services[0]
        },
        new Appointment
        {
            Id = 2,
            DateTime = new DateTime(2026, 9, 8, 10, 30, 0),
            Diagnosis = "Зубной камень удален",
            RoomNumber = 101,
            RepeatVisitIndicator = RepeatVisitIndicator.Yes,
            PetId = Pets[1].Id,
            Pet = Pets[1],
            VetId = Vets[0].Id,
            Vet = Vets[0],
            ServiceId = Services[1].Id,
            Service = Services[1]
        },
        new Appointment
        {
            Id = 3,
            DateTime = new DateTime(2026, 9, 12, 15, 45, 0),
            Diagnosis = "Витамины прописаны",
            RoomNumber = 103,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[2].Id,
            Pet = Pets[2],
            VetId = Vets[1].Id,
            Vet = Vets[1],
            ServiceId = Services[0].Id,
            Service = Services[0]
        },
        new Appointment
        {
            Id = 4,
            DateTime = new DateTime(2026, 6, 3, 14, 0, 0),
            Diagnosis = "Здоров",
            RoomNumber = 102,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[3].Id,
            Pet = Pets[3],
            VetId = Vets[2].Id,
            Vet = Vets[2],
            ServiceId = Services[2].Id,
            Service = Services[2]
        },
        new Appointment
        {
            Id = 5,
            DateTime = new DateTime(2026, 7, 4, 15, 0, 0),
            Diagnosis = "Назначена диета",
            RoomNumber = 106,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[4].Id,
            Pet = Pets[4],
            VetId = Vets[3].Id,
            Vet = Vets[3],
            ServiceId = Services[2].Id,
            Service = Services[2]
        },
        new Appointment
        {
            Id = 6,
            DateTime = new DateTime(2026, 9, 9, 12, 0, 0),
            Diagnosis = "Плановое УЗИ",
            RoomNumber = 102,
            RepeatVisitIndicator = RepeatVisitIndicator.Yes,
            PetId = Pets[3].Id,
            Pet = Pets[3],
            VetId = Vets[2].Id,
            Vet = Vets[2],
            ServiceId = Services[3].Id,
            Service = Services[3]
        },
        new Appointment
        {
            Id = 7,
            DateTime = new DateTime(2026, 9, 13, 16, 0, 0),
            Diagnosis = "Вакцинация выполнена",
            RoomNumber = 102,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[5].Id,
            Pet = Pets[5],
            VetId = Vets[2].Id,
            Vet = Vets[2],
            ServiceId = Services[2].Id,
            Service = Services[2]
        },
        new Appointment
        {
            Id = 8,
            DateTime = new DateTime(2026, 6, 6, 16, 30, 0),
            Diagnosis = "Здоров",
            RoomNumber = 105,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[6].Id,
            Pet = Pets[6],
            VetId = Vets[4].Id,
            Vet = Vets[4],
            ServiceId = Services[4].Id,
            Service = Services[4]
        },
        new Appointment
        {
            Id = 9,
            DateTime = new DateTime(2026, 7, 14, 10, 0, 0),
            Diagnosis = "Клюв в норме",
            RoomNumber = 107,
            RepeatVisitIndicator = RepeatVisitIndicator.Yes,
            PetId = Pets[7].Id,
            Pet = Pets[7],
            VetId = Vets[5].Id,
            Vet = Vets[5],
            ServiceId = Services[4].Id,
            Service = Services[4]
        },
        new Appointment
        {
            Id = 10,
            DateTime = new DateTime(2026, 8, 16, 11, 0, 0),
            Diagnosis = "Осмотр крыла",
            RoomNumber = 105,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[8].Id,
            Pet = Pets[8],
            VetId = Vets[4].Id,
            Vet = Vets[4],
            ServiceId = Services[4].Id,
            Service = Services[4]
        },
        new Appointment
        {
            Id = 11,
            DateTime = new DateTime(2026, 10, 7, 13, 0, 0),
            Diagnosis = "Отрастание резцов",
            RoomNumber = 104,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[9].Id,
            Pet = Pets[9],
            VetId = Vets[6].Id,
            Vet = Vets[6],
            ServiceId = Services[5].Id,
            Service = Services[5]
        },
        new Appointment
        {
            Id = 12,
            DateTime = new DateTime(2026, 10, 15, 14, 0, 0),
            Diagnosis = "Зубы в порядке",
            RoomNumber = 108,
            RepeatVisitIndicator = RepeatVisitIndicator.Yes,
            PetId = Pets[10].Id,
            Pet = Pets[10],
            VetId = Vets[7].Id,
            Vet = Vets[7],
            ServiceId = Services[5].Id,
            Service = Services[5]
        },
        new Appointment
        {
            Id = 13,
            DateTime = new DateTime(2026, 10, 17, 9, 30, 0),
            Diagnosis = "Здоров",
            RoomNumber = 104,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[11].Id,
            Pet = Pets[11],
            VetId = Vets[6].Id,
            Vet = Vets[6],
            ServiceId = Services[5].Id,
            Service = Services[5]
        },
        new Appointment
        {
            Id = 14,
            DateTime = new DateTime(2026, 9, 5, 9, 0, 0),
            Diagnosis = "Профилактика копыт",
            RoomNumber = 109,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[12].Id,
            Pet = Pets[12],
            VetId = Vets[8].Id,
            Vet = Vets[8],
            ServiceId = Services[6].Id,
            Service = Services[6]
        },
        new Appointment
        {
            Id = 15,
            DateTime = new DateTime(2026, 9, 12, 9, 0, 0),
            Diagnosis = "Профилактика копыт",
            RoomNumber = 109,
            RepeatVisitIndicator = RepeatVisitIndicator.Not,
            PetId = Pets[16].Id,
            Pet = Pets[16],
            VetId = Vets[8].Id,
            Vet = Vets[8],
            ServiceId = Services[6].Id,
            Service = Services[6]
        },
        new Appointment
        {
            Id = 16,
            DateTime = new DateTime(2026, 8, 9, 11, 30, 0),
            Diagnosis = "Здоров",
            RoomNumber = 103,
            RepeatVisitIndicator = RepeatVisitIndicator.Yes,
            PetId = Pets[15].Id,
            Pet = Pets[15],
            VetId = Vets[1].Id,
            Vet = Vets[1],
            ServiceId = Services[0].Id,
            Service = Services[0]
        }
    ];
}