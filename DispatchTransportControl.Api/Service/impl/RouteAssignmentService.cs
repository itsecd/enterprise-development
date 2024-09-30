using AutoMapper;
using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Repository;
using DispatchTransportControl.Api.Repository.impl;
using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Service.impl;

public class RouteAssignmentService(
    IRouteAssignmentRepository repository,
    IVehicleRepository vehicleRepository,
    IDriverRepository driverRepository,
    IMapper mapper
    ) : IRouteAssignmentService
{
    public IEnumerable<RouteAssignmentDto> GetAll()
    {
        return repository.GetAll().Select(mapper.Map<RouteAssignmentDto>);
    }

    public RouteAssignmentDto? GetById(int id)
    {
        return mapper.Map<RouteAssignmentDto>(repository.GetById(id));
    }

    public RouteAssignmentDto Create(RouteAssignmentCreateDto dto)
    {
        var vehicle = vehicleRepository.GetById(dto.VehicleId);
        if (vehicle == null)
        {
            throw new Exception("Vehicle not found");
        }

        var driver = driverRepository.GetById(dto.DriverId);
        if (driver == null)
        {
            throw new Exception("Driver not found");
        }

        var routeAssignment = new RouteAssignment
        {
            Vehicle = vehicle,
            Driver = driver,
            RouteNumber = dto.RouteNumber,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };

        return mapper.Map<RouteAssignmentDto>(repository.Create(routeAssignment));
    }

    public RouteAssignmentDto Update(RouteAssignmentUpdateDto dto)
    {
        var routeAssignment = repository.GetById(dto.Id);
        if (routeAssignment == null)
        {
            throw new Exception("Route assignment not found");
        }

        var vehicle = vehicleRepository.GetById(dto.VehicleId);
        if (vehicle == null)
        {
            throw new Exception("Vehicle not found");
        }

        var driver = driverRepository.GetById(dto.DriverId);
        if (driver == null)
        {
            throw new Exception("Driver not found");
        }

        routeAssignment.Vehicle = vehicle;
        routeAssignment.Driver = driver;
        routeAssignment.RouteNumber = dto.RouteNumber;
        routeAssignment.StartTime = dto.StartTime;
        routeAssignment.EndTime = dto.EndTime;

        return mapper.Map<RouteAssignmentDto>(repository.Update(routeAssignment));
    }

    public void Delete(int id)
    {
        var routeAssignment = repository.GetById(id);
        if (routeAssignment != null)
        {
            repository.Delete(routeAssignment);
        }
    }
}