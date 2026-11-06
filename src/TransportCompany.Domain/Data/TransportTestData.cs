using TransportCompany.Domain.Models;
using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Domain.Data;

/// <summary>
/// Тестовые данные для системы транспортной компании
/// </summary>
public class TransportTestData
{
    /// <summary>
    /// Справочник моделей транспортных средств
    /// </summary>
    public IReadOnlyList<VehicleModel> VehicleModels { get; }
    
    /// <summary>
    /// Список водителей
    /// </summary>
    public IReadOnlyList<Driver> Drivers { get; }

    /// <summary>
    /// Список клиентов
    /// </summary>
    public IReadOnlyList<Client> Clients { get; }

    /// <summary>
    /// Список транспортных средств
    /// </summary>
    public IReadOnlyList<Vehicle> Vehicles { get; }

    /// <summary>
    /// Список рейсов
    /// </summary>
    public IReadOnlyList<Trip> Trips { get; }

    public TransportTestData()
    {
        VehicleModels =
        [
            new() { Id = 1,  Brand = "ГАЗ",      Name = "ГАЗель Next",     Capacity = 1.5,  RequiredLicenseCategory = LicenseCategory.B,  BodyType = BodyType.Tent,         BodyVolume = 10.5 },
            new() { Id = 2,  Brand = "КАМАЗ",    Name = "КАМАЗ 5490",      Capacity = 20.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Tent,         BodyVolume = 60.0 },
            new() { Id = 3,  Brand = "Volvo",    Name = "Volvo FH",        Capacity = 20.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Tent,         BodyVolume = 80.0 },
            new() { Id = 4,  Brand = "Scania",   Name = "Scania R450",     Capacity = 25.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Container,    BodyVolume = 75.0 },
            new() { Id = 5,  Brand = "MAN",      Name = "MAN TGX",         Capacity = 20.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Flatbed,      BodyVolume = 70.0 },
            new() { Id = 6,  Brand = "Mercedes", Name = "Mercedes Actros", Capacity = 20.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Refrigerator, BodyVolume = 82.0 },
            new() { Id = 7,  Brand = "Isuzu",    Name = "Isuzu Elf",       Capacity = 5.0,  RequiredLicenseCategory = LicenseCategory.C,  BodyType = BodyType.Tent,         BodyVolume = 20.0 },
            new() { Id = 8,  Brand = "Ford",     Name = "Ford Transit",    Capacity = 1.5,  RequiredLicenseCategory = LicenseCategory.B,  BodyType = BodyType.Tent,         BodyVolume = 12.0 },
            new() { Id = 9,  Brand = "Renault",  Name = "Renault Premium", Capacity = 18.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Tank,         BodyVolume = 24.0 },
            new() { Id = 10, Brand = "DAF",      Name = "DAF XF",          Capacity = 20.0, RequiredLicenseCategory = LicenseCategory.CE, BodyType = BodyType.Container,    BodyVolume = 78.0 }
        ];

        Drivers =
        [
            new() { Id = 1,  PassportNumber = "4501 123456", LastName = "Иванов",   FirstName = "Иван",    Patronymic = "Иванович",   Experience = 15, LicenseCategory = LicenseCategory.CE },
            new() { Id = 2,  PassportNumber = "4502 234567", LastName = "Петров",   FirstName = "Пётр",    Patronymic = "Петрович",   Experience = 12, LicenseCategory = LicenseCategory.CE },
            new() { Id = 3,  PassportNumber = "4503 345678", LastName = "Сидоров",  FirstName = "Сидор",   Patronymic = "Сидорович",  Experience = 8,  LicenseCategory = LicenseCategory.CE },
            new() { Id = 4,  PassportNumber = "4504 456789", LastName = "Кузнецов", FirstName = "Алексей", Patronymic = "Иванович",   Experience = 20, LicenseCategory = LicenseCategory.CE },
            new() { Id = 5,  PassportNumber = "4505 567890", LastName = "Смирнов",  FirstName = "Дмитрий", Patronymic = "Олегович",   Experience = 5,  LicenseCategory = LicenseCategory.C },
            new() { Id = 6,  PassportNumber = "4506 678901", LastName = "Волков",   FirstName = "Андрей",  Patronymic = "Сергеевич",  Experience = 7,  LicenseCategory = LicenseCategory.CE },
            new() { Id = 7,  PassportNumber = "4507 789012", LastName = "Морозов",  FirstName = "Сергей",  Patronymic = "Павлович",   Experience = 10, LicenseCategory = LicenseCategory.C },
            new() { Id = 8,  PassportNumber = "4508 890123", LastName = "Новиков",  FirstName = "Олег",    Patronymic = "Викторович", Experience = 3,  LicenseCategory = LicenseCategory.B },
            new() { Id = 9,  PassportNumber = "4509 901234", LastName = "Фёдоров",  FirstName = "Максим",  Patronymic = "Игоревич",   Experience = 9,  LicenseCategory = LicenseCategory.CE },
            new() { Id = 10, PassportNumber = "4510 012345", LastName = "Егоров",   FirstName = "Никита",  Patronymic = "Романович",  Experience = 4,  LicenseCategory = LicenseCategory.B }
        ];

        Clients =
        [
            new() { Id = 1,  Name = "Ромашка",        LegalForm = "ООО", Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-01" },
            new() { Id = 2,  Name = "Иванов И.И.",                       Type = ClientType.IndividualEntrepreneur, Phone = "+7 900 000-00-02" },
            new() { Id = 3,  Name = "Василёк",        LegalForm = "ООО", Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-03" },
            new() { Id = 4,  Name = "ТехноСервис",    LegalForm = "АО",  Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-04" },
            new() { Id = 5,  Name = "Логистик Плюс",  LegalForm = "ООО", Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-05" },
            new() { Id = 6,  Name = "Петров П.П.",                       Type = ClientType.IndividualEntrepreneur, Phone = "+7 900 000-00-06" },
            new() { Id = 7,  Name = "Алексеев А.А.",                     Type = ClientType.PhysicalPerson,         Phone = "+7 900 000-00-07" },
            new() { Id = 8,  Name = "АгроТрейд",      LegalForm = "ООО", Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-08" },
            new() { Id = 9,  Name = "Сидоров С.С.",                      Type = ClientType.IndividualEntrepreneur, Phone = "+7 900 000-00-09" },
            new() { Id = 10, Name = "МебельГрад",     LegalForm = "ООО", Type = ClientType.LegalPerson,          Phone = "+7 900 000-00-10" }
        ];

        Vehicles =
        [
            new() { Id = 1,  LicensePlate = "А001АА 77", Model = VehicleModels[0] },
            new() { Id = 2,  LicensePlate = "А002АА 77", Model = VehicleModels[0] },
            new() { Id = 3,  LicensePlate = "В001ВВ 77", Model = VehicleModels[1] },
            new() { Id = 4,  LicensePlate = "С001СС 77", Model = VehicleModels[2] },
            new() { Id = 5,  LicensePlate = "Е001ЕЕ 77", Model = VehicleModels[3] },
            new() { Id = 6,  LicensePlate = "К001КК 77", Model = VehicleModels[4] },
            new() { Id = 7,  LicensePlate = "М001ММ 77", Model = VehicleModels[5] },
            new() { Id = 8,  LicensePlate = "Н001НН 77", Model = VehicleModels[6] },
            new() { Id = 9,  LicensePlate = "О001ОО 77", Model = VehicleModels[7] },
            new() { Id = 10, LicensePlate = "Р001РР 77", Model = VehicleModels[8] },
            new() { Id = 11, LicensePlate = "Т001ТТ 77", Model = VehicleModels[9] },
            new() { Id = 12, LicensePlate = "У001УУ 77", Model = VehicleModels[0] }
        ];

        Trips =
        [
            new() { Id = 1,  Driver = Drivers[0], Vehicle = Vehicles[2],  Client = Clients[0],  DepartureDate = new DateOnly(2026, 1, 10), Cost = 120_000m, CargoWeight = 18.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Санкт-Петербург" },
            new() { Id = 2,  Driver = Drivers[0], Vehicle = Vehicles[2],  Client = Clients[1],  DepartureDate = new DateOnly(2026, 1, 15), Cost =  90_000m, CargoWeight = 16.0, Status = TripStatus.Completed,  DeparturePoint = "Санкт-Петербург", DestinationPoint = "Казань" },
            new() { Id = 3,  Driver = Drivers[0], Vehicle = Vehicles[2],  Client = Clients[2],  DepartureDate = new DateOnly(2026, 1, 20), Cost =  80_000m, CargoWeight = 17.0, Status = TripStatus.Completed,  DeparturePoint = "Казань",          DestinationPoint = "Москва" },
            new() { Id = 4,  Driver = Drivers[0], Vehicle = Vehicles[3],  Client = Clients[3],  DepartureDate = new DateOnly(2026, 2, 1),  Cost = 150_000m, CargoWeight = 15.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Сочи" },
            new() { Id = 5,  Driver = Drivers[1], Vehicle = Vehicles[4],  Client = Clients[0],  DepartureDate = new DateOnly(2026, 2, 3),  Cost =  50_000m, CargoWeight = 20.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Уфа" },
            new() { Id = 6,  Driver = Drivers[0], Vehicle = Vehicles[3],  Client = Clients[8],  DepartureDate = new DateOnly(2026, 2, 6),  Cost = 250_000m, CargoWeight = 18.0, Status = TripStatus.Completed,  DeparturePoint = "Сочи",            DestinationPoint = "Москва" },
            new() { Id = 7,  Driver = Drivers[1], Vehicle = Vehicles[5],  Client = Clients[4],  DepartureDate = new DateOnly(2026, 2, 8),  Cost =  60_000m, CargoWeight = 15.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Пермь" },
            new() { Id = 8,  Driver = Drivers[3], Vehicle = Vehicles[3],  Client = Clients[9],  DepartureDate = new DateOnly(2026, 2, 10), Cost =  28_000m, CargoWeight = 14.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Курск" },
            new() { Id = 9,  Driver = Drivers[2], Vehicle = Vehicles[9],  Client = Clients[7],  DepartureDate = new DateOnly(2026, 2, 12), Cost =  25_000m, CargoWeight = 15.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Калуга" },
            new() { Id = 10, Driver = Drivers[1], Vehicle = Vehicles[6],  Client = Clients[5],  DepartureDate = new DateOnly(2026, 2, 14), Cost = 180_000m, CargoWeight = 18.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Казань" },
            new() { Id = 11, Driver = Drivers[2], Vehicle = Vehicles[9],  Client = Clients[1],  DepartureDate = new DateOnly(2026, 2, 15), Cost =  45_000m, CargoWeight = 14.0, Status = TripStatus.Completed,  DeparturePoint = "Калуга",          DestinationPoint = "Москва" },
            new() { Id = 12, Driver = Drivers[4], Vehicle = Vehicles[7],  Client = Clients[4],  DepartureDate = new DateOnly(2026, 2, 16), Cost =  15_000m, CargoWeight = 4.0,  Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Курск" },
            new() { Id = 13, Driver = Drivers[2], Vehicle = Vehicles[10], Client = Clients[2],  DepartureDate = new DateOnly(2026, 2, 18), Cost =  35_000m, CargoWeight = 18.0, Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Орёл" },
            new() { Id = 14, Driver = Drivers[9], Vehicle = Vehicles[8],  Client = Clients[1],  DepartureDate = new DateOnly(2026, 2, 19), Cost =  18_000m, CargoWeight = 1.2,  Status = TripStatus.Completed,  DeparturePoint = "Москва",          DestinationPoint = "Владимир" },
            new() { Id = 15, Driver = Drivers[8], Vehicle = Vehicles[5],  Client = Clients[0],  DepartureDate = new DateOnly(2026, 2, 20), Cost =  25_000m, CargoWeight = 15.0, Status = TripStatus.Completed,  DeparturePoint = "Пермь",           DestinationPoint = "Москва" },
            new() { Id = 16, Driver = Drivers[4], Vehicle = Vehicles[7],  Client = Clients[8],  DepartureDate = new DateOnly(2026, 2, 22), Cost =  12_000m, CargoWeight = 3.5,  Status = TripStatus.Completed,  DeparturePoint = "Курск",           DestinationPoint = "Тула" },
            new() { Id = 17, Driver = Drivers[5], Vehicle = Vehicles[4],  Client = Clients[3],  DepartureDate = new DateOnly(2026, 2, 23), Cost =  40_000m, CargoWeight = 20.0, Status = TripStatus.Completed,  DeparturePoint = "Уфа",             DestinationPoint = "Москва" },
            new() { Id = 18, Driver = Drivers[1], Vehicle = Vehicles[6],  Client = Clients[0],  DepartureDate = new DateOnly(2026, 2, 24), Cost = 130_000m, CargoWeight = 19.0, Status = TripStatus.InProgress, DeparturePoint = "Казань",          DestinationPoint = "Москва" },
            new() { Id = 19, Driver = Drivers[3], Vehicle = Vehicles[3],  Client = Clients[9],  DepartureDate = new DateOnly(2026, 2, 24), Cost =  55_000m, CargoWeight = 15.0, Status = TripStatus.InProgress, DeparturePoint = "Курск",           DestinationPoint = "Белгород" },
            new() { Id = 20, Driver = Drivers[6], Vehicle = Vehicles[7],  Client = Clients[6],  DepartureDate = new DateOnly(2026, 2, 25), Cost =  20_000m, CargoWeight = 4.5,  Status = TripStatus.Completed,  DeparturePoint = "Тула",            DestinationPoint = "Москва" },
            new() { Id = 21, Driver = Drivers[7], Vehicle = Vehicles[0],  Client = Clients[9],  DepartureDate = new DateOnly(2026, 2, 25), Cost =  30_000m, CargoWeight = 1.2,  Status = TripStatus.InProgress, DeparturePoint = "Москва",          DestinationPoint = "Тверь" },
            new() { Id = 22, Driver = Drivers[3], Vehicle = Vehicles[10], Client = Clients[6],  DepartureDate = new DateOnly(2026, 3, 1),  Cost =  70_000m, CargoWeight = 16.0, Status = TripStatus.Planned,    DeparturePoint = "Орёл",            DestinationPoint = "Сочи" }
        ];
    }
}