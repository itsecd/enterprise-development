using Vet.Domain.Entities;
using Vet.Domain.Enums;

namespace Vet.Domain.DataSeed;

/// <summary>
/// Тестовый набор данных для проверки запросов ветеринарной клиники
/// </summary>
public class VetClinicTestFixture
{
    /// <summary>
    /// Фиксированная дата
    /// </summary>
    public static readonly DateTime ReferenceDate = new(2026, 10, 15, 12, 0, 0);

    /// <summary>
    /// Список пород
    /// </summary>
    public readonly List<Breed> Breeds;

    /// <summary>
    /// Список специализаций
    /// </summary>
    public readonly List<Specialization> Specializations;

    /// <summary>
    /// Список ветеринаров
    /// </summary>
    public readonly List<Veterinarian> Veterinarians;

    /// <summary>
    /// Список хозяев
    /// </summary>
    public readonly List<Owner> Owners;

    /// <summary>
    /// Список питомцев
    /// </summary>
    public readonly List<Pet> Pets;

    /// <summary>
    /// Список записей на прием
    /// </summary>
    public readonly List<Appointment> Appointments;


    /// <summary>
    /// Инициализирует тестовые данные
    /// </summary>
    public VetClinicTestFixture()
    {
        Breeds = GetBreeds();
        Specializations = GetSpecializations();
        Veterinarians = GetVeterinarians(Specializations);
        Owners = GetOwners();
        Pets = GetPets(Breeds, Owners);
        Appointments = GetAppointments(Pets, Veterinarians);

        foreach (var pet in Pets)
            pet.Owner.Pets.Add(pet);

        foreach (var appt in Appointments)
        {
            appt.Pet.Appointments.Add(appt);
            appt.Veterinarian.Appointments.Add(appt);
        }
    }

    /// <summary>
    /// Справочник пород
    /// </summary>
    private static List<Breed> GetBreeds() =>
    [
        new Breed {
            Id = 0,
            Name = "Лабрадор",
            Species = Species.Dog
        },
        new Breed {
            Id = 1,
            Name = "Немецкая овчарка",
            Species = Species.Dog
        },
        new Breed {
            Id = 2,
            Name = "Британская короткошёрстная",
            Species = Species.Cat
        },
        new Breed {
            Id = 3,
            Name = "Мейн-кун",
            Species = Species.Cat
        },
        new Breed {
            Id = 4,
            Name = "Волнистый попугай",
            Species = Species.Bird
        },
        new Breed {
            Id = 5,
            Name = "Корелла",
            Species = Species.Bird
        },
        new Breed {
            Id = 6,
            Name = "Сирийский хомяк",
            Species = Species.Hamster
        },
        new Breed {
            Id = 7,
            Name = "Джунгарский хомяк",
            Species = Species.Hamster
        },
        new Breed {
            Id = 8,
            Name = "Белая мышь",
            Species = Species.Mouse
        },
        new Breed {
            Id = 9,
            Name = "Кролик баран",
            Species = Species.Rabbit
        },
        new Breed {
            Id = 10,
            Name = "Красноухая черепаха",
            Species = Species.Turtle
        },
        new Breed {
            Id = 11,
            Name = "Среднеазиатская черепаха",
            Species = Species.Turtle
        }
    ];

    /// <summary>
    /// Справочник специализаций
    /// </summary>
    private static List<Specialization> GetSpecializations() =>
    [
        new Specialization {
            Id = 0,
            Name = "Терапевт",
            Species = [Species.Cat, Species.Dog]
        },
        new Specialization {
            Id = 1,
            Name = "Хирург",
            Species = [Species.Dog]
        },
        new Specialization {
            Id = 2,
            Name = "Орнитолог",
            Species = [Species.Bird]
        },
        new Specialization {
            Id = 3,
            Name = "Универсал",
            Species = [..Enum.GetValues<Species>()]
        },
        new Specialization {
            Id = 4,
            Name = "Фелинолог",
            Species = [Species.Cat]
        },
        new Specialization {
            Id = 5,
            Name = "Герпетолог",
            Species = [Species.Turtle]
        },
        new Specialization {
            Id = 6,
            Name = "Ратолог",
            Species = [Species.Hamster, Species.Mouse, Species.Rabbit]
        },
        new Specialization {
            Id = 7,
            Name = "Офтальмолог",
            Species = [Species.Cat, Species.Dog]
        },
        new Specialization {
            Id = 8,
            Name = "Стоматолог",
            Species = [Species.Cat, Species.Dog]
        },
        new Specialization {
            Id = 9,
            Name = "Дерматолог",
            Species = [Species.Cat, Species.Dog]
        },
        new Specialization {
            Id = 10,
            Name = "Кардиолог",
            Species = [Species.Cat, Species.Dog]
        },
        new Specialization {
            Id = 11,
            Name = "Невролог",
            Species = [Species.Cat, Species.Dog]
        }
    ];

