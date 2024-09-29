using AirlineCompany.Domain;
using System.Globalization;

namespace AirlineCompany.Tests;

public class AirlineCompanyFixture
{
    public List<AirFlight> GetFlights()
    {
        var airReader = File.ReadAllLines("airflyights.csv");
        var airFlights = new List<AirFlight>();
        ushort flyId = 0;

        foreach (var airLine in airReader)
        {
            if (!airLine.Contains('"')) continue;

            var tokens = airLine.Split(';');

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var flight = new AirFlight
            {
                Idflight = flyId++,
                CodeNumber = tokens[0],
                DeparturePoint = tokens[1],
                ArrivalPoint = tokens[2],
                Departure = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:m:s", CultureInfo.InvariantCulture),
                Arrive = DateTime.Parse(tokens[4]),
                FlyingTime = TimeOnly.Parse(tokens[5]),
                PlaneType = tokens[6]
            };

            airFlights.Add(flight);
        }

        return airFlights;
    }

    public List<Passeneger> GetPassenegers()
    {
        var passengerReader = File.ReadAllLines("passengers.csv");
        var passengers = new List<Passeneger>();
        ushort passengerId = 0;

        foreach (var passengerline in passengerReader)
        {
            if (!passengerline.Contains('"')) continue;

            var tokens = passengerline.Split(';');

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var passenger = new Passeneger
            {
                IdPassenger = passengerId++,
                FIO = tokens[0],
                Passport = tokens[1],
                registration = bool.Parse(tokens[2]),
                ticketNumber = tokens[3],
                seatNumber = tokens[4],
                baggageWeight = float.Parse(tokens[5], CultureInfo.InvariantCulture.NumberFormat),
                codeFlight = tokens[6]
            };

            passengers.Add(passenger);
        }

        return passengers;
    }

    public List<Plane> GetPlanes()
    {
        var planeReader = File.ReadAllLines("planes.csv");
        var planes = new List<Plane>();
        ushort planeId = 0;

        foreach (var planeline in planeReader)
        {
            if (!planeline.Contains('"')) continue;

            var tokens = planeline.Split(';');

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var plane = new Plane
            {
                IdPlane = planeId++,
                Model = tokens[0],
                LoadCapacity = float.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat),
                Efficiency = float.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat),
                PassengerMax = ushort.Parse(tokens[3], CultureInfo.InvariantCulture.NumberFormat),
            };

            planes.Add(plane);
        }

        return planes;
    }
}

