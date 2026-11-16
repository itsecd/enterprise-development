using Xunit;

namespace CarWash.Tests;

/// <summary>
/// Тесты операций автомойки
/// </summary>
public class CarWashTests(CarWashFixture fixture)
    : IClassFixture<CarWashFixture>
{
    /// <summary>
    /// Проверка пяти клиентов с наибольшим количеством посещений
    /// </summary>
    [Fact]
    public async Task GetTop5CustomersByVisits()
    {
        var result =
            await fixture.CustomerRepository.GetTop5CustomersByVisitsAsync(TestContext.Current.CancellationToken);

        var actual = result
            .Select(item => item.Customer.Id)
            .ToList();

        var expected = new[]
        {
            fixture.Data.Customers[0].Id,
            fixture.Data.Customers[1].Id,
            fixture.Data.Customers[2].Id,
            fixture.Data.Customers[3].Id,
            fixture.Data.Customers[4].Id
        };

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка автомобилей, находящихся сейчас на мойке
    /// </summary>
    [Fact]
    public async Task GetCarsCurrentlyAtWash()
    {
        var result =
            await fixture.OrderRepository.GetCarsCurrentlyAtWashAsync(TestContext.Current.CancellationToken);

        Assert.Single(result);

        Assert.Contains(
            result,
            car => car.Id == fixture.Data.Cars[0].Id);
    }

    /// <summary>
    /// Проверка пяти наиболее популярных услуг
    /// </summary>
    [Fact]
    public async Task GetTop5PopularServices()
    {
        var result =
            await fixture.WashServiceRepository.GetTop5PopularServicesAsync(TestContext.Current.CancellationToken);

        var actual = result
            .Select(item => item.WashService.Id)
            .ToList();

        var expected = new[]
        {
            1,
            2,
            3,
            4,
            5
        };

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка времени освобождения бокса
    /// </summary>
    [Fact]
    public async Task GetBoxAvailableFrom()
    {
        var result =
            await fixture.OrderRepository.GetBoxAvailableFromAsync(1, TestContext.Current.CancellationToken);

        Assert.Equal(fixture.Data.Orders[15].EndTime, result);
    }

    /// <summary>
    /// Проверка суммарной выручки по услугам
    /// </summary>
    [Fact]
    public async Task GetRevenuePerService()
    {
        var result =
            await fixture.WashServiceRepository.GetRevenuePerServiceAsync(TestContext.Current.CancellationToken);

        Assert.Contains(
            result,
            item =>
                item.WashService.Id == fixture.Data.WashServices[0].Id &&
                item.Revenue == 3000m);
    }
}