using DispatchTransportControl.Api.Repository.Context;
using DispatchTransportControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace DispatchTransportControl.Api.Repository.impl;

/// <summary>
/// Класс реализует интерфейс <see cref="IRouteAssignmentRepository"/>
/// </summary>
public class RouteAssignmentRepository(TransportDbContext context) : IRouteAssignmentRepository
{
    /// <inheritdoc />
    public IEnumerable<RouteAssignment> GetAll()
    {
        return context.RouteAssignments;
    }

    /// <inheritdoc />
    public RouteAssignment? GetById(int id)
    {
        return context.RouteAssignments.Find(id);
    }

    /// <inheritdoc />
    public RouteAssignment Create(RouteAssignment entity)
    {
        var res = context.RouteAssignments.Add(entity).Entity;
        context.SaveChanges();
        return res;
    }

    /// <inheritdoc />
    public RouteAssignment Update(RouteAssignment entity)
    {
        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public void Delete(RouteAssignment entity)
    {
        context.RouteAssignments.Remove(entity);
        context.SaveChanges();
    }
}