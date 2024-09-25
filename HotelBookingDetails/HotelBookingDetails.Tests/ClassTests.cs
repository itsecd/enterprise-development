using HotelBookingDetails;
using HotelBookingDetails.Domain;

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
            _dataProvider.Clients[8]
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
}
