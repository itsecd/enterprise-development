using AirlineCompany.Domain;

namespace AirlineCompany.Tests;
/// <summary>
/// Класс предоставляет доступ к тестовым данным
/// </summary>
public class AirlineCompanyFixture
{

    public List<AirFlight>? AirFlights;
    public List<Passeneger>? Passengers;
    public List<Plane>? Planes;

    public List<AirFlight> GetFlights()
    {
        if (AirFlights != null) return AirFlights;

        using var readerFlight = new FileRreader("airflyights.csv");
        AirFlights = readerFlight.ReadAirFlights();
        return AirFlights;
    }

    public List<Passeneger> GetPassenegers()
    {
        if (Passengers != null) return Passengers;

        using var readerPassenger = new FileRreader("passengers.csv");
        Passengers = readerPassenger.ReadPassengers();
        return Passengers;
    }


    public List<Plane> GetPlanes()
    {
        if (Planes != null) return Planes;

        using var readerPlane = new FileRreader("planes.csv");
        Planes = readerPlane.ReadPlanes();
        return Planes;
    }
}