    /// <summary>
    /// Ветеринары
    /// </summary>
    private static List<Veterinarian> GetVeterinarians(List<Specialization> spec) =>
    [
        new Veterinarian {
            Id = 0,
            PassportNumber = "1001 000001",
            FullName = "Иванов Иван Иванович",
            Specialization = spec[0],
            BirthYear = 1980,
            WorkExperience = 15
        },
        new Veterinarian {
            Id = 1,
            PassportNumber = "1002 000002",
            FullName = "Петров Пётр Петрович",
            Specialization = spec[1],
            BirthYear = 1975,
            WorkExperience = 20
        },
        new Veterinarian {
            Id = 2,
            PassportNumber = "1003 000003",
            FullName = "Сидоров Сидор Сидорович",
            Specialization = spec[2],
            BirthYear = 1985,
            WorkExperience = 10
        },
        new Veterinarian {
            Id = 3,
            PassportNumber = "1004 000004",
            FullName = "Универсалов Универсал Универсалович",
            Specialization = spec[3],
            BirthYear = 1990,
            WorkExperience = 8
        },
        new Veterinarian {
            Id = 4,
            PassportNumber = "1005 000005",
            FullName = "Кузнецов Кузьма Кузьмич",
            Specialization = spec[0],
            BirthYear = 1988,
            WorkExperience = 12
        },
        new Veterinarian {
            Id = 5,
            PassportNumber = "1006 000006",
            FullName = "Соколов Степан Степанович",
            Specialization = spec[1],
            BirthYear = 1982,
            WorkExperience = 14
        },
        new Veterinarian {
            Id = 6,
            PassportNumber = "1007 000007",
            FullName = "Попов Павел Павлович",
            Specialization = spec[2],
            BirthYear = 1978,
            WorkExperience = 18
        },
        new Veterinarian {
            Id = 7,
            PassportNumber = "1008 000008",
            FullName = "Лебедев Лев Львович",
            Specialization = spec[4],
            BirthYear = 1986,
            WorkExperience = 9
        },
        new Veterinarian {
            Id = 8,
            PassportNumber = "1009 000009",
            FullName = "Козлов Кирилл Кириллович",
            Specialization = spec[3],
            BirthYear = 1992,
            WorkExperience = 5
        },
        new Veterinarian {
            Id = 9,
            PassportNumber = "1010 000010",
            FullName = "Новиков Никита Никитич",
            Specialization = spec[5],
            BirthYear = 1984,
            WorkExperience = 11
        }
    ];

    /// <summary>
    /// Владельцы
    /// </summary>
    private static List<Owner> GetOwners() =>
    [
        new Owner {
            Id = 0,
            FullName = "Иванов Иван Иванович",
            PhoneNumber = "+7-900-000-00-01"
        },
        new Owner {
            Id = 1,
            FullName = "Петрова Мария Сергеевна",
            PhoneNumber = "+7-900-000-00-02"
        },
        new Owner {
            Id = 2,
            FullName = "Сидоров Алексей Владимирович",
            PhoneNumber = "+7-900-000-00-03"
        },
        new Owner {
            Id = 3,
            FullName = "Смирнова Анна Дмитриевна",
            PhoneNumber = "+7-900-000-00-04"
        },
        new Owner {
            Id = 4,
            FullName = "Кузнецова Ольга Андреевна",
            PhoneNumber = "+7-900-000-00-05"
        },
        new Owner {
            Id = 5,
            FullName = "Алексеев Андрей Алексеевич",
            PhoneNumber = "+7-900-000-00-06"
        },
        new Owner {
            Id = 6,
            FullName = "Михайлова Мария Ивановна",
            PhoneNumber = "+7-900-000-00-07"
        },
        new Owner {
            Id = 7,
            FullName = "Фёдоров Фёдор Фёдорович",
            PhoneNumber = "+7-900-000-00-08"
        },
        new Owner {
            Id = 8,
            FullName = "Николаев Николай Николаевич",
            PhoneNumber = "+7-900-000-00-09"
        },
        new Owner {
            Id = 9,
            FullName = "Захаров Захар Захарович",
            PhoneNumber = "+7-900-000-00-10"
        }
    ];

