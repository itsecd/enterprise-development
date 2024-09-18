namespace CarRental.Domain.Test;

/// <summary>
/// Класс содержащий unit-тесты для тестирования различных операций связанных с
/// </summary>
/// <param name="_testDataProvider">Провайдер данных для тестов.</param>
public class CarRentalTest(TestDataProvider testDataProvider) : IClassFixture<TestDataProvider>
{
    private readonly TestDataProvider _testDataProvider = testDataProvider;

    /// <summary>
    /// Проверка возврата общего числа автомобилей
    /// </summary>
    [Fact]
    public void ReturnAllCars()
    {
        var allVehicles = _testDataProvider.vehicles.ToList();
        Assert.Equal(7, allVehicles.Count);
    }

    /// <summary>
    /// Проверка возврата клиентов, арендовавших автомобиль определённой модели
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
    /// Проверка возврата автомобилей находящихся в данный момент в аренде
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
    /// Проверка возврата топ-5 автомобилей по количеству аренд
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
    /// Проверка возврата количества аренд для каждого автомобиля
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
    /// Проверка возврата пунктов проката по количеству аренд
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
