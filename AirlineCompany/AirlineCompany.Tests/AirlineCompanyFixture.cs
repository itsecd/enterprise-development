using AirlineCompany.Domain;

namespace AirlineCompany.Tests;
/// <summary>
/// Класс предоставляет доступ к тестовым данным
/// </summary>
public class AirlineCompanyFixture
{
    public List<AirFlight> AirFlights;
    public List<Passeneger> Passengers;
    public List<Plane> Planes;

    public AirlineCompanyFixture()
    {
        AirFlights = FileRreader.ReadAirFlights("Data/airflyights.csv");

        Passengers = FileRreader.ReadPassengers("Data/passengers.csv");

        Planes = FileRreader.ReadPlanes("Data/planes.csv");
    }
}