    /// <summary>
    /// Питомцы
    /// </summary>
    private static List<Pet> GetPets(List<Breed> breeds, List<Owner> owners) =>
    [
        new Pet {
            Id = 0,
            Name = "Рекс",
            Species = Species.Dog,
            Breed = breeds[0],
            Owner = owners[0]
        },
        new Pet {
            Id = 1,
            Name = "Барсик",
            Species = Species.Cat,
            Breed = breeds[2],
            Owner = owners[0]
        },
        new Pet {
            Id = 2,
            Name = "Белла",
            Species = Species.Dog,
            Breed = breeds[1],
            Owner = owners[2]
        },
        new Pet {
            Id = 3,
            Name = "Мурка",
            Species = Species.Cat,
            Breed = breeds[3],
            Owner = owners[2]
        },
        new Pet {
            Id = 4,
            Name = "Кеша",
            Species = Species.Bird,
            Breed = breeds[4],
            Owner = owners[1]
        },
        new Pet {
            Id = 5,
            Name = "Тиша",
            Species = Species.Cat,
            Breed = breeds[2],
            Owner = owners[2]
        },
        new Pet {
            Id = 6,
            Name = "Шарик",
            Species = Species.Dog,
            Breed = breeds[0],
            Owner = owners[4]
        },
        new Pet {
            Id = 7,
            Name = "Пушок",
            Species = Species.Cat,
            Breed = breeds[3],
            Owner = owners[4]
        },
        new Pet {
            Id = 8,
            Name = "Гоша",
            Species = Species.Bird,
            Breed = breeds[5],
            Owner = owners[5]
        },
        new Pet {
            Id = 9,
            Name = "Муся",
            Species = Species.Cat,
            Breed = breeds[2],
            Owner = owners[6]
        },
        new Pet {
            Id = 10,
            Name = "Хома",
            Species = Species.Hamster,
            Breed = breeds[6],
            Owner = owners[3]
        },
        new Pet {
            Id = 11,
            Name = "Снежок",
            Species = Species.Rabbit,
            Breed = breeds[9],
            Owner = owners[7]
        }
    ];

    /// <summary>
    /// Приёмы
    /// </summary>
    private static List<Appointment> GetAppointments(List<Pet> pets, List<Veterinarian> vets)
    {
        var thisMonth = ReferenceDate;
        var lastMonth = thisMonth.AddMonths(-1);
        var nextMonth = thisMonth.AddMonths(1);

        return
        [

            new Appointment {
                Id = 0,
                Pet = pets[0],
                Veterinarian = vets[0],
                AppointmentDateTime = thisMonth.AddHours(1),
                RoomNumber = 101,
                IsRepeat = false
            },
            new Appointment {
                Id = 1,
                Pet = pets[0],
                Veterinarian = vets[0],
                AppointmentDateTime = thisMonth.AddHours(2),
                RoomNumber = 102,
                IsRepeat = true
            },
            new Appointment {
                Id = 2,
                Pet = pets[0],
                Veterinarian = vets[0],
                AppointmentDateTime = thisMonth.AddHours(3),
                RoomNumber = 101,
                IsRepeat = true
            },
            new Appointment {
                Id = 3,
                Pet = pets[1],
                Veterinarian = vets[0],
                AppointmentDateTime = thisMonth.AddHours(4),
                RoomNumber = 101,
                IsRepeat = false
            },

            new Appointment {
                Id = 4,
                Pet = pets[2],
                Veterinarian = vets[1],
                AppointmentDateTime = lastMonth,
                RoomNumber = 101,
                IsRepeat = true
            },
            new Appointment {
                Id = 12,
                Pet = pets[2],
                Veterinarian = vets[1],
                AppointmentDateTime = thisMonth.AddHours(11),
                RoomNumber = 101,
                IsRepeat = false
            },
            new Appointment {
                Id = 13,
                Pet = pets[0],
                Veterinarian = vets[1],
                AppointmentDateTime = lastMonth,
                RoomNumber = 102,
                IsRepeat = false
            },

            new Appointment {
                Id = 9,
                Pet = pets[4],
                Veterinarian = vets[2],
                AppointmentDateTime = thisMonth.AddHours(8),
                RoomNumber = 102,
                IsRepeat = true
            },

            new Appointment {
                Id = 7,  Pet = pets[7],
                Veterinarian = vets[3],
                AppointmentDateTime = nextMonth,
                RoomNumber = 101,
                IsRepeat = false
            },
            new Appointment {
                Id = 14, Pet = pets[3],
                Veterinarian = vets[3],
                AppointmentDateTime = thisMonth.AddHours(12),
                RoomNumber = 102,
                IsRepeat = true
            },

            new Appointment {
                Id = 5,  Pet = pets[3],
                Veterinarian = vets[4],
                AppointmentDateTime = thisMonth.AddHours(5),
                RoomNumber = 102,
                IsRepeat = false
            },
            new Appointment {
                Id = 11, Pet = pets[9],
                Veterinarian = vets[4],
                AppointmentDateTime = thisMonth.AddHours(10),
                RoomNumber = 101,
                IsRepeat = false
            },

            new Appointment {
                Id = 8,
                Pet = pets[6],
                Veterinarian = vets[5],
                AppointmentDateTime = thisMonth.AddHours(7),
                RoomNumber = 101,
                IsRepeat = true
            },

            new Appointment {
                Id = 10,
                Pet = pets[8],
                Veterinarian = vets[6],
                AppointmentDateTime = thisMonth.AddHours(9),
                RoomNumber = 101,
                IsRepeat = false
            },

            new Appointment {
                Id = 6,
                Pet = pets[5],
                Veterinarian = vets[7],
                AppointmentDateTime = thisMonth.AddHours(6),
                RoomNumber = 101,
                IsRepeat = true
            }
        ];
    }
}