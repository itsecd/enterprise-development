using AirlineCompany.Domain;

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

        using var readerPassenger = new FileRreader("passengers.csv");
        _passengers = readerPassenger.ReadPassengers();
        return _passengers;
    }


    public List<Plane> GetPlanes()
    {
        if (_planes != null) return _planes;

        using var readerPlane = new FileRreader("planes.csv");
        _planes = readerPlane.ReadPlanes();
        return _planes;
    }
}

