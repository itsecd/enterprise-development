using AirlineCompany.Domain;
using System.Globalization;

namespace AirlineCompany.Tests;

/// <summary>
/// Читает данные из файлов
/// </summary>
/// <param name="fileName"></param>
static class FileRreader
{
    public static List<AirFlight> ReadAirFlights(string filename)
    {
        using var reader = new StreamReader(filename);
        var airFlights = new List<AirFlight>();
        var flyId = 0;

        while(!reader.EndOfStream)
        {
            var airLine = reader.ReadLine();
            if (airLine == null || !airLine.Contains('"')) continue;

            var tokens = airLine.Split(';');

            tokens = tokens.Select(token => token.Trim('"')).ToArray();

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

    public static List<Passeneger> ReadPassengers(string filename)
    {
        using var reader = new StreamReader(filename);
        var passengers = new List<Passeneger>();
        var passengerId = 0;

        while (!reader.EndOfStream)
        {
            var passengerline = reader.ReadLine();
            if (passengerline == null || !passengerline.Contains('"')) continue;

            var tokens = passengerline.Split(';');

            tokens = tokens.Select(token => token.Trim('"')).ToArray();

            var passenger = new Passeneger
            {
                IdPassenger = passengerId++,
                FullName = tokens[0],
                Passport = tokens[1],
                Registration = bool.Parse(tokens[2]),
                TicketNumber = tokens[3],
                SeatNumber = tokens[4],
                BaggageWeight = double.Parse(tokens[5], CultureInfo.InvariantCulture.NumberFormat),
                IdFlight = int.Parse(tokens[6])
            };

            passengers.Add(passenger);
        }

        return passengers;
    }

    public static List<Plane> ReadPlanes(string filename)
    {
        using var reader = new StreamReader(filename);
        var planes = new List<Plane>();
        var planeId = 0;

        while (!reader.EndOfStream)
        {
            var planeline = reader.ReadLine();
            if (planeline == null || !planeline.Contains('"')) continue;

            var tokens = planeline.Split(';');

            tokens = tokens.Select(token => token.Trim('"')).ToArray();

            var plane = new Plane
            {
                IdPlane = planeId++,
                Model = tokens[0],
                LoadCapacity = double.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat),
                Efficiency = double.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat),
                PassengerMax = int.Parse(tokens[3], CultureInfo.InvariantCulture.NumberFormat),
            };

            planes.Add(plane);
        }

        return planes;
    }
}
