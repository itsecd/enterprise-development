using System.Security.Cryptography.X509Certificates;

namespace AirlineCompany.Domain;
public class AirFlight
{
    /// <summary>
    /// Класс описывает 1 полет какого-либо самолета
    /// </summary>
    public required ushort Idflight { get; set; }
    public required string CodeNumber { get; set; }
    public required string DeparturePoint { get; set; } //*отправление*
    public required string ArrivalPoint { get; set; } //*прибытие*
    public required DateTime Departure { get; set; }
    public required DateTime Arrive { get; set; }
    public required double FlyingTime { get; set; }
    public required ushort PlaneType { get; set; }
}

