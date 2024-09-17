namespace CarRental.Domain.Test;

/// <summary>
/// ����� ���������� unit-����� ��� ������������ ��������� �������� ��������� �
/// </summary>
/// <param name="_testDataProvider">��������� ������ ��� ������.</param>
public class CarRentalTest(TestDataProvider testDataProvider) : IClassFixture<TestDataProvider>
{
    private readonly TestDataProvider _testDataProvider = testDataProvider;

    /// <summary>
    /// �������� �������� ������ ����� �����������
    /// </summary>
    [Fact]
    public void ReturnAllCars()
    {
        var allVehicles = _testDataProvider.vehicles.ToList();
        Assert.Equal(7, allVehicles.Count);
    }

    /// <summary>
    /// �������� �������� ��������, ������������ ���������� ����������� ������
    /// </summary>
    [Fact]
    public void ClientsWhoRentedCarOfTheSpecialModel()
    {
        string modelToSearch = "Model 1";
        var clientsWhoOrderedCurrentModel = _testDataProvider.vehiclesOnRent
            .Where(r => r.Vehicle!.Model == modelToSearch)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c!.FullName)
            .ToList();
        Assert.Equal("Full Name 2", clientsWhoOrderedCurrentModel[0]!.FullName);
    }

    /// <summary>
    /// �������� �������� ����������� ����������� � ������ ������ � ������
    /// </summary>
    [Fact]
    public void CarsOnRentRightNow()
    {
        var carsOnRentRightNow = _testDataProvider.vehiclesOnRent
            .Where(r => r.ReturnTime == null)
            .ToList();
        Assert.Equal(_testDataProvider.vehicles[6], carsOnRentRightNow[0].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[5], carsOnRentRightNow[1].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[4], carsOnRentRightNow[2].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[3], carsOnRentRightNow[3].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[2], carsOnRentRightNow[4].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[1], carsOnRentRightNow[5].Vehicle);
        Assert.Equal(_testDataProvider.vehicles[0], carsOnRentRightNow[6].Vehicle);
    }

    /// <summary>
    /// �������� �������� ���-5 ����������� �� ���������� �����
    /// </summary>
    [Fact]
    public void Top5CarsOnRent()
    {
        var top5RentedCars = _testDataProvider.vehiclesOnRent
            .GroupBy(r => r.Vehicle)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();
        Assert.Equal(_testDataProvider.vehicles[6], top5RentedCars[0]);
        Assert.Equal(_testDataProvider.vehicles[5], top5RentedCars[1]);
        Assert.Equal(_testDataProvider.vehicles[4], top5RentedCars[2]);
        Assert.Equal(_testDataProvider.vehicles[3], top5RentedCars[3]);
        Assert.Equal(_testDataProvider.vehicles[2], top5RentedCars[4]);
    }

    /// <summary>
    /// �������� �������� ���������� ����� ��� ������� ����������
    /// </summary>
    [Fact]
    public void NumberOfRentForEachVehicle()
    {
        var vehicleRentalCounts = _testDataProvider.vehiclesOnRent
            .GroupBy(r => r.Vehicle)
            .Select(g => new
            {
                Car = g.Key,
                Count = g.Count()
            })
            .ToList();
        Assert.Equal(2, vehicleRentalCounts[0].Count);
        Assert.Equal(3, vehicleRentalCounts[1].Count);
        Assert.Equal(4, vehicleRentalCounts[2].Count);
        Assert.Equal(5, vehicleRentalCounts[3].Count);
        Assert.Equal(6, vehicleRentalCounts[4].Count);
        Assert.Equal(1, vehicleRentalCounts[5].Count);
        Assert.Equal(1, vehicleRentalCounts[6].Count);
    }

    /// <summary>
    /// �������� �������� ������� ������� �� ���������� �����
    /// </summary>
    [Fact]
    public void RentalPointsWithMostRents()
    {
        var mostFrequentRentalPoints = _testDataProvider.vehiclesOnRent
            .GroupBy(r => r.RentalPointGetFrom)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key!.Name)
            .Select(g => g.Key)
            .Take(1)
            .ToList();
        Assert.Equal(_testDataProvider.rentalPoints[2], mostFrequentRentalPoints[0]);
    }
}
