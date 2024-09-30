using HotelBookingDetails;
using HotelBookingDetails.Domain;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit.Sdk;

namespace HotelBookingDetails.Tests;

public class Test(HotelBookingDetailsData dataProvider) : IClassFixture<HotelBookingDetailsData>
{
    private readonly HotelBookingDetailsData _dataProvider = dataProvider;

    [Fact]
    public void ReturnAllHotels()
    {
        var hotel = _dataProvider.Hotels.Select(h => h.Name).ToList();
        var hotelNames = new List<String> {
            "Гостиница Москва",
            "Grand Hotel",
            "Hilton",
            "Marriott",
            "Hyatt Regency",
            "Ritz-Carlton"
        };
        Assert.Equal(hotel, hotelNames);
    }

    [Fact]
    public void ReturnAllClientInHotel()
    {
        var clients = new List<Client>
        {
            _dataProvider.Clients[9],
            _dataProvider.Clients[10],
            _dataProvider.Clients[8],
            _dataProvider.Clients[11],
        };
        var rooms_hotel = _dataProvider.Hotels
            .Where(h => h.Name == "Hilton")
            .Select(h => h.Rooms).First();
        var client_in_hotel = _dataProvider.ReservedRooms
            .OrderBy(r => r.client.FullName)
            .Where(r => rooms_hotel.Contains(r.room))
            .Select(r => r.client)
            .ToList();
        Assert.Equal(client_in_hotel, clients);
    }

    [Fact]
    void ReturnTopFiveHotel()
    {
        var hotel = new List<Hotel>
        {
            _dataProvider.Hotels[0],
            _dataProvider.Hotels[1],
            _dataProvider.Hotels[2],
            _dataProvider.Hotels[3],
            _dataProvider.Hotels[5]
        };
        var topPopularHotelId = _dataProvider.ReservedRooms
            .GroupBy(r => r.room.HotelId)
            .Select(r => r.Key)
            .Take(5)
            .ToList();
            
        var topFIveHotel = _dataProvider.Hotels
            .Where(h => topPopularHotelId.Contains(h.Id))
            .Select(h => h)
            .ToList();
        Assert.Equal(hotel, topFIveHotel);

    }

    [Fact]
    void ReturnFreeRooms()
    {
        var rooms = new List<Room>
        {
            _dataProvider.Rooms[4],
            _dataProvider.Rooms[5],
            _dataProvider.Rooms[6],

        };

        var roomInCity = _dataProvider.Rooms
            .Where(r => (_dataProvider.Hotels.Where(h => h.City == "New York").Select(h => h.Id).ToList()).Contains(r.HotelId))
            .Select(h => h).ToList();

        var reservedRooms = _dataProvider.ReservedRooms
            .Where(r => r.DateDeparture == DateOnly.ParseExact("0001-01-01", "yyyy-mm-dd"))
            .Select(r => r).ToList();

        var freeRooms = _dataProvider.ReservedRooms
            .Where(r => !reservedRooms.Contains(r) && roomInCity.Contains(r.room))
            .Select(r => r.room).ToList();

        Assert.Equal(rooms, freeRooms);
    }

    [Fact]
    void returnLongLiversHotel()
    {

        var clients = new List<Client>
        {
            _dataProvider.Clients[14],
            _dataProvider.Clients[15],
     
        };

        var longerPeriods = (_dataProvider.ReservedRooms
            .GroupBy(c => c.client)
            .Select(c => new {
                client = c.Key,
                total = c.Sum(r => r.Period)
                })).Select(c => c).Max(c => c.total);

        var clientWithLongerPer = (_dataProvider.ReservedRooms
            .GroupBy(c => c.client)
            .Select(c => new
            {
                client = c.Key,
                total = c.Sum(r => r.Period)
            })).Where(c => c.total == longerPeriods).Select(c => c.client).ToList();

        Assert.Equal(clients, clientWithLongerPer);
    }

    [Fact]
    void minAvgMaxCostInHotel()
    {
        var minHotelCost = _dataProvider.Hotels
            .Select(c => new {
                hotel = c,
                min = c.Rooms.Min(r => r.Cost),
                max = c.Rooms.Max(r => r.Cost),
                avg = c.Rooms.Average(r => r.Cost)
            }).ToList();

        Assert.Equal(minHotelCost[0].min, 3000.00);
        Assert.Equal(minHotelCost[0].avg, 4500.00);
        Assert.Equal(minHotelCost[0].max, 6000.00);
    }
}
