using System;
using System.Collections.Generic;

namespace AirCompany.Domain.Test;

public class TestDataProvider
{
    public List<Aircraft> aircrafts =
    [
        new(1, "Boeing 737", 10000m, 850m, 180),
        new(2, "Airbus A320", 12000m, 900m, 180),
        new(3, "Boeing 777", 15000m, 950m, 300),
        new(4, "Airbus A380", 20000m, 900m, 500),
        new(5, "Embraer E195", 8000m, 800m, 120)
    ];

    public List<Passenger> passengers =
    [
        new(1, "ABC123456", "John Doe"),
        new(2, "DEF789012", "Jane Smith"),
        new(3, "GHI345678", "Alice Johnson"),
        new(4, "JKL901234", "Bob Brown"),
        new(5, "MNO567890", "Charlie Davis")
    ];

    public List<Flight> flights;
    public List<RegisteredPassenger> registeredPassengers;

    public TestDataProvider()
    {
        flights = new List<Flight>
        {
            new(1, "FL123", "New York", "Los Angeles", DateTime.Now.AddHours(1), DateTime.Now.AddHours(5), TimeSpan.FromHours(1), TimeSpan.FromHours(4), aircrafts[0], new List<Passenger> { passengers[0], passengers[1] }),
            new(2, "FL456", "Chicago", "Miami", DateTime.Now.AddHours(2), DateTime.Now.AddHours(6), TimeSpan.FromHours(2), TimeSpan.FromHours(4), aircrafts[1], new List<Passenger> { passengers[2] }),
            new(3, "FL789", "San Francisco", "Seattle", DateTime.Now.AddHours(3), DateTime.Now.AddHours(7), TimeSpan.FromHours(3), TimeSpan.FromHours(4), aircrafts[2], new List<Passenger> { passengers[3], passengers[4] }),
            new(4, "FL101", "Dallas", "Austin", DateTime.Now.AddHours(4), DateTime.Now.AddHours(8), TimeSpan.FromHours(1), TimeSpan.FromHours(4), aircrafts[3], new List<Passenger> { passengers[0], passengers[1], passengers[2] }),
            new(5, "FL202", "Boston", "Newark", DateTime.Now.AddHours(5), DateTime.Now.AddHours(9), TimeSpan.FromHours(1), TimeSpan.FromHours(4), aircrafts[4], new List<Passenger> { passengers[3] }),
        };

        registeredPassengers = new List<RegisteredPassenger>
        {
            new(1, "RP001", "12A", 20m, flights[0], passengers[0]),
            new(2, "RP002", "12B", 15m, flights[0], passengers[1]),
            new(3, "RP003", "14A", 25m, flights[1], passengers[2]),
            new(4, "RP004", "18C", 30m, flights[2], passengers[3]),
            new(5, "RP005", "22D", 10m, flights[3], passengers[4]),
            new(6, "RP006", "10A", 5m, flights[4], passengers[3]),
        };
    }
}