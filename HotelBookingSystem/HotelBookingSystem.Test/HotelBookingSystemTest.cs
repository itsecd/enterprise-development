namespace HotelBookingSystem.Domain.Test;

/// <summary>
/// Тесты
/// </summary>
public class HotelBookingSystemTest(TestData testData) : IClassFixture<TestData>
{

    private readonly TestData _testData = testData;
    
    /// <summary>
    /// Информация о всех отелях
    /// </summary>
    [Fact]
    public void InfoAllHotels()
    {
        var result = _testData.Hotels;

        Assert.Equal("Альфа", result[0].Name);
        Assert.Equal("Браво", result[1].Name);
        Assert.Equal("Чарли", result[2].Name);
        Assert.Equal("Дельта", result[3].Name);
        Assert.Equal("Эхо", result[4].Name);
        Assert.Equal("Фокстрот", result[5].Name);
        Assert.Equal("Голф", result[6].Name);
    }

    /// <summary>
    /// Все клиенты в указанном отеле, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void InfoClientsInHotels()
    {
        var hotelId = _testData.Hotels.FirstOrDefault(hotel => hotel.Name == "Альфа")?.Id;

        var hotelClients = _testData.HotelClients
            .Where(client => client.Brooms
                .Any(broom => _testData.Rooms
                    .Where(room => room.Id == broom.RoomId && room.HotelID == hotelId)
                    .Any()))
            .OrderBy(client => client.Surname)
            .ThenBy(client => client.Name)
            .ThenBy(client => client.Patronymic)
            .ToList();

        Assert.True(hotelClients.Any());
        Assert.Equal("Сидоров", hotelClients[0].Surname);
        Assert.Equal("Щербакова", hotelClients[1].Surname);
    }

    /// <summary>
    /// Топ 5 отелей с большим числом бронирования
    /// </summary>
    [Fact]
    public void TopHotels()
    {
        var hotelBookingCounts = _testData.Hotels
            .Select(hotel => new
            {
                Hotel = hotel,
                BookingCount = _testData.Rooms
                    .Where(room => room.HotelID == hotel.Id)
                    .SelectMany(room => room.BookedRooms)
                    .Count()
            })
            .OrderByDescending(hotel => hotel.BookingCount)
            .Take(5)
            .ToList();

        Assert.Equal("Альфа", hotelBookingCounts[0].Hotel.Name);
        Assert.Equal("Браво", hotelBookingCounts[1].Hotel.Name);
    }

    /// <summary>
    /// Вывести информацию о свободных номерах во всех отелях выбранного города 
    /// (сколько типов номеров свободно и сколько всего свободно номеров)
    /// </summary>
    [Fact]
    public void FreeRoomsInCity()
    {
        var freeRooms = _testData.Hotels
            .Where(hotel => hotel.City == "Москва") 
            .Select(hotel => new
            {
                HotelName = hotel.Name,
                Rooms = _testData.Rooms
                    .Where(room => room.HotelID == hotel.Id)
                    .GroupBy(room => room.TypeRoom)
                    .Select(group => new
                    {
                        RoomType = group.Key,
                        TotalRooms = group.Sum(r => r.Number), // все номера
                        BookedRooms = group.Sum(r => r.BookedRooms.Count), // забронированные
                        AvailableRooms = group.Sum(r => r.Number) - group.Sum(r => r.BookedRooms.Count) // свободные
                    })
                    .Where(r => r.AvailableRooms > 0) // фильтрация если есть свободные
                    .ToList()
            })
            .ToList();

        Assert.True(freeRooms.Any());
        Assert.Equal("Альфа", freeRooms[0].HotelName);
        Assert.Equal(2, freeRooms[0].Rooms.Count()); // типы свободных номеров
        var availableRooms = freeRooms[0].Rooms.Select(r => r.AvailableRooms).ToList();
        Assert.Equal(15, availableRooms.Sum()); //количество свободных
    }

    /// <summary>
    /// Вывести информацию о клиентах, арендовавших номера на наибольшее количество дней
    /// </summary>
    [Fact]
    public void ClientsLongestRentalPeriod()
    {
        var maxRentalPeriod = _testData.BookedRooms
            .Max(broom => broom.BookingPeriod.Days);

        var clientsWithLongestRental = _testData.BookedRooms
            .Where(broom => broom.BookingPeriod.Days == maxRentalPeriod)
            .Select(broom => new
            {
                Client = _testData.HotelClients[broom.ClientId],
                RentalDays = broom.BookingPeriod.Days
            })
            .ToList();

        Assert.True(clientsWithLongestRental.Any());
        Assert.Equal(maxRentalPeriod, clientsWithLongestRental[0].RentalDays);
        Assert.Equal(1, clientsWithLongestRental[0].Client.Id);
        Assert.Equal("Морозов", clientsWithLongestRental[1].Client.Surname);
    }

    /// <summary>
    /// Вывести информацию о минимальной, средней и максимальной стоимости номера в каждом отеле
    /// </summary>
    [Fact]
    public void RoomPriceStatistics()
    {
        var roomPricingStats = _testData.Hotels
            .Select(hotel => new
            {
                HotelName = hotel.Name,
                MinPrice = _testData.Rooms
                    .Where(room => room.HotelID == hotel.Id)
                    .Min(room => room.Price), 
                AvgPrice = _testData.Rooms
                    .Where(room => room.HotelID == hotel.Id)
                    .Average(room => room.Price), 
                MaxPrice = _testData.Rooms
                    .Where(room => room.HotelID == hotel.Id)
                    .Max(room => room.Price) 
            })
            .ToList();

        Assert.Equal(1000, roomPricingStats[0].MinPrice);
        Assert.Equal(1900, roomPricingStats[0].MaxPrice);
        Assert.Equal(1450, roomPricingStats[0].AvgPrice);
        Assert.Equal(2600, roomPricingStats[4].MinPrice);
        Assert.Equal(2700, roomPricingStats[4].MaxPrice);
        Assert.Equal(2650, roomPricingStats[4].AvgPrice);
    }
}