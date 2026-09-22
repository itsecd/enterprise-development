using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Shared.Enums;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Тестовые данные предметной области салона красоты
/// </summary>
public static class BeautySalonData
{
    /// <summary>
    /// Посетители салона
    /// </summary>
    public static List<Customer> Customers { get; } =
    [
        new() { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Сергеевич", Gender = Gender.Male, DateOfBirth = new DateOnly(1998, 2, 14), PhoneNumber = "+79000000001" },
        new() { Id = 2, LastName = "Петрова", FirstName = "Анна", Patronymic = "Игоревна", Gender = Gender.Female, DateOfBirth = new DateOnly(1995, 6, 21), PhoneNumber = "+79000000002" },
        new() { Id = 3, LastName = "Соколова", FirstName = "Мария", Patronymic = "Андреевна", Gender = Gender.Female, DateOfBirth = new DateOnly(2001, 1, 9), PhoneNumber = "+79000000003" },
        new() { Id = 4, LastName = "Кузнецов", FirstName = "Дмитрий", Patronymic = "Олегович", Gender = Gender.Male, DateOfBirth = new DateOnly(1992, 11, 3), PhoneNumber = "+79000000004" },
        new() { Id = 5, LastName = "Морозова", FirstName = "Ольга", Patronymic = "Викторовна", Gender = Gender.Female, DateOfBirth = new DateOnly(1989, 8, 17), PhoneNumber = "+79000000005" },
        new() { Id = 6, LastName = "Волков", FirstName = "Максим", Patronymic = "Алексеевич", Gender = Gender.Male, DateOfBirth = new DateOnly(1997, 4, 28), PhoneNumber = "+79000000006" },
        new() { Id = 7, LastName = "Орлова", FirstName = "Елена", Patronymic = "Дмитриевна", Gender = Gender.Female, DateOfBirth = new DateOnly(2000, 9, 12), PhoneNumber = "+79000000007" },
        new() { Id = 8, LastName = "Смирнов", FirstName = "Алексей", Patronymic = "Романович", Gender = Gender.Male, DateOfBirth = new DateOnly(1994, 12, 1), PhoneNumber = "+79000000008" },
        new() { Id = 9, LastName = "Федорова", FirstName = "Наталья", Patronymic = "Сергеевна", Gender = Gender.Female, DateOfBirth = new DateOnly(1987, 3, 25), PhoneNumber = "+79000000009" },
        new() { Id = 10, LastName = "Никитин", FirstName = "Артем", Patronymic = null, Gender = Gender.Male, DateOfBirth = new DateOnly(2002, 7, 30), PhoneNumber = "+79000000010" }
    ];

    /// <summary>
    /// Специалисты салона
    /// </summary>
    public static List<BeautySpecialist> Specialists { get; } =
    [
        new() { Id = 1, LastName = "Крылова", FirstName = "Алина", Patronymic = "Павловна", Gender = Gender.Female, DateOfBirth = new DateOnly(1990, 2, 11), PhoneNumber = "+79010000001", PassportNumber = "4501000001", Specialization = "Парикмахер", WorkExperience = 3 },
        new() { Id = 2, LastName = "Семенова", FirstName = "Ирина", Patronymic = "Владимировна", Gender = Gender.Female, DateOfBirth = new DateOnly(1988, 5, 19), PhoneNumber = "+79010000002", PassportNumber = "4501000002", Specialization = "Парикмахер", WorkExperience = 5 },
        new() { Id = 3, LastName = "Михайлов", FirstName = "Роман", Patronymic = "Ильич", Gender = Gender.Male, DateOfBirth = new DateOnly(1987, 9, 4), PhoneNumber = "+79010000003", PassportNumber = "4501000003", Specialization = "Барбер", WorkExperience = 7 },
        new() { Id = 4, LastName = "Белова", FirstName = "Екатерина", Patronymic = "Олеговна", Gender = Gender.Female, DateOfBirth = new DateOnly(1992, 1, 27), PhoneNumber = "+79010000004", PassportNumber = "4501000004", Specialization = "Мастер маникюра", WorkExperience = 9 },
        new() { Id = 5, LastName = "Зайцев", FirstName = "Денис", Patronymic = "Андреевич", Gender = Gender.Male, DateOfBirth = new DateOnly(1985, 6, 8), PhoneNumber = "+79010000005", PassportNumber = "4501000005", Specialization = "Массажист", WorkExperience = 12 },
        new() { Id = 6, LastName = "Орлова", FirstName = "Марина", Patronymic = "Игоревна", Gender = Gender.Female, DateOfBirth = new DateOnly(1991, 10, 15), PhoneNumber = "+79010000006", PassportNumber = "4501000006", Specialization = "Бровист", WorkExperience = 4 },
        new() { Id = 7, LastName = "Громов", FirstName = "Николай", Patronymic = "Сергеевич", Gender = Gender.Male, DateOfBirth = new DateOnly(1986, 12, 22), PhoneNumber = "+79010000007", PassportNumber = "4501000007", Specialization = "Парикмахер", WorkExperience = 6 },
        new() { Id = 8, LastName = "Власова", FirstName = "Светлана", Patronymic = "Алексеевна", Gender = Gender.Female, DateOfBirth = new DateOnly(1993, 4, 16), PhoneNumber = "+79010000008", PassportNumber = "4501000008", Specialization = "Визажист", WorkExperience = 8 },
        new() { Id = 9, LastName = "Фролов", FirstName = "Евгений", Patronymic = "Максимович", Gender = Gender.Male, DateOfBirth = new DateOnly(1989, 7, 7), PhoneNumber = "+79010000009", PassportNumber = "4501000009", Specialization = "Массажист", WorkExperience = 10 },
        new() { Id = 10, LastName = "Романова", FirstName = "Дарья", Patronymic = "Артемовна", Gender = Gender.Female, DateOfBirth = new DateOnly(1996, 3, 13), PhoneNumber = "+79010000010", PassportNumber = "4501000010", Specialization = "Мастер маникюра", WorkExperience = 2 }
    ];

    /// <summary>
    /// Справочник услуг
    /// </summary>
    public static List<Service> Services { get; } =
    [
        new() { Id = 1, Name = "Женская стрижка", Category = "Волосы", Price = 2500m, DurationMinutes = 90 },
        new() { Id = 2, Name = "Мужская стрижка", Category = "Волосы", Price = 1800m, DurationMinutes = 60 },
        new() { Id = 3, Name = "Окрашивание волос", Category = "Волосы", Price = 5500m, DurationMinutes = 180 },
        new() { Id = 4, Name = "Маникюр", Category = "Ногтевой сервис", Price = 2200m, DurationMinutes = 90 },
        new() { Id = 5, Name = "Педикюр", Category = "Ногтевой сервис", Price = 2800m, DurationMinutes = 100 },
        new() { Id = 6, Name = "Макияж", Category = "Макияж", Price = 3500m, DurationMinutes = 90 },
        new() { Id = 7, Name = "Коррекция бровей", Category = "Брови", Price = 1200m, DurationMinutes = 40 },
        new() { Id = 8, Name = "Массаж лица", Category = "Уход", Price = 2000m, DurationMinutes = 60 },
        new() { Id = 9, Name = "Расслабляющий массаж", Category = "Массаж", Price = 4000m, DurationMinutes = 90 },
        new() { Id = 10, Name = "Укладка", Category = "Волосы", Price = 2000m, DurationMinutes = 60 }
    ];

    /// <summary>
    /// Записи посетителей на услуги
    /// </summary>
    public static List<Appointment> Appointments { get; } =
    [
        new() { Id = 1, Customer = Customers[0], Specialist = Specialists[1], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(2).AddHours(9), IsRegularCustomer = true },
        new() { Id = 2, Customer = Customers[1], Specialist = Specialists[1], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(2).AddHours(12), IsRegularCustomer = true },
        new() { Id = 3, Customer = Customers[2], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(2).AddHours(17), IsRegularCustomer = true },
        new() { Id = 4, Customer = Customers[3], Specialist = Specialists[1], Service = Services[9], AppointmentDateTime = DateTime.Today.AddDays(2).AddHours(20), IsRegularCustomer = true },

        new() { Id = 5, Customer = Customers[0], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-10).AddHours(10), IsRegularCustomer = true },
        new() { Id = 6, Customer = Customers[0], Specialist = Specialists[1], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-20).AddHours(11), IsRegularCustomer = true },
        new() { Id = 7, Customer = Customers[0], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-25).AddHours(12), IsRegularCustomer = true },
        new() { Id = 8, Customer = Customers[0], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-45).AddHours(10), IsRegularCustomer = true },

        new() { Id = 9, Customer = Customers[1], Specialist = Specialists[3], Service = Services[3], AppointmentDateTime = DateTime.Today.AddDays(-8).AddHours(12), IsRegularCustomer = true },
        new() { Id = 10, Customer = Customers[1], Specialist = Specialists[3], Service = Services[3], AppointmentDateTime = DateTime.Today.AddDays(-18).AddHours(13), IsRegularCustomer = true },

        new() { Id = 11, Customer = Customers[4], Specialist = Specialists[2], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-12).AddHours(14), IsRegularCustomer = true },
        new() { Id = 12, Customer = Customers[4], Specialist = Specialists[7], Service = Services[5], AppointmentDateTime = DateTime.Today.AddDays(-22).AddHours(15), IsRegularCustomer = true },

        new() { Id = 13, Customer = Customers[7], Specialist = Specialists[7], Service = Services[5], AppointmentDateTime = DateTime.Today.AddDays(-3).AddHours(10), IsRegularCustomer = true },
        new() { Id = 14, Customer = Customers[7], Specialist = Specialists[7], Service = Services[5], AppointmentDateTime = DateTime.Today.AddDays(-7).AddHours(11), IsRegularCustomer = true },
        new() { Id = 15, Customer = Customers[7], Specialist = Specialists[7], Service = Services[5], AppointmentDateTime = DateTime.Today.AddDays(-14).AddHours(12), IsRegularCustomer = true },
        new() { Id = 16, Customer = Customers[7], Specialist = Specialists[8], Service = Services[8], AppointmentDateTime = DateTime.Today.AddDays(-21).AddHours(13), IsRegularCustomer = true },

        new() { Id = 17, Customer = Customers[3], Specialist = Specialists[4], Service = Services[8], AppointmentDateTime = DateTime.Today.AddDays(-9).AddHours(11), IsRegularCustomer = true },
        new() { Id = 18, Customer = Customers[8], Specialist = Specialists[0], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-16).AddHours(10), IsRegularCustomer = true },
        new() { Id = 19, Customer = Customers[8], Specialist = Specialists[2], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-40).AddHours(11), IsRegularCustomer = true },

        new() { Id = 20, Customer = Customers[2], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-6).AddHours(16), IsRegularCustomer = true },
        new() { Id = 21, Customer = Customers[5], Specialist = Specialists[6], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-11).AddHours(10), IsRegularCustomer = true },
        new() { Id = 22, Customer = Customers[6], Specialist = Specialists[5], Service = Services[6], AppointmentDateTime = DateTime.Today.AddDays(-13).AddHours(11), IsRegularCustomer = true },
        new() { Id = 23, Customer = Customers[9], Specialist = Specialists[9], Service = Services[3], AppointmentDateTime = DateTime.Today.AddDays(-15).AddHours(12), IsRegularCustomer = false },

        new() { Id = 24, Customer = Customers[5], Specialist = Specialists[6], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-50).AddHours(10), IsRegularCustomer = true },
        new() { Id = 25, Customer = Customers[6], Specialist = Specialists[5], Service = Services[6], AppointmentDateTime = DateTime.Today.AddDays(-42).AddHours(11), IsRegularCustomer = true },
        new() { Id = 26, Customer = Customers[9], Specialist = Specialists[9], Service = Services[3], AppointmentDateTime = DateTime.Today.AddDays(-48).AddHours(12), IsRegularCustomer = false },
        new() { Id = 27, Customer = Customers[2], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-38).AddHours(16), IsRegularCustomer = true },

        new() { Id = 28, Customer = Customers[5], Specialist = Specialists[6], Service = Services[1], AppointmentDateTime = DateTime.Today.AddDays(-17).AddHours(9), IsRegularCustomer = true },
        new() { Id = 29, Customer = Customers[6], Specialist = Specialists[5], Service = Services[6], AppointmentDateTime = DateTime.Today.AddDays(-19).AddHours(9), IsRegularCustomer = true },
        new() { Id = 30, Customer = Customers[9], Specialist = Specialists[9], Service = Services[4], AppointmentDateTime = DateTime.Today.AddDays(-23).AddHours(9), IsRegularCustomer = false },
        new() { Id = 31, Customer = Customers[2], Specialist = Specialists[1], Service = Services[9], AppointmentDateTime = DateTime.Today.AddDays(-4).AddHours(9), IsRegularCustomer = true },
        new() { Id = 32, Customer = Customers[5], Specialist = Specialists[6], Service = Services[9], AppointmentDateTime = DateTime.Today.AddDays(-2).AddHours(9), IsRegularCustomer = true },
        new() { Id = 33, Customer = Customers[9], Specialist = Specialists[9], Service = Services[3], AppointmentDateTime = DateTime.Today.AddDays(-27).AddHours(9), IsRegularCustomer = false },
        new() { Id = 34, Customer = Customers[6], Specialist = Specialists[5], Service = Services[6], AppointmentDateTime = DateTime.Today.AddDays(-1).AddHours(9), IsRegularCustomer = true },
        new() { Id = 35, Customer = Customers[2], Specialist = Specialists[1], Service = Services[0], AppointmentDateTime = DateTime.Today.AddDays(-29).AddHours(9), IsRegularCustomer = true }
    ];
}
