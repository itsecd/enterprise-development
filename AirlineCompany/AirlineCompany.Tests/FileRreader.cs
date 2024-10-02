using AirlineCompany.Domain;
using System.Globalization;

namespace AirlineCompany.Tests;

/// <summary>
/// Читает данные из файлов
/// </summary>
/// <param name="fileName"></param>
internal class FileRreader(string fileName): IDisposable
{
    private StreamReader _reader = new(fileName);
    public List<AirFlight> ReadAirFlights()
    {
        var airFlights = new List<AirFlight>();
        ushort flyId = 0;

        while(!_reader.EndOfStream)
        {
            var airLine = _reader.ReadLine();
            if (airLine == null || !airLine.Contains('"')) continue;

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

    public List<Passeneger> ReadPassengers()
    {
        var passengers = new List<Passeneger>();
        ushort passengerId = 0;

        while (!_reader.EndOfStream)
        {
            var passengerline = _reader.ReadLine();
            if (passengerline == null || !passengerline.Contains('"')) continue;

            var tokens = passengerline.Split(';');

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var passenger = new Passeneger
            {
                IdPassenger = passengerId++,
                FullName = tokens[0],
                Passport = tokens[1],
                Registration = bool.Parse(tokens[2]),
                TicketNumber = tokens[3],
                SeatNumber = tokens[4],
                BaggageWeight = double.Parse(tokens[5], CultureInfo.InvariantCulture.NumberFormat),
                CodeFlight = tokens[6]
            };

            passengers.Add(passenger);
        }

        return passengers;
    }

    //
    public List<Plane> ReadPlanes()
    {
        var planes = new List<Plane>();
        ushort planeId = 0;

        while (!_reader.EndOfStream)
        {
            var planeline = _reader.ReadLine();
            if (planeline == null || !planeline.Contains('"')) continue;

            var tokens = planeline.Split(';');

            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var plane = new Plane
            {
                IdPlane = planeId++,
                Model = tokens[0],
                LoadCapacity = double.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat),
                Efficiency = double.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat),
                PassengerMax = ushort.Parse(tokens[3], CultureInfo.InvariantCulture.NumberFormat),
            };

            planes.Add(plane);
        }

        return planes;
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}
