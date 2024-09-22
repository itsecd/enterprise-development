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
        var hotelClients = _testData.HotelClients.Where(client => client.Brooms.Any(broom => _testData.Rooms
                .FirstOrDefault(room => room.Id == broom.RoomId)
                .HotelID == _testData.Hotels
                .FirstOrDefault(hotel => hotel.Name == "Альфа")?.Id))
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

        var topHotels = _testData.Hotels
    .OrderByDescending(hotel => hotel.Id == _testData.Rooms
        .Where(room => room.BookedRooms.Any())
        .Select(room => room.HotelID)
        .Distinct()
        .Count(id => id == hotel.Id))
    .Take(5)
    .Select(hotel => new {
        HotelName = hotel.Name,
        BookingCount = _testData.Rooms
        .Where(room => room.HotelID == hotel.Id)
        .SelectMany(room => room.BookedRooms)
        .Count()
    })
    .ToList();/*
        var hotelBookingCounts = _testData.Rooms
            .Where(room => room.BookedRooms.Any())
            .GroupBy(room => room.HotelID)
            .ToDictionary(
                g => g.Key,
                g => g.Select(room => room.HotelID).Distinct().Count()
            );

        var topHotels = _testData.Hotels
            .OrderByDescending(hotel => hotelBookingCounts[hotel.Id])
            .Take(5)
            .Select(hotel => new { HotelName = hotel.Name, BookingCount = hotelBookingCounts[hotel.Id] })
            .ToList();*/

        Assert.Equal("Альфа", topHotels[0].HotelName);
    }
}

/*Id = 1, Name = "Альфа", City = "Москва", Address = "ул. Ленина, 1" },
        new(){ Id = 2, Name = "Браво", City = "Санкт-Петербург", Address = "Невский пр., 123" },
        new(){ Id = 3, Name = "Чарли", City = "Санкт-Петербург", Address = "ул. Невская, 45" },
        new(){ Id = 4, Name = "Дельта", City = "Самара", Address = "ул. Ленинская, 111" },
        new(){ Id = 5, Name = "Эхо", City = "Самара", Address = "ул. Панова, 12" },
        new(){ Id = 6, Name = "Фокстрот", City = "Москва", Address = "ул. Самарская, 34" },
        new(){ Id = 7, Name = "Голф",*/