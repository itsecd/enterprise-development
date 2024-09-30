using DispatchTransportControl.Api.Repository.Context;
using DispatchTransportControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace DispatchTransportControl.Api.Repository.impl;

/// <summary>
/// Класс реализует интерфейс <see cref="IDriverRepository"/>
/// </summary>
public class DriverRepository(TransportDbContext context) : IDriverRepository
{
    /// <inheritdoc />
    public IEnumerable<Driver> GetAll()
    {
        return context.Drivers;
    }

    /// <inheritdoc />
    public Driver? GetById(int id)
    {
        return context.Drivers.Find(id);
    }

    /// <inheritdoc />
    public Driver Create(Driver entity)
    {
        var res = context.Drivers.Add(entity).Entity;
        context.SaveChanges();
        return res;
    }

    /// <inheritdoc />
    public Driver Update(Driver entity)
    {
        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public void Delete(Driver entity)
    {
        context.Drivers.Remove(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public IEnumerable<Driver> GetAllByPeriod(DateTime start, DateTime end)
    {
        return context.RouteAssignments
            .Where(ra => start <= ra.StartTime && ra.EndTime <= end)
            .Select(ra => ra.Driver)
            .Distinct()
            .OrderBy(d => d.Surname).ThenBy(d => d.Name).ThenBy(d => d.Patronymic);
    }

    /// <inheritdoc />
    public IEnumerable<(Driver Driver, int TripCount)> GetTop5DriversByTripCount()
    {
        return context.RouteAssignments
            .GroupBy(ra => ra.Driver)
            .Select(group => new
            {
                Driver = group.Key,
                TripCount = group.Count()
            })
            .OrderByDescending(g => g.TripCount)
            .Take(5)
            .AsEnumerable()
            .Select(g => (g.Driver, g.TripCount));
    }

    /// <inheritdoc />
    public Dictionary<Driver, (int TripCount, double AvgTime, double MaxTime)> GetDriverTripStats()
    {
        return context.RouteAssignments
            .GroupBy(ra => ra.Driver)
            .ToDictionary(
                group => group.Key,
                group => (
                    TripCount: group.Count(),
                    AvgTime: group.Average(ra => (ra.EndTime - ra.StartTime).TotalHours),
                    MaxTime: group.Max(ra => (ra.EndTime - ra.StartTime).TotalHours)
                )
            );
    }
}