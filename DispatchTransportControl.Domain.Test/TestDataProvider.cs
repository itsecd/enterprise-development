using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Domain.Test;

public class TestDataProvider
{
    public List<Driver> Drivers =
    [
        new()
        {
            Id = 1,
            Name = "driverName1",
            Surname = "driverSurname1",
            Patronymic = "driverPatronymic1",
            Passport = "passport1",
            DriverLicense = "driverLicense1",
            Address = "address1",
            Phone = "phone1"
        },
        new()
        {
            Id = 2,
            Name = "driverName2",
            Surname = "driverSurname2",
            Patronymic = "driverPatronymic2",
            Passport = "passport2",
            DriverLicense = "driverLicense2",
            Address = "address2",
            Phone = "phone2"
        },
        new()
        {
            Id = 3,
            Name = "driverName3",
            Surname = "driverSurname3",
            Patronymic = "driverPatronymic3",
            Passport = "passport3",
            DriverLicense = "driverLicense3",
            Address = "address3",
            Phone = "phone3"
        },
        new()
        {
            Id = 4,
            Name = "driverName4",
            Surname = "driverSurname4",
            Patronymic = "driverPatronymic4",
            Passport = "passport4",
            DriverLicense = "driverLicense4",
            Address = "address4",
            Phone = "phone4"
        },
        new()
        {
            Id = 5,
            Name = "driverName5",
            Surname = "driverSurname5",
            Patronymic = "driverPatronymic5",
            Passport = "passport5",
            DriverLicense = "driverLicense5",
            Address = "address5",
            Phone = "phone5"
        }
    ];

    public List<RouteAssignment> RouteAssignments;

    public List<VehicleModel> VehicleModels =
    [
        new() { Id = 1, Name = "vehicleName1", LowFloor = true, MaxCapacity = 10 },
        new() { Id = 2, Name = "vehicleName2", LowFloor = false, MaxCapacity = 20 },
        new() { Id = 3, Name = "vehicleName3", LowFloor = false, MaxCapacity = 30 },
        new() { Id = 4, Name = "vehicleName4", LowFloor = true, MaxCapacity = 12 },
        new() { Id = 5, Name = "vehicleName5", LowFloor = true, MaxCapacity = 15 }
    ];

    public List<Vehicle> Vehicles;

    public TestDataProvider()
    {
        Vehicles =
        [
            new Vehicle
            {
                Id = 1,
                RegistrationNumber = "registrationNumber1",
                VehicleType = VehicleType.Bus,
                VehicleModel = VehicleModels[0],
                YearOfManufacture = 1993
            },
            new Vehicle
            {
                Id = 2,
                RegistrationNumber = "registrationNumber2",
                VehicleType = VehicleType.Bus,
                VehicleModel = VehicleModels[2],
                YearOfManufacture = 1999
            },
            new Vehicle
            {
                Id = 3,
                RegistrationNumber = "registrationNumber3",
                VehicleType = VehicleType.Trolley,
                VehicleModel = VehicleModels[1],
                YearOfManufacture = 2005
            },
            new Vehicle
            {
                Id = 4,
                RegistrationNumber = "registrationNumber4",
                VehicleType = VehicleType.Tram,
                VehicleModel = VehicleModels[4],
                YearOfManufacture = 2007
            },
            new Vehicle
            {
                Id = 5,
                RegistrationNumber = "registrationNumber5",
                VehicleType = VehicleType.Bus,
                VehicleModel = VehicleModels[3],
                YearOfManufacture = 1823
            }
        ];

        RouteAssignments =
        [
            new RouteAssignment
            {
                Id = 1,
                Vehicle = Vehicles[0],
                Driver = Drivers[0],
                RouteNumber = "routeNumber1",
                StartTime = new DateTime(2024, 9, 20, 15, 0, 0),
                EndTime = new DateTime(2024, 9, 20, 16, 0, 0)
            },
            new RouteAssignment
            {
                Id = 2,
                Vehicle = Vehicles[1],
                Driver = Drivers[2],
                RouteNumber = "routeNumber2",
                StartTime = new DateTime(2024, 9, 20, 16, 0, 0),
                EndTime = new DateTime(2024, 9, 20, 18, 0, 0)
            },
            new RouteAssignment
            {
                Id = 3,
                Vehicle = Vehicles[2],
                Driver = Drivers[1],
                RouteNumber = "routeNumber3",
                StartTime = new DateTime(2024, 9, 20, 16, 0, 0),
                EndTime = new DateTime(2024, 9, 20, 17, 0, 0)
            },
            new RouteAssignment
            {
                Id = 4,
                Vehicle = Vehicles[3],
                Driver = Drivers[4],
                RouteNumber = "routeNumber4",
                StartTime = new DateTime(2024, 9, 20, 18, 0, 0),
                EndTime = new DateTime(2024, 9, 20, 20, 0, 0)
            },
            new RouteAssignment
            {
                Id = 5,
                Vehicle = Vehicles[4],
                Driver = Drivers[3],
                RouteNumber = "routeNumber5",
                StartTime = new DateTime(2024, 9, 20, 18, 0, 0),
                EndTime = new DateTime(2024, 9, 20, 19, 0, 0)
            },
            new RouteAssignment
            {
                Id = 6,
                Vehicle = Vehicles[4],
                Driver = Drivers[3],
                RouteNumber = "routeNumber6",
                StartTime = new DateTime(2024, 9, 20, 22, 0, 0),
                EndTime = new DateTime(2024, 9, 21, 00, 0, 0)
            }
        ];
    }
}