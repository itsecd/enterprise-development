using TransportCompany.Domain.Data;
using TransportCompany.Domain.Models;
using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Tests;

public class TransportTests : IClassFixture<TransportTestData>
{
    private readonly TransportTestData _data;

    public TransportTests(TransportTestData data)
    {
        _data = data;
    }

    /// <summary>
    /// 1. Вывести информацию обо всех клиентах, заказывавших перевозку
    /// на транспорте указанной модели, упорядочить по ФИО
    /// </summary>
    [Theory]
    [InlineData(1, new[] { 10 })]
    [InlineData(2, new[] { 2, 3, 1 })]
    [InlineData(3, new[] { 9, 4, 10 })]
    [InlineData(7, new[] { 7, 9, 5 })]
    [InlineData(99, new int[0])]
    public void GetClientsByVehicleModel(int targetModelId, int[] expectedClientIds)
    {
        var clients = _data.Trips
            .Where(trip => trip.Vehicle.Model.Id == targetModelId)
            .Select(trip => trip.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.Type)
            .ThenBy(client => client.DisplayName)
            .ToArray();

        Assert.Equal(expectedClientIds, clients.Select(client => client.Id));
    }

    /// <summary>
    /// 2. Вывести информацию о транспортных средствах, находящихся в рейсе
    /// </summary>
    [Fact]
    public void GetVehiclesOnTrip()
    {
        var expectedVehicleIds = new[] { 1, 4, 7 };

        var vehicles = _data.Trips
            .Where(trip => trip.IsActive)
            .Select(trip => trip.Vehicle)
            .DistinctBy(vehicle => vehicle.Id)
            .OrderBy(vehicle => vehicle.Id)
            .ToArray();

        Assert.Equal(expectedVehicleIds, vehicles.Select(vehicle => vehicle.Id));
    }

    /// <summary>
    /// 3. Вывести топ 5 водителей по количеству рейсов
    /// </summary>
    [Fact]
    public void GetTop5DriversByTripCount()
    {
        var expected = new (int DriverId, int TripCount)[]
        {
            (1, 5),
            (2, 4),
            (3, 3),
            (4, 3),
            (5, 2)
        };

        var result = _data.Trips
            .GroupBy(trip => trip.Driver.Id)
            .Select(group => (DriverId: group.Key, TripCount: group.Count()))
            .OrderByDescending(x => x.TripCount)
            .ThenBy(x => x.DriverId)
            .Take(5)
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// 4. Для каждого транспортного средства вывести число рейсов
    /// </summary>
    [Fact]
    public void GetTripCountForEachVehicle()
    {
        var expected = new (int VehicleId, int TripCount)[]
        {
            (1, 1), (2, 0), (3, 3), (4, 4), (5, 2), (6, 2),
            (7, 2), (8, 3), (9, 1), (10, 2), (11, 2), (12, 0)
        };

        var result = _data.Vehicles
            .GroupJoin(
                _data.Trips,
                vehicle => vehicle.Id,
                trip => trip.Vehicle.Id,
                (vehicle, trips) => (VehicleId: vehicle.Id, TripCount: trips.Count()))
            .OrderBy(x => x.VehicleId)
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// 5. Вывести топ 5 клиентов по сумме затрат на перевозки
    /// </summary>
    [Fact]
    public void GetTop5ClientsByTotalCost()
    {
        var expected = new (int ClientId, decimal TotalCost)[]
        {
            (1, 325_000m),
            (9, 262_000m),
            (4, 190_000m),
            (6, 180_000m),
            (2, 153_000m)
        };

        var result = _data.Trips
            .GroupBy(trip => trip.Client.Id)
            .Select(group => (ClientId: group.Key, TotalCost: group.Sum(trip => trip.Cost)))
            .OrderByDescending(x => x.TotalCost)
            .ThenBy(x => x.ClientId)
            .Take(5)
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверка корректности формирования отображаемого имени клиента
    /// </summary>
    [Theory]
    [InlineData(ClientType.LegalPerson, "Ромашка", "ООО", "ООО «Ромашка»")]
    [InlineData(ClientType.LegalPerson, "ТехноСервис", "АО", "АО «ТехноСервис»")]
    [InlineData(ClientType.IndividualEntrepreneur, "Иванов И.И.", null, "ИП Иванов И.И.")]
    [InlineData(ClientType.PhysicalPerson, "Алексеев А.А.", null, "Алексеев А.А.")]
    public void ClientDisplayName_FormattedCorrectly(
        ClientType type, string name, string? legalForm, string expectedDisplayName)
    {
        var client = new Client
        {
            Id = 999,
            Name = name,
            Phone = "+7 000 000-00-00",
            Type = type,
            LegalForm = legalForm
        };

        Assert.Equal(expectedDisplayName, client.DisplayName);
    }

    /// <summary>
    /// Проверка категории водительских прав водителя
    ///  на соответствие категории управляемого автомобиля
    /// </summary>
    [Fact]
    public void AllTrips_DriverHasRequiredLicenseCategory()
    {
        var invalidAssignments = _data.Trips
            .Where(trip => trip.Driver.LicenseCategory != trip.Vehicle.Model.RequiredLicenseCategory)
            .Select(trip => new { trip.Id, Driver = trip.Driver.LastName, Vehicle = trip.Vehicle.Model.Name })
            .ToArray();

        Assert.Empty(invalidAssignments);
    }
}