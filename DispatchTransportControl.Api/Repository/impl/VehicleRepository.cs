using DispatchTransportControl.Api.Repository.Context;
using DispatchTransportControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace DispatchTransportControl.Api.Repository.impl;

/// <summary>
/// Класс реализует интерфейс <see cref="IVehicleRepository"/>
/// </summary>
public class VehicleRepository(TransportDbContext context) : IVehicleRepository
{
    /// <inheritdoc />
    public IEnumerable<Vehicle> GetAll()
    {
        return context.Vehicles;
    }

    /// <inheritdoc />
    public Vehicle? GetById(int id)
    {
        return context.Vehicles.Find(id);
    }

    /// <inheritdoc />
    public Vehicle Create(Vehicle entity)
    {
        var res = context.Vehicles.Add(entity).Entity;
        context.SaveChanges();
        return res;
    }

    /// <inheritdoc />
    public Vehicle Update(Vehicle entity)
    {
        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public void Delete(Vehicle entity)
    {
        context.Vehicles.Remove(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public Dictionary<(VehicleType VehicleType, VehicleModel VehicleModel), double>
        GetTotalTripTimeForEveryVehicleTypeAndModel()
    {
        return context.RouteAssignments
            .GroupBy(ra => new { ra.Vehicle.VehicleType, ra.Vehicle.VehicleModel.Id })
            .ToDictionary
            (
                group => (
                    group.Key.VehicleType,
                    group.Select(ra => ra.Vehicle.VehicleModel).First()
                    ),
                group => group.Sum(ra => (ra.EndTime - ra.StartTime).TotalHours)
            );
    }

    /// <inheritdoc />
    public IEnumerable<(Vehicle Vehicle, int TripCount)> GetVehiclesWithMaxTripsForPeriod(DateTime start, DateTime end)
    {
        var vehiclesTripCount = context.RouteAssignments
            .Where(ra => ra.StartTime >= start && ra.EndTime <= end)
            .GroupBy(ra => ra.Vehicle)
            .Select(group => new { Vehicle = group.Key, TripCount = group.Count() })
            .ToList();

        if (vehiclesTripCount.Count == 0)
        {
            return new List<(Vehicle, int)>();
        }

        var maxTripCount = vehiclesTripCount.Max(g => g.TripCount);

        return vehiclesTripCount
            .Where(g => g.TripCount == maxTripCount)
            .Select(g => (g.Vehicle, g.TripCount));
    }
}