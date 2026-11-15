using BeautySalon.Domain.Entities;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Domain.Seeds;

/// <summary>
/// Начальный набор тестовых данных салона красоты.
/// Каждый экземпляр содержит собственные независимые коллекции, связанные между собой ссылками.
/// </summary>
public class BeautySalonSeed
{
    /// <summary>
    /// Клиенты салона
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Мастера салона
    /// </summary>
    public List<Master> Masters { get; }

    /// <summary>
    /// Предоставляемые услуги
    /// </summary>
    public List<Service> Services { get; }

    /// <summary>
    /// Записи клиентов на приём
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Создаёт новый набор тестовых данных
    /// </summary>
    public BeautySalonSeed()
    {
        Clients =
        [
            new Client 
            { 
                Id = 1,
                FullName = "Иванова Анна Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1991, 1, 1),
                PhoneNumber = "+79001234561"
            },
            new Client
            {
                Id = 2,
                FullName = "Иванова Инна Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1992, 2, 2),
                PhoneNumber = "+79001234562"
            },
            new Client 
            { 
                Id = 3,
                FullName = "Иванова Василина Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1993, 3, 3),
                PhoneNumber = "+79001234563"
            },
            new Client 
            { 
                Id = 4,
                FullName = "Иванова Анастасия Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1994, 3, 3),
                PhoneNumber = "+79001234564"
            },
            new Client 
            { 
                Id = 5,
                FullName = "Иванова Клавдия Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1995, 4, 4),
                PhoneNumber = "+79001234565"
            },
            new Client 
            { 
                Id = 6,
                FullName = "Иванова Мария Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1995, 5, 5),
                PhoneNumber = "+79001234566"
            },
            new Client 
            { 
                Id = 7,
                FullName = "Иванова Марина Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1996, 6, 6),
                PhoneNumber = "+79001234567"
            },
            new Client 
            { 
                Id = 8,
                FullName = "Иванова Галина Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1997, 7, 7),
                PhoneNumber = "+79001234568"
            },
            new Client 
            { 
                Id = 9,
                FullName = "Иванова Екатерина Ивановна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1998, 8, 8),
                PhoneNumber = "+79001234569"
            },
            new Client 
            { 
                Id = 10,
                FullName = "Иванов Иван Иванович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1999, 9, 9),
                PhoneNumber = "+79001234510"
            }
        ];

        Masters =
        [
            new Master 
            {
                Id = 1,
                FullName = "Петрова Анна Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567890",
                Specialization = Specialization.Hairdresser,
                WorkExperience = 5 
            },
            new Master
            {
                Id = 2,
                FullName = "Петрова Инна Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567891", 
                Specialization = Specialization.NailArtist, 
                WorkExperience = 3 
            },
            new Master
            {
                Id = 3,
                FullName = "Петрова Василина Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567892",
                Specialization = Specialization.MakeupArtist,
                WorkExperience = 4
            },
            new Master
            {
                Id = 4,
                FullName = "Петрова Анастасия Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567893",
                Specialization = Specialization.BrowArtist,
                WorkExperience = 2
            },
            new Master
            {
                Id = 5,
                FullName = "Петрова Клавдия Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567894",
                Specialization = Specialization.LashArtist,
                WorkExperience = 6
            },
            new Master
            {
                Id = 6,
                FullName = "Петрова Мария Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567895",
                Specialization = Specialization.Cosmetologist,
                WorkExperience = 7
            },
            new Master
            {
                Id = 7,
                FullName = "Петрова Марина Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567896",
                Specialization = Specialization.MassageArtist,
                WorkExperience = 8
            },
            new Master
            {
                Id = 8,
                FullName = "Петрова Галина Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567897",
                Specialization = Specialization.LashArtist,
                WorkExperience = 6
            },
            new Master
            {
                Id = 9,
                FullName = "Петрова Екатерина Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1234 567898",
                Specialization = Specialization.Cosmetologist,
                WorkExperience = 7
            },
            new Master
            {
                Id = 10,
                FullName = "Петров Иван Иванович",
                Gender = Gender.Male,
				PassportNumber = "1234 567899",
				Specialization = Specialization.MassageArtist,
				WorkExperience = 10
			}
		];

        Services =
        [
            new Service 
            {
                Id = 1,
                Name = "Стрижка",
                Category = Category.Hair,
                Duration = TimeSpan.FromMinutes(30),
                Price = 1000
            },
            new Service 
            {
                Id = 2,
                Name = "Маникюр",
                Category = Category.Nails,
                Duration = TimeSpan.FromMinutes(60),
                Price = 1500
            },
            new Service 
            {
                Id = 3,
                Name = "Макияж",
                Category = Category.Makeup,
                Duration = TimeSpan.FromMinutes(90),
                Price = 2000
            },
            new Service 
            {
                Id = 4,
                Name = "Брови",
                Category = Category.Brows,
                Duration = TimeSpan.FromMinutes(30),
                Price = 500
            },
            new Service 
            {
                Id = 5,
                Name = "Ресницы",
                Category = Category.Lashes,
                Duration = TimeSpan.FromMinutes(60),
                Price = 1000
            },
            new Service 
            {
                Id = 6,
                Name = "Косметология",
                Category = Category.Cosmetology,
                Duration = TimeSpan.FromMinutes(90),
                Price = 2500
            },
            new Service 
            {
                Id = 7,
                Name = "Массаж",
                Category = Category.Massage,
                Duration = TimeSpan.FromMinutes(60),
                Price = 2000
            },
            new Service 
            {
                Id = 8,
                Name = "Маникюр",
                Category = Category.Nails,
                Duration = TimeSpan.FromMinutes(60),
                Price = 1500
            },
            new Service 
            {
                Id = 9,
                Name = "Макияж",
                Category = Category.Makeup,
                Duration = TimeSpan.FromMinutes(90),
                Price = 2000
            },
            new Service 
            {
                Id = 10,
                Name = "Брови",
                Category = Category.Brows,
                Duration = TimeSpan.FromMinutes(30),
                Price = 500
            }
        ];

        Appointments =
        [
            new Appointment {
                Id = 1,
                Client = Clients[0],
                Master = Masters[0],
                Service = Services[0],
                DateTime = new DateTime(2024, 6, 1, 10, 0, 0)
            },
            new Appointment {
                Id = 2,
                Client = Clients[0], 
                Master = Masters[1],
                Service = Services[1],
                DateTime = new DateTime(2024, 6, 2, 11, 0, 0)
            },
            new Appointment {
                Id = 3,
                Client = Clients[2],
                Master = Masters[2],
                Service = Services[2],
                DateTime = new DateTime(2024, 6, 3, 12, 0, 0)
            },
            new Appointment {
                Id = 4,
                Client = Clients[3],
                Master = Masters[3],
                Service = Services[3],
                DateTime = new DateTime(2024, 6, 4, 13, 0, 0)
            },
            new Appointment {
                Id = 5,
                Client = Clients[4],
                Master = Masters[4],
                Service = Services[4],
                DateTime = new DateTime(2024, 6, 5, 14, 0, 0)
            },
            new Appointment {
                Id = 6,
                Client = Clients[5],
                Master = Masters[5],
                Service = Services[0],
                DateTime = new DateTime(2024, 6, 6, 15, 0, 0)
            },
            new Appointment {
                Id = 7,
                Client = Clients[6],
                Master = Masters[6],
                Service = Services[4],
                DateTime = new DateTime(2024, 6, 7, 16, 0, 0)
            },
            new Appointment {
                Id = 8,
                Client = Clients[7],
                Master = Masters[7],
                Service = Services[7],
                DateTime = new DateTime(2024, 6, 8, 17, 0, 0)
            },
            new Appointment {
                Id = 9,
                Client = Clients[8],
                Master = Masters[8],
                Service = Services[8],
                DateTime = new DateTime(2024, 6, 9, 18, 0, 0)
            },
            new Appointment {
                Id = 10,
                Client = Clients[9],
                Master = Masters[9],
                Service = Services[9],
                DateTime = new DateTime(2024, 6, 10, 19, 0, 0)
            }
        ];
    }
}