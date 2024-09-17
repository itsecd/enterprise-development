using System.Security.Cryptography.X509Certificates;

namespace AirlineCompany.Domain;
public class AirFlight
{
    private string _codeNumber;
    public string DeparturePoint; //*отправление*
    public string ArrivalPoint; //*прибытие*
    public DateTime Departure;
    public DateTime Arrive;
    public string FlyingTime;
    public string PlaneType; // изменить на Plane

    public AirFlight(string codeNumber, string departurePoint, string arrivalPoint, DateTime departure, 
        DateTime arrive, string flyingTime, string planeType)
    {
        _codeNumber = codeNumber; //можно задать рандомом
        DeparturePoint = departurePoint;
        ArrivalPoint = arrivalPoint;
        Departure = departure;
        Arrive = arrive;
        FlyingTime = flyingTime;
        PlaneType = planeType;
    }

    public string CodeNumber => _codeNumber;
}

