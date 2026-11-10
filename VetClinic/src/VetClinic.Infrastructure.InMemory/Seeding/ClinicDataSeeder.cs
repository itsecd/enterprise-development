using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;

namespace VetClinic.Infrastructure.Seeding;

/// <summary>
/// Класс детерминированной инициализации тестовых данных
/// </summary>
public class ClinicDataSeeder
{
    /// <summary>
    /// Список пород
    /// </summary>
    public List<Breed> Breeds { get; }

    /// <summary>
    /// Список специализаций врачей
    /// </summary>
    public List<Specialization> Specializations { get; }

    /// <summary>
    /// Список владельцев питомцев
    /// </summary>
    public List<Owner> Owners { get; }

    /// <summary>
    /// Список питомцев
    /// </summary>
    public List<Pet> Pets { get; }

    /// <summary>
    /// Список ветеринаров
    /// </summary>
    public List<Veterinarian> Veterinarians { get; }

    /// <summary>
    /// Список приемов
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Инициализирует фиксированные тестовые данные ветеринарной клиники
    /// </summary>
    public ClinicDataSeeder()
    {
        // 1. Породы (12 штук >= 10)
        Breeds =
        [
            new Breed { Id = 1, Species = AnimalSpecies.Dog, Name = "Лабрадор ретривер" },
            new Breed { Id = 2, Species = AnimalSpecies.Dog, Name = "Немецкая овчарка" },
            new Breed { Id = 3, Species = AnimalSpecies.Dog, Name = "Французский бульдог" },
            new Breed { Id = 4, Species = AnimalSpecies.Cat, Name = "Мейн-кун" },
            new Breed { Id = 5, Species = AnimalSpecies.Cat, Name = "Британская короткошерстная" },
            new Breed { Id = 6, Species = AnimalSpecies.Cat, Name = "Сфинкс" },
            new Breed { Id = 7, Species = AnimalSpecies.Bird, Name = "Волнистый попугай" },
            new Breed { Id = 8, Species = AnimalSpecies.Bird, Name = "Корелла" },
            new Breed { Id = 9, Species = AnimalSpecies.Rodent, Name = "Джунгарский хомяк" },
            new Breed { Id = 10, Species = AnimalSpecies.Rodent, Name = "Морская свинка" },
            new Breed { Id = 11, Species = AnimalSpecies.Reptile, Name = "Бородатая агама" },
            new Breed { Id = 12, Species = AnimalSpecies.Reptile, Name = "Йеменский хамелеон" }
        ];

        // 2. Специализации (10 штук >= 10)
        Specializations =
        [
            new Specialization { Id = 1, TargetSpecies = AnimalSpecies.Cat, Name = "Терапия кошек" },
            new Specialization { Id = 2, TargetSpecies = AnimalSpecies.Cat, Name = "Хирургия кошек" },
            new Specialization { Id = 3, TargetSpecies = AnimalSpecies.Cat, Name = "Дерматология кошек" },
            new Specialization { Id = 4, TargetSpecies = AnimalSpecies.Dog, Name = "Терапия собак" },
            new Specialization { Id = 5, TargetSpecies = AnimalSpecies.Dog, Name = "Хирургия собак" },
            new Specialization { Id = 6, TargetSpecies = AnimalSpecies.Bird, Name = "Орнитология" },
            new Specialization { Id = 7, TargetSpecies = AnimalSpecies.Bird, Name = "Хирургия птиц" },
            new Specialization { Id = 8, TargetSpecies = AnimalSpecies.Rodent, Name = "Ратология" },
            new Specialization { Id = 9, TargetSpecies = AnimalSpecies.Rodent, Name = "Терапия грызунов" },
            new Specialization { Id = 10, TargetSpecies = AnimalSpecies.Reptile, Name = "Герпетология" }
        ];

        // 3. Владельцы (10 штук >= 10)
        Owners =
        [
            new Owner { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+7-900-111-22-33", Address = "ул. Ленина, д. 10" },
            new Owner { Id = 2, FullName = "Петров Петр Петрович", Phone = "+7-900-222-33-44", Address = null },
            new Owner { Id = 3, FullName = "Сидоров Сидор Сидорович", Phone = "+7-900-333-44-55", Address = "ул. Мира, д. 5" },
            new Owner { Id = 4, FullName = "Алексеев Алексей Алексеевич", Phone = "+7-900-444-55-66", Address = null },
            new Owner { Id = 5, FullName = "Васильев Василий Васильевич", Phone = "+7-900-555-66-77", Address = "ул. Гагарина, д. 1" },
            new Owner { Id = 6, FullName = "Дмитриев Дмитрий Дмитриевич", Phone = "+7-900-666-77-88", Address = "ул. Кирова, д. 12" },
            new Owner { Id = 7, FullName = "Елисеев Елисей Елисеевич", Phone = "+7-900-777-88-99", Address = null },
            new Owner { Id = 8, FullName = "Федоров Федор Федорович", Phone = "+7-900-888-99-00", Address = "пр. Победы, д. 20" },
            new Owner { Id = 9, FullName = "Григорьев Григорий Григорьевич", Phone = "+7-900-999-00-11", Address = null },
            new Owner { Id = 10, FullName = "Николаев Николай Николаевич", Phone = "+7-900-000-11-22", Address = "ул. Чехова, д. 8" }
        ];

        // 4. Питомцы (10 штук >= 10, у Иванова и Петрова по 2 питомца)
        Pets =
        [
            new Pet { Id = 1, Name = "Шарик", Species = AnimalSpecies.Dog, BreedId = 1, Breed = Breeds[0], BirthDate = new DateOnly(2020, 1, 15), WeightKg = 28.5m, OwnerId = 1, Owner = Owners[0] },
            new Pet { Id = 2, Name = "Барсик", Species = AnimalSpecies.Cat, BreedId = 4, Breed = Breeds[3], BirthDate = new DateOnly(2021, 4, 10), WeightKg = 6.8m, OwnerId = 1, Owner = Owners[0] },
            new Pet { Id = 3, Name = "Рекс", Species = AnimalSpecies.Dog, BreedId = 2, Breed = Breeds[1], BirthDate = new DateOnly(2019, 7, 20), WeightKg = 34.0m, OwnerId = 2, Owner = Owners[1] },
            new Pet { Id = 4, Name = "Мурка", Species = AnimalSpecies.Cat, BreedId = 5, Breed = Breeds[4], BirthDate = new DateOnly(2022, 3, 5), WeightKg = 4.2m, OwnerId = 2, Owner = Owners[1] },
            new Pet { Id = 5, Name = "Кеша", Species = AnimalSpecies.Bird, BreedId = 7, Breed = Breeds[6], BirthDate = new DateOnly(2023, 2, 1), WeightKg = 0.04m, OwnerId = 3, Owner = Owners[2] },
            new Pet { Id = 6, Name = "Хома", Species = AnimalSpecies.Rodent, BreedId = 9, Breed = Breeds[8], BirthDate = new DateOnly(2023, 10, 15), WeightKg = 0.05m, OwnerId = 4, Owner = Owners[3] },
            new Pet { Id = 7, Name = "Горыныч", Species = AnimalSpecies.Reptile, BreedId = 11, Breed = Breeds[10], BirthDate = new DateOnly(2021, 11, 25), WeightKg = 0.45m, OwnerId = 5, Owner = Owners[4] },
            new Pet { Id = 8, Name = "Белла", Species = AnimalSpecies.Dog, BreedId = 3, Breed = Breeds[2], BirthDate = new DateOnly(2022, 5, 12), WeightKg = 11.5m, OwnerId = 6, Owner = Owners[5] },
            new Pet { Id = 9, Name = "Симба", Species = AnimalSpecies.Cat, BreedId = 6, Breed = Breeds[5], BirthDate = new DateOnly(2020, 8, 30), WeightKg = 3.9m, OwnerId = 7, Owner = Owners[6] },
            new Pet { Id = 10, Name = "Карлуша", Species = AnimalSpecies.Bird, BreedId = 8, Breed = Breeds[7], BirthDate = new DateOnly(2022, 1, 18), WeightKg = 0.09m, OwnerId = 8, Owner = Owners[7] }
        ];

        // Привязываем питомцев к владельцам
        Owners[0].Pets.AddRange([Pets[0], Pets[1]]);
        Owners[1].Pets.AddRange([Pets[2], Pets[3]]);
        Owners[2].Pets.Add(Pets[4]);
        Owners[3].Pets.Add(Pets[5]);
        Owners[4].Pets.Add(Pets[6]);
        Owners[5].Pets.Add(Pets[7]);
        Owners[6].Pets.Add(Pets[8]);
        Owners[7].Pets.Add(Pets[9]);

        // 5. Ветеринары (10 штук >= 10, первые 3 специализируются на Cat)
        Veterinarians =
        [
            new Veterinarian { Id = 1, PassportNumber = "4501 111111", FullName = "Смирнов Андрей Викторович", BirthYear = 1980, ExperienceYears = 18, SpecializationId = 1, Specialization = Specializations[0] },
            new Veterinarian { Id = 2, PassportNumber = "4502 222222", FullName = "Кузнецова Анна Павловна", BirthYear = 1985, ExperienceYears = 12, SpecializationId = 2, Specialization = Specializations[1] },
            new Veterinarian { Id = 3, PassportNumber = "4503 333333", FullName = "Морозов Игорь Сергеевич", BirthYear = 1990, ExperienceYears = 7, SpecializationId = 3, Specialization = Specializations[2] },
            new Veterinarian { Id = 4, PassportNumber = "4504 444444", FullName = "Попова Елена Дмитриевна", BirthYear = 1978, ExperienceYears = 21, SpecializationId = 4, Specialization = Specializations[3] },
            new Veterinarian { Id = 5, PassportNumber = "4505 555555", FullName = "Лебедев Максим Олегович", BirthYear = 1988, ExperienceYears = 9, SpecializationId = 5, Specialization = Specializations[4] },
            new Veterinarian { Id = 6, PassportNumber = "4506 666666", FullName = "Новикова Ольга Игоревна", BirthYear = 1992, ExperienceYears = 5, SpecializationId = 6, Specialization = Specializations[5] },
            new Veterinarian { Id = 7, PassportNumber = "4507 777777", FullName = "Соколов Артем Владимирович", BirthYear = 1983, ExperienceYears = 15, SpecializationId = 7, Specialization = Specializations[6] },
            new Veterinarian { Id = 8, PassportNumber = "4508 888888", FullName = "Козлов Денис Юрьевич", BirthYear = 1995, ExperienceYears = 3, SpecializationId = 8, Specialization = Specializations[7] },
            new Veterinarian { Id = 9, PassportNumber = "4509 999999", FullName = "Павлова Марина Сергеевна", BirthYear = 1987, ExperienceYears = 11, SpecializationId = 9, Specialization = Specializations[8] },
            new Veterinarian { Id = 10, PassportNumber = "4510 000000", FullName = "Семенов Кирилл Алексеевич", BirthYear = 1982, ExperienceYears = 16, SpecializationId = 10, Specialization = Specializations[9] }
        ];

        // 6. Записи на прием (10 штук >= 10)
        Appointments =
        [
            // Приемы в кабинете 101 в сентябре 2026:
            new Appointment { Id = 1, DateTime = new DateTime(2026, 9, 10, 10, 0, 0), RoomNumber = 101, IsFollowUp = false, PetId = 1, Pet = Pets[0], VeterinarianId = 4, Veterinarian = Veterinarians[3] },
            new Appointment { Id = 2, DateTime = new DateTime(2026, 9, 15, 11, 0, 0), RoomNumber = 101, IsFollowUp = true, PetId = 1, Pet = Pets[0], VeterinarianId = 4, Veterinarian = Veterinarians[3] },
            new Appointment { Id = 3, DateTime = new DateTime(2026, 9, 20, 14, 0, 0), RoomNumber = 101, IsFollowUp = false, PetId = 3, Pet = Pets[2], VeterinarianId = 4, Veterinarian = Veterinarians[3] },

            // Прием в кабинете 101, в августе 2026:
            new Appointment { Id = 4, DateTime = new DateTime(2026, 8, 25, 12, 0, 0), RoomNumber = 101, IsFollowUp = false, PetId = 1, Pet = Pets[0], VeterinarianId = 4, Veterinarian = Veterinarians[3] },

            // Приемы в других кабинетах:
            new Appointment { Id = 5, DateTime = new DateTime(2026, 9, 12, 9, 30, 0), RoomNumber = 102, IsFollowUp = true, PetId = 2, Pet = Pets[1], VeterinarianId = 1, Veterinarian = Veterinarians[0] },
            new Appointment { Id = 6, DateTime = new DateTime(2026, 9, 18, 15, 0, 0), RoomNumber = 102, IsFollowUp = false, PetId = 4, Pet = Pets[3], VeterinarianId = 2, Veterinarian = Veterinarians[1] },
            new Appointment { Id = 7, DateTime = new DateTime(2026, 9, 5, 10, 0, 0), RoomNumber = 103, IsFollowUp = false, PetId = 5, Pet = Pets[4], VeterinarianId = 6, Veterinarian = Veterinarians[5] },
            new Appointment { Id = 8, DateTime = new DateTime(2026, 9, 14, 16, 0, 0), RoomNumber = 103, IsFollowUp = true, PetId = 1, Pet = Pets[0], VeterinarianId = 4, Veterinarian = Veterinarians[3] },
            new Appointment { Id = 9, DateTime = new DateTime(2026, 9, 22, 11, 30, 0), RoomNumber = 104, IsFollowUp = false, PetId = 6, Pet = Pets[5], VeterinarianId = 8, Veterinarian = Veterinarians[7] },
            new Appointment { Id = 10, DateTime = new DateTime(2026, 9, 28, 13, 0, 0), RoomNumber = 105, IsFollowUp = false, PetId = 7, Pet = Pets[6], VeterinarianId = 10, Veterinarian = Veterinarians[9] }
        ];

        Pets[0].Appointments.AddRange([Appointments[0], Appointments[1], Appointments[3], Appointments[7]]);
        Pets[1].Appointments.Add(Appointments[4]);
        Pets[2].Appointments.Add(Appointments[2]);
        Pets[3].Appointments.Add(Appointments[5]);
        Pets[4].Appointments.Add(Appointments[6]);
        Pets[5].Appointments.Add(Appointments[8]);
        Pets[6].Appointments.Add(Appointments[9]);

        Veterinarians[3].Appointments.AddRange([Appointments[0], Appointments[1], Appointments[2], Appointments[3], Appointments[7]]);
    }
}