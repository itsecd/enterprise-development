using AirlineCompany.Domain;
using System.Globalization;

namespace AirlineCompany.Tests;
/// <summary>
/// Класс предоставляет доступ к тестовым данным
/// </summary>
public class AirlineCompanyFixture
{

    private List<AirFlight>? _airFlights;
    private List<Passeneger>? _passengers;
    private List<Plane>? _planes;

    public List<AirFlight> GetFlights()
    {
        if (_airFlights != null) return _airFlights;

        using var readerFlight = new FileRreader("airflyights.csv");
        _airFlights = readerFlight.ReadAirFlights();
        return _airFlights;
    }

    public List<Passeneger> GetPassenegers()
    {
        if (_passengers != null) return _passengers;

        using var readerFlight = new FileRreader("passengers.csv");
        _passengers = readerFlight.ReadPassengers();
        return _passengers;
    }


    public List<Plane> GetPlanes()
    {
        if (_planes != null) return _planes;

        using var readerFlight = new FileRreader("planes.csv");
        _planes = readerFlight.ReadPlanes();
        return _planes;
    }
}

