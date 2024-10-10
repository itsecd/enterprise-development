using System;
namespace TaxiDetailsClass.Domain;
/// <summary>
/// class Travel about info of trips
/// </summary>
public class Travel(int id, string departurePoint, string destinationPoint, DateTime tripDate, TimeSpan travelTime, decimal cost, Car assignedCar, User client)
{
    /// <value>Identivicator</value>
    public int Id { get; set; } = id;
    /// <value>From</value>
    public string DeparturePoint { get; set; } = departurePoint;
    /// <value>To</value>
    public string DestinationPoint { get; set; } = destinationPoint;
    /// <value>Date of trip</value>
    public DateTime TripDate { get; set; } = tripDate;
    /// <value>Time of travel</value>
    public TimeSpan TravelTime { get; set; } = travelTime;
    /// <value>Cost</value>
    public decimal Cost { get; set; } = cost;
    /// <value>Relationship with car</value>
    public Car AssignedCar { get; set; } = assignedCar;
    /// <value>Relationship with user</value>
    public User Client { get; set; } = client;
}